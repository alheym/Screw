using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Screw.Model.Entity;
using Screw.Validator;
using Screw.Model.FigureParam;
using Screw.Error;
using Kompas6Constants3D;
using Kompas6API5;
using Screw.Manager;

namespace NutScrew.UnitTests
{
	/// <summary>
	/// Test of class "KompasExtrusion"
	/// </summary>
	[TestFixture]
	class KompasExtrusionTest
	{
		/// <summary>
		/// Test of creating base extrusion by sketch
		/// </summary>
		/// <param name="errorCode">Error code</param>
		/// <param name="extrusionType">Extrusion type, an instance of Obj3dType</param>
		/// <param name="directionType">Direction type, an instance of Direction_Type</param>
		[TestCase(ErrorCodes.OK, Obj3dType.o3d_baseExtrusion, Direction_Type.dtNormal, TestName = "KompasExtrusion by sketch, type = baseExtrusion, normal direction")]
		[TestCase(ErrorCodes.OK, Obj3dType.o3d_baseExtrusion, Direction_Type.dtReverse, TestName = "KompasExtrusion by sketch, type = baseExtrusion, reverse direction")]
		[TestCase(ErrorCodes.ExtrusionDirectionNotSupported, Obj3dType.o3d_baseExtrusion, Direction_Type.dtMiddlePlane, TestName = "KompasExtrusion by sketch, type = baseExtrusion, middle plane direction (not supported)")]
		[TestCase(ErrorCodes.ExtrusionDirectionNotSupported, Obj3dType.o3d_baseExtrusion, Direction_Type.dtBoth, TestName = "KompasExtrusion by sketch, type = baseExtrusion, direction = both (not supported)")]
		[TestCase(ErrorCodes.ExtrusionTypeCurrentlyNotSupported, Obj3dType.o3d_axisOZ, Direction_Type.dtReverse, TestName = "KompasExtrusion by sketch, type = axisOZ (incorrect)")]
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
		/// Test of creating cut extrusion by sketch
		/// </summary>
		/// <param name="errorCode">Error code</param>
		/// <param name="extrusionType">Extrusion type, an instance of Obj3dType</param>
		/// <param name="directionType">Direction type, an instance of Direction_Type</param>
		[TestCase(ErrorCodes.OK, Obj3dType.o3d_cutExtrusion, Direction_Type.dtNormal, TestName = "KompasExtrusion by sketch, type = cutExtrusion, normal direction")]
		[TestCase(ErrorCodes.OK, Obj3dType.o3d_cutExtrusion, Direction_Type.dtReverse, TestName = "KompasExtrusion by sketch, type = cutExtrusion, reverse direction")]
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
		/// Test of creating cut extrusion by sketch with not supported direction
		/// </summary>
		/// <param name="errorCode">Error code</param>
		/// <param name="extrusionType">Extrusion type, an instance of Obj3dType</param>
		/// <param name="directionType">Direction type, an instance of Direction_Type</param>
		[TestCase(ErrorCodes.ExtrusionDirectionNotSupported, Obj3dType.o3d_cutExtrusion, Direction_Type.dtMiddlePlane, TestName = "KompasExtrusion by sketch, type = cutExtrusion, middle plane direction (not supported)")]
		[TestCase(ErrorCodes.ExtrusionDirectionNotSupported, Obj3dType.o3d_cutExtrusion, Direction_Type.dtBoth, TestName = "KompasExtrusion by sketch, type = cutExtrusion, direction = both (not supported)")]
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
		/// Create kompas sketch with circle inside
		/// </summary>
		/// <param name="app">Kompas application</param>
		/// <returns>Kompas sketch with circle</returns>
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
