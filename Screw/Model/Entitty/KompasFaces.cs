using Kompas6API5;
using Kompas6Constants3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Model.Entitty
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
        /// ksFaceDefinition.IsCylinder() defines is face cylindric or not,
        /// it seems to be unlogical, but in any case: base planes are NOT cylindric,
        /// they are just plane circles
        /// <param name="_doc3DPart">Document 3D part, represents detail</param>
        /// <param name="startIndex">Start index of faces in faces collection</param>
        /// <param name="endIndex">End index of faces in faces collection</param>
        /// <param name="outFirstIndex">First base plane index</param>
        /// <param name="outSecondIndex">Second base plane index</param>
        public static void GetCylinderBasePlaneIndexes(ksPart _doc3DPart, int startIndex, int endIndex, out int outFirstIndex, out int outSecondIndex)
        {
            /* TODO: функция getCylinderBasePlane работает только, если базовая плоскость больше размером, чем получаемая */
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
                uint ST_MIX_SM = 0x0;   // area in santimeters
                var entity = (ksEntity)faceCollection.GetByIndex(i);
                var def = (ksFaceDefinition)entity.GetDefinition();

                var area = Math.Round(def.GetArea(ST_MIX_SM), 10);  // round to 10 numbers

                // If face isn't cylindric and if it is not base face (see xml-comment to this function)
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
        /// Return face which is parallel to base face
        /// </summary>
        /// This algorithm uses areas of faces collection 
        /// in diapason from start index to end index.
        /// The essence of the algorithm is that any extruded figure
        /// has parralel top and bottom planes and sides planes,
        /// and areas of these planes are equal as side planes!
        /// But all these planes are sorted randomly.
        /// If we add any figure to bottom plane
        /// (i.e. create sketch and extrude him on bottom plane)
        /// then area of bottom plane decreases
        /// and we can exactly say which index belongs to each parralel plane!
        /// <param name="_doc3DPart">Kompas part of 3D document</param>
        /// <param name="startIndex">Face collection start index</param>
        /// <param name="endIndex">Face collection end index</param>
        /// <param name="outFirstIndex">First base plane index in faces collection</param>
        /// <param name="outSecondIndex">Second base plane index in faces collection</param>
        public static void GetRegPolyBasePlanesIndexes(ksPart _doc3DPart, int startIndex, int endIndex, out int outFirstIndex, out int outSecondIndex)
        {
            /*	TODO: эта функция работает только с многоугольниками с количеством граней строго больше 4 */
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
                uint ST_MIX_SM = 0x0;   // this is similar to "area in santimeters" definition in API
                var entity = (ksEntity)faceCollection.GetByIndex(i);

                var def = (ksFaceDefinition)entity.GetDefinition();

                // Get unrounded area of plane
                var area = def.GetArea(ST_MIX_SM);

                unroundedList.Add(area);
            }

            // Get minimal epsilon for all unrounded areas (see comment to this function)
            var minimalEpsilon = GetMinimalEspilonOfAreas(unroundedList);

            // Set init list with rounded values
            for (int i = 0, length = unroundedList.Count; i < length; i++)
            {
                initList.Add(Math.Round(unroundedList[i], minimalEpsilon));
            }

            // Get unique areas in this list
            for (int i = 0, length = initList.Count; i < length; i++)
            {
                // If value is not set neither in unique nor in non-unique list --
                // -- then set him to unique list
                if (!uniqList.Contains(initList[i]))
                {
                    if (!notUniqList.Contains(initList[i]))
                    {
                        uniqList.Add(initList[i]);
                    }
                }
                else
                {
                    // Else if he already set in unique list -- 
                    // -- delete from this list and set him to not-unique list, but only once
                    uniqList.Remove(initList[i]);
                    notUniqList.Add(initList[i]);
                }
            }

            // Check this 2 indexes for adjacency with base face of figure
            if (uniqList.Count == 2)
            {
                firstIndex += initList.IndexOf(uniqList[0]);
                secondIndex += initList.IndexOf(uniqList[1]);
            }
            // Else if main base face area is higher than parallel base face area
            // -- then return only first index
            else if (uniqList.Count == 1)
            {
                firstIndex += initList.IndexOf(uniqList[0]);
                secondIndex = -1;
            }
            // Else if parallel faces doesn't have the same area --
            // -- then get value which repeat twice in array
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
            // Else try to do facepalm and exit immediately
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
            // This is optimal epsilon by my countings,
            // works for all areas without any division remainders on mantissa.
            var epsilon = 13;

            // If area is higher then 1 --
            // -- then just round logarithm of area
            if (area > 1)
            {
                return epsilon - (int)Math.Floor(Math.Log10(area)) - 1;
            }
            // Else count amount of zeros after comma
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

        /// </example>
        /// <param name="_doc3DPart">Kompas part of 3D document</param>
        /// <param name="faceIndex1">Base plane index 1</param>
        /// <param name="faceIndex2">Base plane index 2</param>
        /// <param name="baseFaceAreaState"> Base plane area state, using for correct definition of parallel base plane and main base plane indexes.</param>
        /// <returns>Parallel base plane by base plane area state and indexes of faces in detail faces collection</returns>
        public static ksEntity GetParallelBasePlane(ksPart _doc3DPart, int faceIndex1, int faceIndex2, BaseFaceAreaState baseFaceAreaState)
        {
            // If first index isn't defined --
            // -- then just open this and enjoy

            if (faceIndex1 == -1)
            {
                return null;
            }

            var faceCollection = (ksEntityCollection)_doc3DPart.EntityCollection((short)Obj3dType.o3d_face);

            var face1 = (ksEntity)faceCollection.GetByIndex(faceIndex1);
            var faceDefinition1 = (ksFaceDefinition)face1.GetDefinition();

            // If second base face index isn't defined --
            // -- then get first base face
            if (faceIndex2 == -1)
            {
                return face1;
            }

            var face2 = (ksEntity)faceCollection.GetByIndex(faceIndex2);
            var faceDefinition2 = (ksFaceDefinition)face2.GetDefinition();

            var face = (ksEntity)faceCollection.GetByIndex(0);

            uint SM = 0x0; // this is similar to "area in santimeters" in API

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
        /// Get elements count in list
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
    