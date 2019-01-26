﻿using Kompas6API5;
using Kompas6Constants3D;
using Screw.Model.Point;
using Screw.Model.FigureParam;
using Screw.Model.Entity;
using Screw.Error;
using Screw.Validator;
using Screw.Model;

namespace Screw.Manager
{
    class ScrewManager : IBuildable
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
        /// Type of screwdriver.
        /// </summary>
        public Screwdriver ScrewdriverType = Screwdriver.WithoutHole;



        /// <summary>
        /// Step of thread of screw
        /// </summary>
        public double ThreadStep
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor of screw manager
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
        /// <returns>true if operation successful; false in case of error</returns>
        public bool CreateDetail()
        {
            var basePlaneOfHat = CreateHat();
            if (basePlaneOfHat == null)
            {
                return false;
            }

            if (ScrewdriverType != Screwdriver.WithoutHole)
            {
                ScrewdriverBase builder = null;

                switch (ScrewdriverType)
                {
                    case Screwdriver.CrossheadScrewdriver:
                        builder = new CrossheadScrewdriver(_kompasApp);
                        break;
                    case Screwdriver.FlatheadScrewdriver:
                        builder = new FlatheadScrewdriver(_kompasApp);
                        break;
                    case Screwdriver.RegularPolygonScrewdriver:
                        builder = new RegularPolygonScrewdriver(_kompasApp);
                        break;
                }
              
                if (builder.LastErrorCode != ErrorCodes.OK)
                {
                    LastErrorCode = builder.LastErrorCode;
                    return false;
                }

                builder.BuildScrewdriver();
            }

            var carvingEntities = CreateBase(basePlaneOfHat);
            if (carvingEntities == null
                || carvingEntities[0] == null
                || carvingEntities == null
            )
            {
                return false;
            }

            if (!CreateThread(carvingEntities)) return false;

            return true;
        }

        /// <summary>
        /// Создание шляпки винта с методом выдавливания
        /// </summary>
        /// <returns>Выдавленная шляпки винта для базовой части винта</returns>
        private ksEntity CreateHat()
        {
            // 0.1 Create muffler, which base point is (D / 5, D / 5)
            var basePoint = -(_kompasApp.Parameters[0] / 5.0);
            var mufflerParameters = new MufflerParameters
            {
                Document3DPart = _kompasApp.ScrewPart,
                Direction = Direction_Type.dtNormal,
                BasePlaneAxis = Obj3dType.o3d_planeYOZ,
                BasePlanePoint = new KompasPoint2D(basePoint, basePoint)
            };

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

            // 1.1 Create hat
            var screwHat = new KompasSketch(_kompasApp.ScrewPart, Obj3dType.o3d_planeYOZ);
            if (screwHat.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = screwHat.LastErrorCode;
                return null;
            }

            var screwHatSketchEdit = screwHat.BeginEntityEdit();
            if (screwHatSketchEdit == null)
            {
                LastErrorCode = screwHat.LastErrorCode;
                return null;
            }

            var screwHatPoint = new KompasPoint2D(0, 0);
            if (screwHatSketchEdit.ksCircle(screwHatPoint.X, screwHatPoint.Y, _kompasApp.Parameters[0] / 2, 1) == 0)
            {
                LastErrorCode = ErrorCodes.Document2DCircleCreatingError;
                return null;
            }
            screwHat.EndEntityEdit();

            // 1.2 Screw base extrusion
            var extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_baseExtrusion, screwHat.Entity, 
                Direction_Type.dtReverse, _kompasApp.Parameters[4] * 0.84);
            var screwHatExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity); // H
            if (screwHatExtrusion.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = screwHatExtrusion.LastErrorCode;
                return null;
            }

            screwHatExtrusion.BaseFaceAreaState = KompasFaces.BaseFaceAreaState.BaseFaceAreaLower;
            var extruded = screwHatExtrusion.ExtrudedEntity;
            if (extruded == null)
            {
                LastErrorCode =
                    screwHatExtrusion.LastErrorCode;
                return null;
            }
            // 0.2 Delete muffler
            if (!mufflerManager.DeleteDetail())
            {
                LastErrorCode = mufflerManager.LastErrorCode;
                return null;
            }

            return extruded;
        }

        /// <summary>
        /// Create screw base with extrusion operation
        /// </summary>
        /// Width of screw base cylinder is 0.7 * D
        /// <param name="basePlaneofHat">Base plane of hat of screw</param>
        /// <returns>
        /// Carving entities: smooth part end and thread part end
        /// </returns>
        private ksEntity[] CreateBase(ksEntity basePlaneOfHat)
        {
            var coef = 0.7;
            var gostbase = 0.525;
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
            // Диаметр резьбы составляет 0.7 от диаметра шляпки (0.7 * D)
            if (screwBaseSketchEdit.ksCircle(screwBasePoint.X, screwBasePoint.Y, _kompasApp.Parameters[0] * coef / 2.0, 1) == 0)

            {
                LastErrorCode = ErrorCodes.Document2DCircleCreatingError;
                return null;
            }
            screwBase.EndEntityEdit();

            // 1.2 Screw base extrusion
            var extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_baseExtrusion, screwBase.Entity,
                Direction_Type.dtNormal, _kompasApp.Parameters[2]);
            // l; длина гладкой части винта;
            var screwBaseExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity);
            
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
            // Last entity in last extrusion
            var screwCarving = new KompasSketch(_kompasApp.ScrewPart, screwBaseExtrusion.ExtrudedEntity); 
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
            // основа под резьбу это 1/4 от 70 % (iт.е. это 0.525) 
            if (screwCarvingSketchEdit.ksCircle(0, 0, (_kompasApp.Parameters[0] * gostbase / 2.0), 1) == 0) // D
            {
                LastErrorCode = ErrorCodes.Document2DCircleCreatingError;
                return null;
            }
            screwCarving.EndEntityEdit();

            // 1.4 Screw base carving extrusion
            extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_baseExtrusion, screwCarving.Entity,
                Direction_Type.dtNormal, _kompasApp.Parameters[3]);
            var screwCarvingExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity);    // b 
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
            var coef = 0.7;
            var gostspin = 0.037;

            // 1.5 Screw base thread spin
            // Шаг вращения по российскому ГОСТ равен 0,037 (т.е. 3,7%) высоты вращения
            var spinParameters = new SpinParameters
            {
                Document3DPart = _kompasApp.ScrewPart,
                BeginSpinFace = carvingEntities[1],
                EndSpinFace = carvingEntities[0],
                SpinLocationPoint = new KompasPoint2D(0, 0),
                DiameterSize = _kompasApp.Parameters[0] * coef, // 0.7 D
                SpinStep = _kompasApp.Parameters[3] * gostspin //  0.037 b
            };

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
            // l + b + 3 шаг резьбы - 0.86 * D (потому что часть с 0,16 * D находится в координатах, которые меньше нуля XOZ)
            var endX = _kompasApp.Parameters[2] + _kompasApp.Parameters[3] + _kompasApp.Parameters[4] * 0.86;

            var startY = -(_kompasApp.Parameters[0] * coef / 2.0);   // 0.7 * D
            var endY = -(3.0 / 4.0 * _kompasApp.Parameters[0] * coef) / 2.0;    // 0.7 * D на конце базы

            //	Draw triangle: the base of thread. (основание резьбы - треугольник)
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