using Kompas6API5;
using Kompas6Constants3D;
using NUnit.Framework;
using Screw.Error;
using Screw.Model.Entity;
using Screw.UnitTests.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.UnitTests.Model.Entity
{
    /// <summary>
	/// Test "KompasSketch" class
	/// </summary>
	[TestFixture]
    class KompasSketchTest
    {
        /// <summary>
        /// Тест создания эскиза на оси базовой плоскости
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="basePlaneAxis">Ось базовой плоскости, экземпляр Obj3dType</param>
        [TestCase(ErrorCodes.OK, Obj3dType.o3d_planeXOY, TestName = "KompasSketch на оси базовой плоскости = Obj3dType.o3d_planeXOY")]
        [TestCase(ErrorCodes.OK, Obj3dType.o3d_planeXOZ, TestName = "KompasSketch на оси базовой плоскости = Obj3dType.o3d_planeXOZ")]
        [TestCase(ErrorCodes.OK, Obj3dType.o3d_planeYOZ, TestName = "KompasSketch на оси базовой плоскости = Obj3dType.o3d_planeYOZ")]
        [TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_axisOX, TestName = "KompasSketch на оси базовой плоскости = Obj3dType.o3d_axisOX")]
        [TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_axisOY, TestName = "KompasSketch на оси базовой плоскости = Obj3dType.o3d_axisOY")]
        [TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_axisOZ, TestName = "KompasSketch на оси базовой плоскости = Obj3dType.o3d_axisOZ")]
        [TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_unknown, TestName = "KompasSketch на оси базовой плоскости = Obj3dType.o3d_unknown")]
        [TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_face, TestName = "KompasSketch на оси базовой плоскости = Obj3dType.o3d_face")]
        [TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_edge, TestName = "KompasSketch на оси базовой плоскости = Obj3dType.o3d_edge")]
        public void CreateOnBasePlaneAxis(ErrorCodes errorCode, Obj3dType basePlaneAxis)
        {
            var appTest = new KompasApplicationTest();
            var app = appTest.CreateKompasApplication();

            var sketch = new KompasSketch(app.ScrewPart, basePlaneAxis);
            Assert.AreEqual(sketch.LastErrorCode, errorCode);
        }

        /// <summary>
        /// Тест создания эскиза на базовой плоскости = null
        /// </summary>
        /// <param name="errorCode">Error code</param>
        [TestCase(ErrorCodes.ArgumentNull, null, TestName = "KompasSketch на базовой плоскости = null")]
        public void CreateOnNull(ErrorCodes errorCode, ksEntity entity)
        {
            var appTest = new KompasApplicationTest();
            var app = appTest.CreateKompasApplication();

            var sketch = new KompasSketch(app.ScrewPart, entity);
            Assert.AreEqual(sketch.LastErrorCode, errorCode);
        }
    }
}
