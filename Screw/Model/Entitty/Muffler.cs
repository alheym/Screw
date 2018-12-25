using Kompas6Constants3D;
using Screw.Error;
using Screw.Manager;
using Screw.Model.FigureParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Model.Entitty
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
        /// Muffler manager constructor
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

        public Muffler(KompasApplication kompasApp, MufflerParameters mufflerParameters)
        {
            _kompasApp = kompasApp;
        }

        /// <summary>
        /// Create muffler in detail in base plane axis
        /// </summary>
        /// <param name="figureParameters">Parameters of muffler</param>
        /// <param name="basePlane">Base plane of muffler, by default is null</param>
        /// <returns>Muffler extrusion or null if extrusion returns error</returns>
        private KompasExtrusion CreateMuffler(MufflerParameters figureParameters, ksEntity basePlane = null)
        {
            // Muffler sketch
            var muffler = new KompasSketch(figureParameters.Document3DPart, figureParameters.BasePlaneAxis);

            // If base plane is set --
            // -- create sketch of muffler on it 
            // instead of base plane axis
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

            // Muffler rectangle, width and height are screw hat width
            var mufflerRectangleParam = new RectangleParameter(_kompasApp, _kompasApp.Parameters[0], _kompasApp.Parameters[0], figureParameters.BasePlanePoint);
            if (mufflerSketchEdit.ksRectangle(mufflerRectangleParam.FigureParam) == 0)
            {
                LastErrorCode = ErrorCodes.Document2DRectangleCreateError;
                return null;
            }

            muffler.EndEntityEdit();

            // Muffler extrusion, height of muffler is nut height / 4
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
        /// Delete muffler from document 3D part
        /// </summary>
        public bool DeleteDetail()
        {
            if (Extrusion == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return false;
            }

            // Muffler deletion
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
