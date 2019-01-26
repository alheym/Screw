using Kompas6API5;
using Kompas6Constants3D;
using NUnit.Framework;
using Screw.Error;
using Screw.Model.Entity;
using Screw.Model.FigureParam;
using Screw.UnitTests.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.UnitTests.Model.Entity
{
    /// <summary>
	/// Test of class "KompasExtrusion"
	/// </summary>
	[TestFixture]
    class KompasExtrusionTest
    {
        /// <summary>
        /// Тест создания базового extrusion по эскизу
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="extrusionType">Extrusion type, экземпляр Obj3dType</param>
        /// <param name="directionType">Direction type, экземпляр Direction_Type</param>
        [TestCase(ErrorCodes.OK, Obj3dType.o3d_baseExtrusion, Direction_Type.dtNormal, TestName = "KompasExtrusion by sketch, type = baseExtrusion, нормальное направление")]
        [TestCase(ErrorCodes.OK, Obj3dType.o3d_baseExtrusion, Direction_Type.dtReverse, TestName = "KompasExtrusion by sketch, type = baseExtrusion, обратное направление")]
        [TestCase(ErrorCodes.ExtrusionDirectionNotSupported, Obj3dType.o3d_baseExtrusion, Direction_Type.dtMiddlePlane, TestName = "KompasExtrusion по эскизу, тип = baseExtrusion, направление средней плоскости (не поддерживается)")]
        [TestCase(ErrorCodes.ExtrusionDirectionNotSupported, Obj3dType.o3d_baseExtrusion, Direction_Type.dtBoth, TestName = "KompasExtrusion по эскизу, тип = baseExtrusion, направление = оба (не поддерживается)")]
        [TestCase(ErrorCodes.ExtrusionTypeCurrentlyNotSupported, Obj3dType.o3d_axisOZ, Direction_Type.dtReverse, TestName = "KompasExtrusion по эскизу, тип = axisOZ (incorrect)")]
        public void TestBaseBySketch(ErrorCodes errorCode, Obj3dType extrusionType, Direction_Type directionType)
        {
            var appTest = new KompasApplicationTest();
            var app = appTest.CreateKompasApplication();
            var sketch = CreateSketchWithCirle(app);

            var extrusionParameters = new KompasExtrusionParameters(app.ScrewPart, extrusionType, sketch, directionType, 10);
            var extrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity);
            Assert.AreEqual(extrusion.LastErrorCode, errorCode);
        }

        /// <summary>
        /// Тест создания разреза по эскизу
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="extrusionType">Extrusion type, экземпляр Obj3dType</param>
        /// <param name="directionType">Direction type, экземпляр Direction_Type</param>
        [TestCase(ErrorCodes.OK, Obj3dType.o3d_cutExtrusion, Direction_Type.dtNormal, TestName = "KompasExtrusion по эскизу, тип = cutExtrusion, нормальное направление")]
        [TestCase(ErrorCodes.OK, Obj3dType.o3d_cutExtrusion, Direction_Type.dtReverse, TestName = "KompasExtrusion по эскизу, тип = cutExtrusion, обратное направление")]
        public void TestCutBySketch(ErrorCodes errorCode, Obj3dType extrusionType, Direction_Type directionType)
        {
            var appTest = new KompasApplicationTest();
            var app = appTest.CreateKompasApplication();
            var sketch = CreateSketchWithCirle(app);

            var extrusionParameters = new KompasExtrusionParameters(app.ScrewPart, Obj3dType.o3d_baseExtrusion, sketch, directionType, 10);
            var extrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity);
            Assert.AreEqual(extrusion.LastErrorCode, errorCode);

            Direction_Type cutExtrusionDirection = Direction_Type.dtBoth;
            if (directionType == Direction_Type.dtNormal)
            {
                cutExtrusionDirection = Direction_Type.dtReverse;
            }
            else if (directionType == Direction_Type.dtReverse)
            {
                cutExtrusionDirection = Direction_Type.dtNormal;
            }

            extrusionParameters = new KompasExtrusionParameters(app.ScrewPart, extrusionType, sketch, cutExtrusionDirection, 10);
            var cutExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity);
            Assert.AreEqual(cutExtrusion.LastErrorCode, errorCode);
        }

        /// <summary>
        /// Тест создания разреза по эскизу с не поддерживаемым направлением
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="extrusionType">Extrusion type, экземпляр Obj3dType</param>
        /// <param name="directionType">Direction type, экземпляр Direction_Type</param>
        [TestCase(ErrorCodes.ExtrusionDirectionNotSupported, Obj3dType.o3d_cutExtrusion, Direction_Type.dtMiddlePlane, TestName =
            "KompasExtrusion по эскизу, тип = cutExtrusion, направление средней плоскости (не поддерживается)")]
        [TestCase(ErrorCodes.ExtrusionDirectionNotSupported, Obj3dType.o3d_cutExtrusion, Direction_Type.dtBoth, TestName = 
            "KompasExtrusion по эскизу, тип = cutExtrusion, направление = оба (не поддерживается)")]
        public void CutBySketchUnsupportedDirection(ErrorCodes errorCode, Obj3dType extrusionType, Direction_Type directionType)
        {
            var appTest = new KompasApplicationTest();
            var app = appTest.CreateKompasApplication();
            var sketch = CreateSketchWithCirle(app);

            var extrusionParameters = new KompasExtrusionParameters(app.ScrewPart, Obj3dType.o3d_baseExtrusion, sketch, directionType, 10);
            var extrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity);
            Assert.AreEqual(extrusion.LastErrorCode, errorCode);
        }

        /// <summary>
        /// Создать эскиз компаса с кругом внутри
        /// </summary>
        /// <param name="app">Kompas application</param>
        /// <returns>Компас эскиз с кругом</returns>
        public ksEntity CreateSketchWithCirle(KompasApplication app, int circleRadius = 10)
        {
            var sketch = new KompasSketch(app.ScrewPart, Obj3dType.o3d_planeXOY);
            var sketchEdit = sketch.BeginEntityEdit();

            sketchEdit.ksCircle(0.0, 0.0, circleRadius, 1);

            sketch.EndEntityEdit();

            return sketch.Entity;
        }
    }
}
