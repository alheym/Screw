using Kompas6API5;
using Kompas6Constants3D;
using NUnit.Framework;
using Screw.Error;
using Screw.Model.Point;

namespace Screw.UnitTests.Model.Point
{
    /// <summary>
	/// Test of "KompasPoint2D" class
	/// </summary>
	[TestFixture]
    class Point2DTest
    {
        /// <summary>
        ///Тест создания point 2D
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="xc">Coordinate X</param>
        /// <param name="yc">Coordinate OY</param>
        [TestCase(ErrorCodes.OK, 1.0, 1.0, TestName = "Point2D, нормальные параметры")]
        [TestCase(ErrorCodes.OK, 0.0, 0.0, TestName = "Point2D, параметры равны 0")]
        [TestCase(ErrorCodes.OK, -1.0, -1.0, TestName = "Point2D, параметры меньше чем 0")]
        [TestCase(ErrorCodes.ArgumentInvalid, double.PositiveInfinity, double.NegativeInfinity, TestName = "Point2D, параметры бесконечности")]
        [TestCase(ErrorCodes.ArgumentInvalid, double.NaN, double.NaN, TestName = "Point2D, параметры не числа")]
        public void Normal(ErrorCodes errorCode, double xc, double yc)
        {
            var point2D = new KompasPoint2D(xc, yc);
            Assert.AreEqual(point2D.LastErrorCode, errorCode);
        }
    }
}
