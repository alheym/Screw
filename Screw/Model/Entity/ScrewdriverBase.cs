using Kompas6API5;
using Kompas6Constants3D;
using Screw.Error;
using Screw.Model.FigureParam;
using Screw.Model.Point;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Model.Entity
{
    /// <summary>
	/// Base class for screwdriver builders.
	/// </summary>
	abstract class ScrewdriverBase
    {
        /// <summary>
        /// Last error code
        /// </summary>
        public ErrorCodes LastErrorCode = ErrorCodes.OK;

        /// <summary>
        /// Kompas application object
        /// </summary>
        protected KompasApplication _kompasApp;

        /// <summary>
        /// Build screwdriver.
        /// </summary>
        /// <returns>Screwdriver entity.</returns>
        abstract public ksEntity BuildScrewdriver();

        /// <summary>
        /// Create cutoff for flathead screwdriver
        /// </summary>
        /// <returns>Created entity of cutoff</returns>
        protected ksEntity CreateCutout(double[] parameters)
        {
            var offsetX = parameters[0];
            var offsetY = parameters[1];

            var width = parameters[2];
            var height = parameters[3];

            var gost = 0.84;

            // 1. Cutout sketch
            var rectangleSketch = new KompasSketch(_kompasApp.ScrewPart, Obj3dType.o3d_planeYOZ);
            if (rectangleSketch.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = rectangleSketch.LastErrorCode;
                return null;
            }

            var rectangleSketchEdit = rectangleSketch.BeginEntityEdit();
            if (rectangleSketchEdit == null)
            {
                LastErrorCode = rectangleSketch.LastErrorCode;
                return null;
            }

            // Center point
            var rectanglePoint = new KompasPoint2D(offsetX, offsetY);

            var rectangleParam = new RectangleParameter(_kompasApp, width, height, rectanglePoint);
            if (rectangleSketchEdit.ksRectangle(rectangleParam.FigureParam, 0) == 0)
            {
                LastErrorCode = ErrorCodes.Document2DRegPolyCreateError;
                return null;
            }

            rectangleSketch.EndEntityEdit();

            // 2. Cutout entity extrusion
            var extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_cutExtrusion, rectangleSketch.Entity,
                                                                        Direction_Type.dtNormal, _kompasApp.Parameters[1] * gost);
            var rectangleExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity); // 0.84 * m
            if (rectangleExtrusion.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = rectangleExtrusion.LastErrorCode;
                return null;
            }

            return rectangleExtrusion.ExtrudedEntity;
        }

        /// <summary>
        /// Метод построения крестообразного шлица
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected ksEntity CreateCutou(double[] parameters)
        {      
            var width = parameters[0];
            var height = parameters[1];

            var gost = 0.84;

            // 1. Cutout sketch
            var regPolySketch = new KompasSketch(_kompasApp.ScrewPart, Obj3dType.o3d_planeXOZ);
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
           
            regPolySketchEdit.ksLineSeg(0, width, 0, -width, 1);
            regPolySketchEdit.ksLineSeg(0, width, height, 0, 1);
            regPolySketchEdit.ksLineSeg(0, -width, height, 0, 1);
            regPolySketch.EndEntityEdit();

            var regPoSketch = new KompasSketch(_kompasApp.ScrewPart, Obj3dType.o3d_planeXOZ);
            var regPoSketchEdit = regPoSketch.BeginEntityEdit();

            regPoSketchEdit.ksLineSeg(0, width, 0, -width, 1);
            regPoSketchEdit.ksLineSeg(0, width, height, 0, 1);
            regPoSketchEdit.ksLineSeg(0, -width, height, 0, 1);
            regPoSketch.EndEntityEdit();


            var regPolSketch = new KompasSketch(_kompasApp.ScrewPart, Obj3dType.o3d_planeXOY);
            var regPolSketchEdit = regPolSketch.BeginEntityEdit();

            regPolSketchEdit.ksLineSeg(0, width, 0, -width, 1);
            regPolSketchEdit.ksLineSeg(0, width, height, 0, 1);
            regPolSketchEdit.ksLineSeg(0, -width, height, 0, 1);
            regPolSketch.EndEntityEdit();

            var regPSketch = new KompasSketch(_kompasApp.ScrewPart, Obj3dType.o3d_planeXOY);
            var regPSketchEdit = regPSketch.BeginEntityEdit();

            regPSketchEdit.ksLineSeg(0, width, 0, -width, 1);
            regPSketchEdit.ksLineSeg(0, width, height, 0, 1);
            regPSketchEdit.ksLineSeg(0, -width, height, 0, 1);
            regPSketch.EndEntityEdit();

            //// 2. Cutout entity extrusion
            var extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_cutExtrusion, regPolySketch.Entity,
                                                                        Direction_Type.dtReverse, _kompasApp.Parameters[1] * gost / 2);

            var regPolyExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity); // 0.84 * m

            extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_cutExtrusion, regPoSketch.Entity,
                                                                        Direction_Type.dtNormal, _kompasApp.Parameters[1] * gost / 2);

            var regPoExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity); // 0.84 * m
            
            extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_cutExtrusion, regPolSketch.Entity,
                                                                    Direction_Type.dtReverse, _kompasApp.Parameters[1] * gost / 2);
            regPolyExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity); // 0.84 * m

            extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_cutExtrusion, regPSketch.Entity,
                                                                    Direction_Type.dtNormal, _kompasApp.Parameters[1] * gost / 2);
            var regPExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity); // 0.84 * m

            return regPolyExtrusion.ExtrudedEntity;
         
        }

        /// <summary>
        /// Метод построения щлица в виде многоугольника
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected ksEntity CreateCut(double[] parameters)
        {
            var offsetX = parameters[0];
            var offsetY = parameters[1];


            var width = parameters[2];

            // 1. Cutout sketch
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

            // Center point
            var regPolyPoint = new KompasPoint2D(offsetX, offsetY);

            var regPolyParam = new RegularPolygonParameter(_kompasApp, 6, width , regPolyPoint);
            if (regPolySketchEdit.ksRegularPolygon(regPolyParam.FigureParam, 0) == 0)
            {
                LastErrorCode = ErrorCodes.Document2DRegPolyCreateError;
                return null;
            }

            regPolySketch.EndEntityEdit();

            // 2. Cutout entity extrusion
            var gost = 0.84;
            var extrusionParameters = new KompasExtrusionParameters(_kompasApp.ScrewPart, Obj3dType.o3d_cutExtrusion, regPolySketch.Entity,
                                                                        Direction_Type.dtNormal, _kompasApp.Parameters[1] * gost);
            var regPolyExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity); // 0.84 * m
            if (regPolyExtrusion.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = regPolyExtrusion.LastErrorCode;
                return null;
            }

            return regPolyExtrusion.ExtrudedEntity;  
        }   
    }
}
