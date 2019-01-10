using NUnit.Framework;
using System.Collections.Generic;
using Screw.Manager;
using Screw;

namespace UnitTests
{
    /// <summary>
    /// Tests for "BuildManager" class
    /// </summary>
    [TestFixture]
    class BuildManagerTest
    {
        /// <summary>
        /// Load builder specific amount of times
        /// </summary>
        /// <param name="amountOfLoads">Amount of loads of builder</param>
        [TestCase(90, TestName = "LoadBuilder sync, 90 times (this is maximum on my notebook)")]
        public void LoadBuilder(int amountOfLoads)
        {
            var res = true;
            var parameters = new double[] { 85, 75, 125, 375, 50, 60 };

            for (int i = 1; i < amountOfLoads; i++)
            {
                CreateDetail(res, parameters);
            }
        }

        /// <summary>
        /// Detail creating test, normal parameters
        /// </summary>
        /// <param name="expected">Expected state of build: true or false</param>
        /// <param name="parameters">Parameters</param>
        [TestCase(true, 8500, 7500, 12500, 37500, 5000, 6000, TestName = "Build manager, high values     (10^^2)")]
        [TestCase(true, 850, 750, 1250, 3750, 500, 600, TestName = "Build manager, high values     (10^^1)")]
        [TestCase(true, 85, 75, 125, 375, 50, 60, TestName = "Build manager, correct values  (10)")]
        [TestCase(true, 0.85, 0.75, 1.25, 3.75, 0.50, 0.60, TestName = "Build manager, very low values (0.1)")]
        public void CreateDetail(bool expected, params double[] parameters)
        {
            var figureParameters = new List<double>();
            for (int i = 0, length = parameters.Length; i < length; i++)
            {
                figureParameters.Add(parameters[i]);
            }

            var figureParametersValidator = new FigureParametersValidator(figureParameters);
            figureParametersValidator.Validate();

            var app = new KompasApplication
            {
                Parameters = figureParameters
            };
            app.CreateDocument3D();

            var manager = new BuildManager(app);
            var result = manager.CreateDetail();

            Assert.AreEqual(result, expected);
        }

        /// <summary>
        /// Detail creating test, unnormal parameters
        /// </summary>
        /// <param name="expected">Expected code of error</param>
        /// <param name="parameters">Parameters</param>
        [TestCase(false, 100500, 33375, 128325, 37135, 52340, 60, TestName = "Build manager, incorrect values")]
        public void CreateDetailWithUnnormalParameters(bool expected, params double[] parameters)
        {
            var figureParameters = new List<double>();
            for (int i = 0, length = parameters.Length; i < length; i++)
            {
                figureParameters.Add(parameters[i]);
            }

            var figureParametersValidator = new FigureParametersValidator(figureParameters);
            Assert.False(figureParametersValidator.Validate(), "false expected");
        }
    }
}
