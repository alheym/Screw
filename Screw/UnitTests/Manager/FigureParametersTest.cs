using NUnit.Framework;
using Screw.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.UnitTests.Manager
{
    /// <summary>
	/// Test for "FigureParameters" class
	/// </summary>
	[TestFixture]
    class FigureParametersTest
    {
        /// <summary>
        /// Проверка параметров валидации
        /// </summary>
        /// <param name="expected">Ожидаемое состояние проверки: true or false</param>
        /// <param name="parameters">Parameters of figure</param>
        [TestCase(false, 270000, 50000, 2500000, 6400000, 100000, 50000, TestName = "FigureParameters, очень высокие значения  (10^^4)")]
        [TestCase(false, 27000, 5000, 250000, 640000, 10000, 5000, TestName = "FigureParameters, очень высокие значения  (10^^3)")]
        [TestCase(false, 2700, 5000, 25000, 64000, 1000, 5000, TestName = "FigureParameters, высокие значения  (10^^2)")]
        //[TestCase(true, 270, 50, 250, 640, 100, 50, TestName = "FigureParameters, нормальные значения  (10)")]
        [TestCase(true, 27, 5, 25, 64, 10, 5, TestName = "FigureParameters, нормальные значения  (1)")]
        [TestCase(true, 2.7, 0.5, 2.5, 6.4, 1.0, 0.5, TestName = "FigureParameters, очень низкие значения  (0.1)")]
        [TestCase(false, 0.27, 0.05, 0.25, 0.64, 0.10, 0.05, TestName = "FigureParameters, очень низкие значения  (0.01)")]
        [TestCase(false, 0.027, 0.005, 0.025, 0.064, 0.010, 0.005, TestName = "FigureParameters, очень низкие значения  (0.001)")]
        [TestCase(false, 0.0027, 0.0005, 0.0025, 0.0064, 0.0010, 0.0005, TestName = "FigureParameters, очень низкие значения  (0.0001)")]
        [TestCase(false, 100500, 33375, 128325, 37135, 52340, 60, TestName = "FigureParameters, неверные значения")]
        [TestCase(false, double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity, 
                    double.NegativeInfinity, double.PositiveInfinity, TestName = "FigureParameters, incorrect values")]
        [TestCase(false, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, TestName = "FigureParameters, неверные значения")]
        [TestCase(false, double.MaxValue + 1, double.MinValue - 1, double.MinValue - 1, double.MaxValue + 1, double.MaxValue + 1,
                    double.MinValue - 1, TestName = "FigureParameters, max and min values")]
        [TestCase(false, null, null, null, null, null, null, TestName = "FigureParameters, нулевые значения")]
        public void TestValidation(bool expected, params double[] parameters)
        {
            var figureParameters = new List<double>();
            for (int i = 0, length = parameters.Length; i < length; i++)
            {
                figureParameters.Add(parameters[i]);
            }

            var validator = new FigureParametersValidator(figureParameters);
            Assert.AreEqual(validator.Validate(), expected);
        }
    }
}
