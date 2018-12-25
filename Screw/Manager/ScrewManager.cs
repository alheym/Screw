using Kompas6API5;
using Kompas6Constants3D;
using Screw.Error;
using Screw.Model;
using Screw.Model.Entitty;
using Screw.Model.FigureParam;
using Screw.Model.Point;
using Screw.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Manager
{
    class ScrewManager : IManagable
    {
        /// <summary>
		/// Kompas application
		/// </summary>
		private KompasApplication _kompasApp;

        /// <summary>
        /// Last error code
        /// </summary>
        public ErrorCodes LastErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Шаг резьбы винта
        /// </summary>
        public double ThreadStep
        {
            get;
            private set;
        }

        /// <summary>
        /// Конструктор ScrewManager
        /// </summary>
        public ScrewManager(KompasApplication kompasApp)
        {
            if (kompasApp == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return;
            }

            _kompasApp = kompasApp;
        }

        /// <summary>
        /// Create detail
        /// </summary>
        /// <returns>true - если операция успешна; false - при возникновении ошибок</returns>
        public bool CreateDetail()
        {
            var basePlaneOfHat = CreateHat();
            if (basePlaneOfHat == null)
            {
                return false;
            }

            var carvingEntities = CreateBase(basePlaneOfHat);
            if (carvingEntities == null
                || carvingEntities[0] == null
                || carvingEntities[1] == null
            )
            {
                return false;
            }

            if (!CreateThread(carvingEntities)) return false;

            return true;
        }

        /// <summary>
        /// Создание шляпки винта с операцией выдавливания
        /// </summary>
        /// "regPoly" сокрашение от многоугольника (Regular Polygon)
        /// Шляпка винта состоит из основания шляпы (0.84 * H) 
        /// и закругленной фаски на вершине (0.16 * H)
        /// <returns>Выдавленная шляпка винта</returns>
        private ksEntity CreateHat()
        {
            // 0.1 Create muffler, which base point is (W3 / 5, W3 / 5)
            var basePoint = -(_kompasApp.Parameters[0] / 5.0);
            var mufflerParameters = new MufflerParameters();
            mufflerParameters.Document3DPart = _kompasApp.ScrewPart;
            mufflerParameters.Direction = Direction_Type.dtNormal;
            mufflerParameters.BasePlaneAxis = Obj3dType.o3d_planeYOZ;
            mufflerParameters.BasePlanePoint = new KompasPoint2D(basePoint, basePoint);

            var mufflerManager = new Muffler(_kompasApp, mufflerParameters);
            if (mufflerManager.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = mufflerManager.LastErrorCode;
                return null;
            }

            var mufflerExtrusion = mufflerManager.Extrusion;

            if (mufflerExtrusion == null)
            {
                LastErrorCode = mufflerManager.LastErrorCode;
                return null;
            }

            // 1.1 Эскиз шляпки
            var regPolySketch = new KompasSketch(_kompasApp.ScrewPart, Obj3dType.o3d_planeYOZ);
            if (regPolySketch.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = regPolySketch.LastErrorCode;
                return null;
            }

            var regPolySketchEdit = regPolySketch.BeginEntityEdit();
            if (regPolySketchEdit == null)
            {
                LastErrorCode = regPolySketch.LastErrorCode;
                return null;
            }

            // Центральная точка многоугольника
            var regPolyPoint = new KompasPoint2D(0, 0);
            //Многоугольник основа шляпки
            var regPolyParam = new RegularPolygonParameter(_kompasApp, 6, _kompasApp.Parameters[0] / 2.0, regPolyPoint);  // W3
            if (regPolySketchEdit.ksRegularPolygon(regPolyParam.FigureParam, 0) == 0)
            {
                LastErrorCode = ErrorCodes.Document2DRegPolyCreatingError;
                return null;
            }

            regPolySketch.EndEntityEdit();

            // 1.2 Выдавливание шляпки
            // Screw hat height is equal to nut height
            var extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_baseExtrusion, regPolySketch.Entity, Direction_Type.dtReverse, _kompasApp.Parameters[4] * 0.84);
            var regPolyExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity); // 0.84 * H
            if (regPolyExtrusion.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = regPolyExtrusion.LastErrorCode;
                return null;
            }

            /* Main base face area is lower than parallel base face
			 * because of muffler partially overlaps main base face area,
			 * but not overlaps parallel base face area.
			 */
            regPolyExtrusion.BaseFaceAreaState = KompasFaces.BaseFaceAreaState.BaseFaceAreaLower;
            var extruded = regPolyExtrusion.ExtrudedEntity;
            if (extruded == null)
            {
                LastErrorCode = regPolyExtrusion.LastErrorCode;
                return null;
            }

            // 0.2 Delete muffler
            if (!mufflerManager.DeleteDetail())
            {
                LastErrorCode = mufflerManager.LastErrorCode;
                return null;
            }

            //// 1.3 Rounded chamfer in hat
            //var roundedChamferParameters = new RoundedChamferParameters();
            //roundedChamferParameters.Document3DPart = _kompasApp.ScrewPart;
            //roundedChamferParameters.RegularPolygonSketch = regPolySketch.Entity;
            //roundedChamferParameters.RegularPolygonParameters = regPolyParam;
            //roundedChamferParameters.BasePlanePoint = new KompasPoint2D(0.0, 0.0);
            //roundedChamferParameters.Direction = Direction_Type.dtNormal;
            //var roundedChamferManager = new RoundedChamfer(_kompasApp, roundedChamferParameters);

            //if (!roundedChamferManager.CreateDetail())
            //{
            //    LastErrorCode = roundedChamferManager.LastErrorCode;
            //    return null;
            //}

            //return extruded;
        }

        /// <summary>
        /// Create screw base with extrusion operation
        /// </summary>
        /// Width of screw base cylinder is 0.7 * W3
        /// <param name="basePlaneofHat">Base plane of hat of screw</param>
        /// <returns>
        /// Carving entities: smooth part end and thread part end, 
        /// these ones need for thread operation
        /// </returns>
        private ksEntity[] CreateBase(ksEntity basePlaneOfHat)
        {
            // 1. Screw base creation
            // 1.1 Screw base entity
            var screwBase = new KompasSketch(_kompasApp.ScrewPart, basePlaneOfHat);
            if (screwBase.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = screwBase.LastErrorCode;
                return null;
            }

            var screwBasePoint = new KompasPoint2D(0, 0);
            if (screwBasePoint.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = screwBasePoint.LastErrorCode;
                return null;
            }

            var screwBaseSketchEdit = screwBase.BeginEntityEdit();
            if (screwBaseSketchEdit == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return null;
            }
            if (screwBaseSketchEdit.ksCircle(screwBasePoint.X, screwBasePoint.Y, _kompasApp.Parameters[0] * 0.7 / 2.0, 1) == 0) // / 0.7 * W3 /
            {
                LastErrorCode = ErrorCodes.Document2DCircleCreatingError;
                return null;
            }
            screwBase.EndEntityEdit();

            // 1.2 Screw base extrusion
            var extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_baseExtrusion, screwBase.Entity, Direction_Type.dtNormal, _kompasApp.Parameters[2]);
            var screwBaseExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity); // W1
            if (screwBaseExtrusion.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = screwBaseExtrusion.LastErrorCode;
                return null;
            }

            screwBaseExtrusion.BaseFaceAreaState = KompasFaces.BaseFaceAreaState.BaseFaceAreaLower;
            var extruded = screwBaseExtrusion.ExtrudedEntity;
            if (extruded == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return null;
            }

            // 1.3 Screw base carving
            var screwCarving = new KompasSketch(_kompasApp.ScrewPart, screwBaseExtrusion.ExtrudedEntity); // Last entity in last extrusion
            if (screwCarving.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = screwCarving.LastErrorCode;
                return null;
            }

            var screwCarvingSketchEdit = screwCarving.BeginEntityEdit();
            if (screwCarvingSketchEdit == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return null;
            }
            // Carving is 1/4 of 70 % (i.e. is 0.525) of W3
            if (screwCarvingSketchEdit.ksCircle(0, 0, (_kompasApp.Parameters[0] * 0.525 / 2.0), 1) == 0)   // / W1 thread /
            {
                LastErrorCode = ErrorCodes.Document2DCircleCreatingError;
                return null;
            }
            screwCarving.EndEntityEdit();

            // 1.4 Screw base carving extrusion
            extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_baseExtrusion, screwCarving.Entity, Direction_Type.dtNormal, _kompasApp.Parameters[3]);
            var screwCarvingExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity);    //  / W2 /
            if (screwCarvingExtrusion.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = screwCarvingExtrusion.LastErrorCode;
                return null;
            }

            screwCarvingExtrusion.BaseFaceAreaState = KompasFaces.BaseFaceAreaState.BaseFaceAreaLower;
            extruded = screwCarvingExtrusion.ExtrudedEntity;
            if (extruded == null)
            {
                LastErrorCode = screwCarvingExtrusion.LastErrorCode;
                return null;
            }

            return new ksEntity[2] { screwCarving.Entity, extruded };
        }

        /// <summary>
        /// Create thread of base of screw
        /// </summary>
        /// <returns>true if operation successful; false in case of error</returns>
        private bool CreateThread(ksEntity[] carvingEntities)
        {
            // 1.5 Screw base thread spin
            // Spin step by russian GOST is equal to 0.037 (i.e. 3.7%) of spin height
            var spinParameters = new SpinParameters();
            spinParameters.Document3DPart = _kompasApp.ScrewPart;
            spinParameters.BeginSpinFace = carvingEntities[1];
            spinParameters.EndSpinFace = carvingEntities[0];
            spinParameters.SpinLocationPoint = new KompasPoint2D(0, 0);
            spinParameters.DiameterSize = _kompasApp.Parameters[0] * 0.7; // 0.7 W3
            spinParameters.SpinStep = _kompasApp.Parameters[3] * 0.037; //  0.037 W2

            var screwThreadSpin = new Spin(spinParameters);
            if (screwThreadSpin.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = screwThreadSpin.LastErrorCode;
                return false;
            }

            ThreadStep = screwThreadSpin.SpinStep;
            if (!DoubleValidator.Validate(ThreadStep))
            {
                LastErrorCode = ErrorCodes.DoubleValueValidationError;
                return false;
            }

            // 1.6 Screw base thread sketch
            var screwThreadSketch = new KompasSketch(_kompasApp.ScrewPart, Obj3dType.o3d_planeXOZ);
            if (screwThreadSketch.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = screwThreadSketch.LastErrorCode;
                return false;
            }

            var screwThreadEdit = screwThreadSketch.BeginEntityEdit();
            if (screwThreadEdit == null)
            {
                LastErrorCode = screwThreadSketch.LastErrorCode;
                return false;
            }

            var step = screwThreadSpin.SpinStep;
            // W1 + W2 + 3 thread steps - 0.86 * W3 (because part with 0.16 * W3 is in coordinates which are less than zero of XOZ)
            var endX = _kompasApp.Parameters[2] + _kompasApp.Parameters[3] + _kompasApp.Parameters[4] * 0.86;

            var startY = -(_kompasApp.Parameters[0] * 0.7 / 2.0);   // 0.7 * W3
            var endY = -(3.0 / 4.0 * _kompasApp.Parameters[0] * 0.7) / 2.0;    // 0.7 * W3 on end of base

            //	Draw triangle: the base of thread.
            screwThreadEdit.ksLineSeg(endX - step, endY, endX, endY, 1);
            screwThreadEdit.ksLineSeg(endX, endY, endX - step / 2.0, startY, 1);
            screwThreadEdit.ksLineSeg(endX - step / 2.0, startY, endX - step, endY, 1);

            screwThreadSketch.EndEntityEdit();

            // 1.7 Screw base thread extrusion
            var spinCollection = (ksEntityCollection)_kompasApp.ScrewPart.EntityCollection((short)Obj3dType.o3d_cylindricSpiral);
            if (spinCollection == null)
            {
                LastErrorCode = ErrorCodes.EntityCollectionCreateError;
                return false;
            }
            spinCollection.Clear();

            spinCollection.Add(screwThreadSpin.Entity);
            spinCollection.refresh();

            if (spinCollection.GetCount() != 1)
            {
                LastErrorCode = ErrorCodes.EntityCollectionWrong;
            }

            var extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_baseEvolution, screwThreadSketch.Entity, spinCollection);
            var screwThreadExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.BySketchesCollection);
            if (screwThreadExtrusion.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = ErrorCodes.EntityCreateError;
                return false;
            }

            return true;
        }
    }
}
