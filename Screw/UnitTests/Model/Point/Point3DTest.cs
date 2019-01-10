using Kompas6API5;
using Kompas6Constants3D;
using NUnit.Framework;
using Screw.Error;
using Screw.Model.Point;

namespace Screw.UnitTests.Model.Point
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
        [TestCase(ErrorCodes.OK, 1.0, 1.0, 1.0, TestName = "Point3D, нормальные параметры")]
        [TestCase(ErrorCodes.OK, 0.0, 0.0, 0.0, TestName = "Point3D, параметры равны 0")]
        [TestCase(ErrorCodes.OK, -1.0, -1.0, -1.0, TestName = "Point3D, параметры меньше чем 0")]
        [TestCase(ErrorCodes.ArgumentInvalid, double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity, TestName = "Point3D, параметры бесконечности")]
        [TestCase(ErrorCodes.ArgumentInvalid, double.NaN, double.NaN, double.NaN, TestName = "Point3D, параметры не числа")]
        public void Normal(ErrorCodes errorCode, double xc, double yc, double zc)
        {
            var point3D = new KompasPoint3D(xc, yc, zc);
            Assert.AreEqual(point3D.LastErrorCode, errorCode);
        }
    }
}
