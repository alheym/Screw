using Kompas6API5;
using Kompas6Constants3D;
using Screw.Error;

namespace Screw.Model.Entity
{
    /// <summary>
    /// Sketch class.
    /// Represents sketch in 2D entity in detail.
    /// </summary>
    class KompasSketch
    {
        /// <summary>
        /// Base plane fo sketch
        /// </summary>
        private ksEntity _basePlane;

        /// <summary>
        /// Axis of base plane
        /// </summary>
        private Obj3dType _basePlaneAxis;

        /// <summary>
        /// Sketch definition
        /// </summary>
        private ksSketchDefinition _sketchDef;

        /// <summary>
        /// Last error code
        /// </summary>
        public ErrorCodes LastErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Sketch entity getter
        /// </summary>
        public ksEntity Entity
        {
            get;
            private set;
        }

        /// <summary>
        /// Entity base plane setter
        /// </summary>
        public ksEntity BasePlane
        {
            get;
            private set;
        }

        /// <summary>
        /// Создать эскиз по базовой плоскости
        /// </summary>
        /// <param name="doc3DPart">Document 3D part</param>
        /// <param name="basePlane">Base plane</param>
        public KompasSketch(ksPart doc3DPart, ksEntity basePlane)
        {
            if (doc3DPart == null || basePlane == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return;
            }

            _basePlane = basePlane;

            Entity = CreateEntity(doc3DPart);
        }

        /// <summary>
        /// Создать эскиз по базовой плоскости
        /// </summary>
        /// <param name="doc3DPart">Document 3D part</param>
        /// <param name="basePlaneAxis">Base plane axis</param>
        public KompasSketch(ksPart doc3DPart, Obj3dType basePlaneAxis)
        {
            if (doc3DPart == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return;
            }

            if (!(basePlaneAxis == Obj3dType.o3d_planeXOY
                || basePlaneAxis == Obj3dType.o3d_planeXOZ
                || basePlaneAxis == Obj3dType.o3d_planeYOZ)
            )
            {
                LastErrorCode = ErrorCodes.ArgumentInvalid;
                return;
            }

            _basePlaneAxis = basePlaneAxis; // ось базовой плоскости

            Entity = CreateEntity(doc3DPart);
        }

        /// <summary>
        /// Начать редактирование объекта
        /// </summary>
        /// <returns>Kompas 2D document (editable sketch)</returns>
        public ksDocument2D BeginEntityEdit()
        {
            if (_sketchDef == null)
            {
                LastErrorCode = ErrorCodes.EntityDefinitionNull;
                return null;
            }
            return (ksDocument2D)_sketchDef.BeginEdit();
        }

        /// <summary>
        /// Конец редактирования объекта
        /// </summary>
        public void EndEntityEdit()
        {
            _sketchDef.EndEdit();
        }

        /// <summary>
        /// Создать объект по базовой плоскости
        /// </summary>
        /// <param name="doc3DPart">Part of 3D document (detail in build)</param>
        /// <returns>true if operation successful; false in case of error</returns>
        private ksEntity CreateEntity(ksPart doc3DPart)
        {
            // эскиз
            var sketch = (ksEntity)doc3DPart.NewEntity((short)Obj3dType.o3d_sketch);
            if (sketch == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return null;
            }

            // Определение эскиза
            var sketchDef = (ksSketchDefinition)sketch.GetDefinition();
            if (sketchDef == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return null;
            }

            // Базовая плоскость для эскиза
            var basePlane = GetBasePlane(doc3DPart);
            if (basePlane == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return null;
            }

            sketchDef.SetPlane(basePlane);
            if (sketch.Create() != true)
            {
                LastErrorCode = ErrorCodes.EntityCreateError;
                return null;
            }

            _sketchDef = sketchDef;

            return sketch;
        }

        /// <summary>
        /// Получить базовую плоскость по оси или получить уже установленную базовую плоскость
        /// </summary>
        /// <param name="doc3DPart">Part of 3D document (detail in build)</param>
        /// <returns>Уже установлена ​​базовая плоскость или базовая плоскость по оси</returns>
        private ksEntity GetBasePlane(ksPart doc3DPart)
        {
            ksEntity basePlane = null;

            if (_basePlane != null)
            {
                basePlane = _basePlane;
            }
            else
            {
                basePlane = (ksEntity)doc3DPart.GetDefaultEntity((short)_basePlaneAxis);
            }

            return basePlane;
        }
    }
}
