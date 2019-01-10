using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Screw.Model.Entity;
using Screw.Validator;
using Screw.Error;
using Kompas6Constants3D;
using Kompas6API5;

namespace Screw.UnitTests.Model.Entity
{
	/// <summary>
	/// Test "KompasSketch" class
	/// </summary>
	[TestFixture]
	class KompasSketchTest
	{
		/// <summary>
		/// Test of creating sketch on base plane axis
		/// </summary>
		/// <param name="errorCode">Error code</param>
		/// <param name="basePlaneAxis">Base plane axis, an instance of Obj3dType</param>
		[TestCase(ErrorCodes.OK, Obj3dType.o3d_planeXOY, TestName = "KompasSketch on base plane axis = Obj3dType.o3d_planeXOY")]
		[TestCase(ErrorCodes.OK, Obj3dType.o3d_planeXOZ, TestName = "KompasSketch on base plane axis = Obj3dType.o3d_planeXOZ")]
		[TestCase(ErrorCodes.OK, Obj3dType.o3d_planeYOZ, TestName = "KompasSketch on base plane axis = Obj3dType.o3d_planeYOZ")]
		[TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_axisOX, TestName = "KompasSketch on base plane axis = Obj3dType.o3d_axisOX")]
		[TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_axisOY, TestName = "KompasSketch on base plane axis = Obj3dType.o3d_axisOY")]
		[TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_axisOZ, TestName = "KompasSketch on base plane axis = Obj3dType.o3d_axisOZ")]
		[TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_unknown, TestName = "KompasSketch on base plane axis = Obj3dType.o3d_unknown")]
		[TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_face, TestName = "KompasSketch on base plane axis = Obj3dType.o3d_face")]
		[TestCase(ErrorCodes.ArgumentInvalid, Obj3dType.o3d_edge, TestName = "KompasSketch on base plane axis = Obj3dType.o3d_edge")]
		public void CreateOnBasePlaneAxis(ErrorCodes errorCode, Obj3dType basePlaneAxis)
		{
			var appTest = new KompasApplicationTest();
			var app = appTest.CreateKompasApplication();

			var sketch = new KompasSketch(app.ScrewPart, basePlaneAxis);
			Assert.AreEqual(sketch.LastErrorCode, errorCode);
		}

		/// <summary>
		/// Test of creating sketch on base plane = null
		/// </summary>
		/// <param name="errorCode">Error code</param>
		[TestCase(ErrorCodes.ArgumentNull, null, TestName = "KompasSketch on base plane = null")]
		public void CreateOnNull(ErrorCodes errorCode, ksEntity entity)
		{
			var appTest = new KompasApplicationTest();
			var app = appTest.CreateKompasApplication();

			var sketch = new KompasSketch(app.ScrewPart, entity);
			Assert.AreEqual(sketch.LastErrorCode, errorCode);
		}
	}
}
