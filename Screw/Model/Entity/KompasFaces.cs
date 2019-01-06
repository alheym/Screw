using System;
using System.Linq;
using System.Collections.Generic;
using Kompas6API5;
using Kompas6Constants3D;
using Screw.Validator;

namespace Screw.Model.Entity
{
    static class KompasFaces
    {
        /// <summary>
        /// Base face area state: 
        /// higher than parralel base face 
        /// or lower than the latter
        /// </summary>
        public enum BaseFaceAreaState { BaseFaceAreaHigher, BaseFaceAreaLower }

        /// <summary>
        /// Get face entity by index
        /// </summary>
        /// <param name="_doc3DPart">Kompas part of 3D document</param>
        /// <param name="index">Returned face index</param>
        /// <returns>Face entity by index</returns>
        public static ksEntity GetFaceEntityByIndex(ksPart _doc3DPart, int index)
        {
            var faceCollection = (ksEntityCollection)_doc3DPart.EntityCollection((short)Obj3dType.o3d_face);
            var face = (ksEntity)faceCollection.GetByIndex(index);

            if (face == null)
            {
                return null;
            }
            else
            {
                return face;
            }
        }

        /// <summary>
        /// Get face definition by index
        /// </summary>
        /// <param name="_doc3DPart">Kompas part of 3D document</param>
        /// <param name="index">Returned face index</param>
        /// <returns>Face definition by index</returns>
        public static ksFaceDefinition GetFaceByIndex(ksPart _doc3DPart, int index)
        {
            var faceCollection = (ksEntityCollection)_doc3DPart.EntityCollection((short)Obj3dType.o3d_face);
            var faceEntity = (ksEntity)faceCollection.GetByIndex(index);
            var face = (ksFaceDefinition)faceEntity.GetDefinition();

            if (face == null)
            {
                return null;
            }
            else
            {
                return face;
            }
        }

        /// <summary>
        /// Get cylinder base plane indexes by indexes of faces of cylinder inside detail faces collection
        /// </summary>
        /// ksFaceDefinition.IsCylinder () определяет, является ли грань цилиндрической или нет,
        /// это кажется нелогичным, но в любом случае: базовые плоскости НЕ являются цилиндрическими,
        /// они просто плоские круги
        /// <param name="_doc3DPart">Document 3D part, represents detail</param>
        /// <param name="startIndex">Start index of faces in faces collection</param>
        /// <param name="endIndex">End index of faces in faces collection</param>
        /// <param name="outFirstIndex">First base plane index</param>
        /// <param name="outSecondIndex">Second base plane index</param>
        public static void GetCylinderBasePlaneIndexes(ksPart _doc3DPart, int startIndex, int endIndex, out int outFirstIndex, out int outSecondIndex)
        {
            /* функция getCylinderBasePlane работает только, если базовая плоскость больше размером, чем получаемая */
            var faceCollection = (ksEntityCollection)_doc3DPart.EntityCollection((short)Obj3dType.o3d_face);

            if (faceCollection == null
                || !DoubleValidator.Validate(startIndex)
                || !DoubleValidator.Validate(endIndex)
            )
            {
                outFirstIndex = outSecondIndex = -1;
                return;
            }

            bool isFirstIndexSet = false;

            int firstIndex = -1;
            int secondIndex = -1;

            for (int i = startIndex - 1; i < endIndex; i++)
            {
                uint ST_MIX_SM = 0x0;   //площадь в сантиметрах
                var entity = (ksEntity)faceCollection.GetByIndex(i);
                var def = (ksFaceDefinition)entity.GetDefinition();

                var area = Math.Round(def.GetArea(ST_MIX_SM), 10);  //округлить до 10 чисел

                // Если грань не является цилиндрической, и если она не является базовой гранью
                if (!def.IsCylinder() && isFirstIndexSet == false)
                {
                    isFirstIndexSet = true;
                    firstIndex = i;
                }
                else if (!def.IsCylinder() && isFirstIndexSet == true)
                {
                    secondIndex = i;
                }
            }

            outFirstIndex = firstIndex;
            outSecondIndex = secondIndex;

            return;
        }

        /// <summary>
        /// Вернуть грань, параллельную базовой грани
        /// </summary>
        /// Этот алгоритм использует области сбора граней в диапазоне от начального индекса до конечного индекса. 
        /// Суть алгоритма заключается в том, что любая вытянутая фигура
        /// имеет верхнюю и нижнюю плоскости и параллельные плоскости, и области этих плоскостей равны боковым плоскостям!
        /// Но все отсортированы случайным образом.
        /// Если мы добавим любую фигуру в нижнюю плоскость
        /// (т.е.создать эскиз и выдавить его на нижнюю плоскость), тогда площадь нижней плоскости уменьшается, 
        /// и мы можем точно сказать, какой индекс принадлежит каждой плоскости
        /// <param name="_doc3DPart">Kompas part of 3D document</param>
        /// <param name="startIndex">Face collection start index</param>
        /// <param name="endIndex">Face collection end index</param>
        /// <param name="outFirstIndex">First base plane index in faces collection</param>
        /// <param name="outSecondIndex">Second base plane index in faces collection</param>
        public static void GetRegPolyBasePlanesIndexes(ksPart _doc3DPart, int startIndex, int endIndex, out int outFirstIndex, out int outSecondIndex)
        {
            // Collection of entities in all figure
            var faceCollection = (ksEntityCollection)_doc3DPart.EntityCollection((short)Obj3dType.o3d_face);

            List<double> initList = new List<double>();
            List<double> unroundedList = new List<double>();
            List<double> uniqList = new List<double>();
            List<double> notUniqList = new List<double>();

            if (faceCollection == null
                || !DoubleValidator.Validate(startIndex)
                || !DoubleValidator.Validate(endIndex)
            )
            {
                outFirstIndex = outSecondIndex = -1;
                return;
            }

            int firstIndex = startIndex - 1;
            int secondIndex = startIndex - 1;

            // Set figure faces areas list with all areas
            for (int i = startIndex - 1; i < endIndex; i++)
            {
                uint ST_MIX_SM = 0x0;   //это похоже на определение "области в сантиметрах" в API
                var entity = (ksEntity)faceCollection.GetByIndex(i);

                var def = (ksFaceDefinition)entity.GetDefinition();

                // Get unrounded area of plane
                var area = def.GetArea(ST_MIX_SM);

                unroundedList.Add(area);
            }

            // Get minimal epsilon for all unrounded areas
            var minimalEpsilon = GetMinimalEspilonOfAreas(unroundedList);

            // Установить список инициализации с округленными значениями
            for (int i = 0, length = unroundedList.Count; i < length; i++)
            {
                initList.Add(Math.Round(unroundedList[i], minimalEpsilon));
            }

            // Получить уникальные области в списке
            for (int i = 0, length = initList.Count; i < length; i++)
            {
                // if значение не задано ни в уникальном, ни в неуникальном списке -
                // else установить его в уникальный список
                if (!uniqList.Contains(initList[i]))
                {
                    if (!notUniqList.Contains(initList[i]))
                    {
                        uniqList.Add(initList[i]);
                    }
                }
                else
                {
                    // Else если он уже указан в уникальном списке -
                    // -- удалить из этого списка и установить его в неуникальный список, но только один раз
                    uniqList.Remove(initList[i]);
                    notUniqList.Add(initList[i]);
                }
            }

            // Проверьте эти 2 индекса на смежность с базовой гранью фигуры
            if (uniqList.Count == 2)
            {
                firstIndex += initList.IndexOf(uniqList[0]);
                secondIndex += initList.IndexOf(uniqList[1]);
            }
            // Else если площадь основной базовой поверхности выше, чем площадь параллельной базовой поверхности
            // -- затем вернуть только первый индекс
            else if (uniqList.Count == 1)
            {
                firstIndex += initList.IndexOf(uniqList[0]);
                secondIndex = -1;
            }
            // Else если параллельные грани не имеют одинаковую площадь --
            // -- затем получить значение, которое повторяется дважды в массиве
            else if (uniqList.Count == 0)
            {
                bool getFirstElement = false;

                for (int i = 0, count = initList.Count; i < count; i++)
                {
                    bool isTwoElements = (GetElementCountInList(initList, initList[i]) == 2);

                    if ((isTwoElements == true) && (getFirstElement == false))
                    {
                        firstIndex += i;
                        getFirstElement = true;
                    }
                    else if ((isTwoElements == true) && (getFirstElement == true))
                    {
                        secondIndex += i;
                        break;
                    }
                }
            }
            
            else
            {
                firstIndex = secondIndex = -1;
            }

            outFirstIndex = firstIndex;
            outSecondIndex = secondIndex;
        }

        /// <summary>
        /// Get minimal epsilon in unrounded areas list
        /// </summary>
        /// <param name="areas">Unrounded areas list</param>
        /// <returns>Minimal epsilon or -1 in case of error</returns>
        private static int GetMinimalEspilonOfAreas(List<double> areas)
        {
            var epsilons = new List<int>();

            for (int i = 0, length = areas.Count; i < length; i++)
            {
                epsilons.Add(GetEpsilonOfArea(areas[i]));
            }

            var epsilon = epsilons.Max();
            // Maximum available epsilon is 15
            if (epsilon > 15)
            {
                epsilon = 15;
            }

            return epsilon;
        }

        /// <summary>
        /// Get epsilon to adequate round area
        /// </summary>
        /// <param name="area">Area of plane</param>
        /// <returns></returns>
        private static int GetEpsilonOfArea(double area)
        {
            // This is optimal epsilon,
            // works for all areas without
            var epsilon = 13;

            // If площадь выше 1--
            // -- тогда просто круглый логарифм площади
            if (area > 1)
            {
                return epsilon - (int)Math.Floor(Math.Log10(area)) - 1;
            }
            // Else посчитать количество нулей после запятой
            else if (area < 1)
            {
                var decimals = 0;

                while (area < 1)
                {
                    area *= 10;
                    decimals++;
                }

                return epsilon + decimals - 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Получить базовую плоскость, параллельную _основной базовой плоскости.
        /// Разница между этими плоскостями заключается в том, что базовая плоскость _main_ является выдавливаемой сущностью,
        /// а базовая плоскость _parallel_ выдавленной вытянутой сущностью.
        /// </summary>
        /// <param name="_doc3DPart">Kompas part of 3D document</param>
        /// <param name="faceIndex1">Base plane index 1</param>
        /// <param name="faceIndex2">Base plane index 2</param>
        /// <param name="baseFaceAreaState"> Base plane area state, using for correct definition of parallel base plane and main base plane indexes.</param>
        /// <returns>Parallel base plane by base plane area state and indexes of faces in detail faces collection</returns>
        public static ksEntity GetParallelBasePlane(ksPart _doc3DPart, int faceIndex1, int faceIndex2, BaseFaceAreaState baseFaceAreaState)
        {
            // If первый индекс не определен --
            if (faceIndex1 == -1)
            {
                return null;
            }

            var faceCollection = (ksEntityCollection)_doc3DPart.EntityCollection((short)Obj3dType.o3d_face);

            var face1 = (ksEntity)faceCollection.GetByIndex(faceIndex1);
            var faceDefinition1 = (ksFaceDefinition)face1.GetDefinition();

            // If второй базовый индекс face не определен --
            // -- затем получите первое базовое face
            if (faceIndex2 == -1)
            {
                return face1;
            }

            var face2 = (ksEntity)faceCollection.GetByIndex(faceIndex2);
            var faceDefinition2 = (ksFaceDefinition)face2.GetDefinition();

            var face = (ksEntity)faceCollection.GetByIndex(0);

            uint SM = 0x0; // это похоже на "площадь в сантиметрах" в API

            switch (baseFaceAreaState)
            {
                case BaseFaceAreaState.BaseFaceAreaHigher:
                    if (faceDefinition1.GetArea(SM) > faceDefinition2.GetArea(SM))
                    {
                        face = face2;
                    }
                    else
                    {
                        face = face1;
                    }
                    break;
                case BaseFaceAreaState.BaseFaceAreaLower:
                    if (faceDefinition1.GetArea(SM) < faceDefinition2.GetArea(SM))
                    {
                        face = face2;
                    }
                    else
                    {
                        face = face1;
                    }
                    break;
            }

            return face;
        }

        /// <summary>
        /// Draws the word "XYZ" on plane. 
        /// Эта функция используется для отладки других функций
        /// которые работают с эскизами.
        /// </summary>
        /// <param name="_doc3DPart">Kompas part of 3D document</param>
        /// <param name="plane">Selected plane</param>
        public static void DrawXyzOnPlane(ksPart _doc3DPart, ksEntity plane)
        {
            var xyz = new KompasSketch(_doc3DPart, plane);

            var xyzEdit = xyz.BeginEntityEdit();

            // Draw X
            xyzEdit.ksLineSeg(0, 0, -5, -10, 1);        // "/"
            xyzEdit.ksLineSeg(0, -10, -5, 0, 1);        // "\"

            // Draw Y
            xyzEdit.ksLineSeg(-7, 0, -13, -10, 1);      // "/"
            xyzEdit.ksLineSeg(-7, -10, -10, -5, 1);     // верхняя половина "\"

            // Draw Z
            xyzEdit.ksLineSeg(-15, -10, -20, -10, 1);   // верх "--"
            xyzEdit.ksLineSeg(-20, -10, -15, 0, 1);     // "/"
            xyzEdit.ksLineSeg(-15, 0, -20, 0, 1);       // низ "--"

            xyz.EndEntityEdit();
        }


        /// <summary>
        ///Получить количество элементов в списке
        /// </summary>
        /// <param name="list">List of doubles</param>
        /// <param name="findElement">Element to find</param>
        /// <returns>Elements count in list</returns>
        private static int GetElementCountInList(List<double> list, double findElement)
        {
            int count = 0;

            foreach (var item in list)
            {
                if (item == findElement) count++;
            }

            return count;
        }
    }
}
