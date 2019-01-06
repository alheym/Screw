using Kompas6API5;
using Kompas6Constants3D;
using Screw.Model.Point;
using Screw.Model.FigureParam;
using Screw.Model.Entity;
using Screw.Error;
using Screw.Validator;
using Screw;

namespace Screw.Model.Entity
{
    class Muffler
    {
        /// <summary>
        /// Parameters of muffler
        /// </summary>
        private MufflerParameters _figureParameters;

        /// <summary>
        /// Kompas object 
        /// </summary>
        private KompasApplication _kompasApp;

        /// <summary>
        /// Last error code getter
        /// </summary>
        public ErrorCodes LastErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Muffler extrusion getter
        /// </summary>
        public KompasExtrusion Extrusion
        {
            get;
            private set;
        }

        /// <summary>
        /// Конструктор глушителя
        /// </summary>
        /// <param name="figureParameters">Parameters of muffler</param>
        /// <param name="kompasApp">Kompas application specimen</param>
        /// <param name="basePlane">Base plane of muffler, by default is null</param>
        public Muffler(KompasApplication kompasApp, MufflerParameters figureParameters, ksEntity basePlane = null)
        {
            if (kompasApp == null
                || figureParameters.Document3DPart == null
                || figureParameters.BasePlanePoint.LastErrorCode != ErrorCodes.OK
                || !(figureParameters.BasePlaneAxis == Obj3dType.o3d_planeXOY
                || figureParameters.BasePlaneAxis == Obj3dType.o3d_planeXOZ
                || figureParameters.BasePlaneAxis == Obj3dType.o3d_planeYOZ)
                || !DoubleValidator.Validate(figureParameters.BasePlanePoint.X)
                || !DoubleValidator.Validate(figureParameters.BasePlanePoint.Y)
            )
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return;
            }
            if (!(figureParameters.Direction == Direction_Type.dtNormal
                || figureParameters.Direction == Direction_Type.dtReverse)
            )
            {
                LastErrorCode = ErrorCodes.ArgumentInvalid;
                return;
            }

            _kompasApp = kompasApp;
            _figureParameters = figureParameters;

            Extrusion = CreateMuffler(figureParameters, basePlane);
            if (Extrusion == null)
            {
                return;
            }
        }

        /// <summary>
        /// Создать глушитель оси базовой плоскости
        /// </summary>
        /// <param name="figureParameters">Parameters of muffler</param>
        /// <param name="basePlane">Base plane of muffler, by default is null</param>
        /// <returns>Выдавливание глушителя или ноль, если выдавливание возвращает ошибку</returns>
        private KompasExtrusion CreateMuffler(MufflerParameters figureParameters, ksEntity basePlane = null)
        {
            // Muffler sketch
            var muffler = new KompasSketch(figureParameters.Document3DPart, figureParameters.BasePlaneAxis);

            // If базовая плоскость установлена --
            // -- создать эскиз глушителя на нем
            // вместо оси базовой плоскости
            if (basePlane != null)
            {
                muffler = new KompasSketch(figureParameters.Document3DPart, basePlane);
            }
            if (muffler.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = muffler.LastErrorCode;
                return null;
            }

            var mufflerSketchEdit = muffler.BeginEntityEdit();
            if (mufflerSketchEdit == null)
            {
                LastErrorCode = ErrorCodes.EntityCreateError;
                return null;
            }

            // Глушитель прямоугольник, ширина и высота диаметр шляпки
            var mufflerRectangleParam = new RectangleParameter(_kompasApp, _kompasApp.Parameters[0], _kompasApp.Parameters[0], figureParameters.BasePlanePoint);
            if (mufflerSketchEdit.ksRectangle(mufflerRectangleParam.FigureParam) == 0)
            {
                LastErrorCode = ErrorCodes.Document2DRectangleCreateError;
                return null;
            }

            muffler.EndEntityEdit();

            // Выдавливание глушителя, высота глушителя высота шляпки / 4
            var extrusionParameters = new KompasExtrusionParameters(figureParameters.Document3DPart, Obj3dType.o3d_baseExtrusion, muffler.Entity, figureParameters.Direction, _kompasApp.Parameters[4] / 4.0);
            var mufflerExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity);

            if (mufflerExtrusion.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = mufflerExtrusion.LastErrorCode;
                return null;
            }

            return mufflerExtrusion;
        }

        /// <summary>
        /// Удалить глушитель из 3D детали документа
        /// </summary>
        public bool DeleteDetail()
        {
            if (Extrusion == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return false;
            }

            // Удаление глушителя
            Extrusion.BaseFaceAreaState = KompasFaces.BaseFaceAreaState.BaseFaceAreaLower;
            var extruded = Extrusion.ExtrudedEntity;
            if (extruded == null)
            {
                LastErrorCode = Extrusion.LastErrorCode;
                return false;
            }

            var extrusionParameters = new KompasExtrusionParameters(_figureParameters.Document3DPart, Obj3dType.o3d_cutExtrusion, extruded, _figureParameters.Direction, _kompasApp.Parameters[4] / 4.0);
            var mufflerDeletion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity);

            if (mufflerDeletion.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = mufflerDeletion.LastErrorCode;
                return false;
            }

            return true;
        }
    }
}
