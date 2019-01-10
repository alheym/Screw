using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NutScrew.Model.Entity;
using NutScrew.Validator;
using NutScrew.Error;
using Kompas6Constants3D;
using Kompas6API5;
using NutScrew.Manager;
using NutScrew.Model.FigureParam;
using NutScrew.Model.Point;

namespace NutScrew.UnitTests
{
	/// <summary>
	/// Test of "KompasPoint3D" class
	/// </summary>
	[TestFixture]
	class Point3DTest
	{
		/// <summary>
		/// Test of creating point 2D
		/// </summary>
		/// <param name="errorCode">Error code</param>
		/// <param name="xc">Coordinate X</param>
		/// <param name="yc">Coordinate OY</param>
		/// <param name="zc">Coordinate OZ</param>
		[TestCase(ErrorCodes.OK, 1.0, 1.0, 1.0, TestName = "Point3D, normal parameters")]
		[TestCase(ErrorCodes.OK, 0.0, 0.0, 0.0, TestName = "Point3D, parameters are equal to 0")]
		[TestCase(ErrorCodes.OK, -1.0, -1.0, -1.0, TestName = "Point3D, parameters are less than 0")]
		[TestCase(ErrorCodes.ArgumentInvalid, double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity, TestName = "Point3D, parameters are infinities")]
		[TestCase(ErrorCodes.ArgumentInvalid, double.NaN, double.NaN, double.NaN, TestName = "Point3D, parameters are not a numbers")]
		public void Normal(ErrorCodes errorCode, double xc, double yc, double zc)
		{
			var point3D = new KompasPoint3D(xc, yc, zc);
			Assert.AreEqual(point3D.LastErrorCode, errorCode);
		}
	}
}
