using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Validator
{
    /// <summary>
	/// Validator of parameters of figures of build
	/// </summary>
	class FigureParametersValidator : IValidator
    {
        /*	Elements of chain: 
			ScrewHatWidth			- W3
			ScrewHatInnerDiameter	- d = H1
			
			ScrewHatHeight			- H1'
			ScrewHatChamferHeight	- H2 (count depending on H1')

			ScrewBaseSmoothPart		- W1
			ScrewBaseThreadPart		- W2
			NutHeight				- H
			NutThreadDiameter		- D
		*/

        /// <summary>
        /// Figure parameters
        /// </summary>
        private List<double> _figureParameters;

        /// <summary>
        /// List with errors
        /// </summary>
        public List<string> ErrorList
        {
            get;
            private set;
        }

        /// <summary>
        /// Last error code
        /// </summary>
        public ErrorCodes LastErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Figure parameters constructor
        /// </summary>
        /// <param name="parameters">List of figure parameters</param>
        public FigureParametersValidator(List<double> parameters)
        {
            ErrorList = new List<string>() { };

            if (parameters.Count != 6)
            {
                LastErrorCode = ErrorCodes.ArgumentInvalid;
                return;
            }

            _figureParameters = parameters;
        }

        /// <summary>
        /// Validate all chain by set of rules
        /// </summary>
        /// Every property must be checked by set of checking rules:
        ///	a) 0.8 W3 <= d <= 0.96 W3
        ///	b) 0.6 W3 <= D <= 0.8 W3
        ///	c) H1' >= 0.55 W3
        ///	d) 0.1 W1+W2 <= H1' <= 0.15 W1+W2
        /// e) d > D
        /// <returns>true if validation successful; false if error is caused while validation</returns>
        public bool Validate()
        {
            var screwHatHeight = _figureParameters[4]; // screw hat height is equal to nut height
            var screwBaseWidth = _figureParameters[2] + _figureParameters[3];   // W1 + W2

            var diapasonStart = default(double);
            var diapasonEnd = default(double);
            var errorMessage = default(string);

            if (!ValidateDoubles()) return false;

            // a) 0.8 W3 <= d <= 0.96 W3
            if (!((0.8 * _figureParameters[0] <= _figureParameters[1]) && (_figureParameters[1] <= 0.96 * _figureParameters[0])))
            {
                diapasonStart = 0.8 * _figureParameters[0];
                diapasonEnd = 0.96 * _figureParameters[0];
                errorMessage = string.Format(CultureInfo.InvariantCulture, "0.8 W3 <= d <= 0.96 W3 \n(for your parameters: {0:####.##} <= d <= {1:####.##})", diapasonStart, diapasonEnd);
                ErrorList.Add(errorMessage);
            }

            // b) 0.6 W3 <= D <= 0.8 W3
            if (!((0.6 * _figureParameters[0] <= _figureParameters[5]) && (_figureParameters[5] <= 0.8 * _figureParameters[0])))
            {
                diapasonStart = 0.6 * _figureParameters[0];
                diapasonEnd = 0.8 * _figureParameters[0];
                errorMessage = string.Format(CultureInfo.InvariantCulture, "0.6 W3 <= D <= 0.8 W3 \n(for your parameters: {0:####.##} <= D <= {1:####.##})", diapasonStart, diapasonEnd);
                ErrorList.Add(errorMessage);
            }

            // d) 0.1 W1+W2 <= H <= 0.15 W1+W2 and H >= 0.55 W3
            if (!((0.1 * screwBaseWidth <= screwHatHeight) && (screwHatHeight <= 0.15 * screwBaseWidth) && (screwHatHeight >= 0.55 * _figureParameters[0])))
            {
                diapasonStart = 0.1 * screwBaseWidth;
                diapasonEnd = 0.15 * screwBaseWidth;
                errorMessage = string.Format(CultureInfo.InvariantCulture, "0.1 W1+W2 <= H <= 0.15 W1+W2 and H >= 0.55 W3 \n(for your parameters: {0:####.##} <= H <= {1:####.##} and H >= {2:####.##})", diapasonStart, diapasonEnd, 0.55 * _figureParameters[0]);
                errorMessage += "\n(this parameters depends on W1 and W2)";
                ErrorList.Add(errorMessage);
            }

            // e) 0.1 W1+W2 <= W2
            if (!((0.1 * screwBaseWidth <= _figureParameters[3])))
            {
                diapasonStart = 0.1 * screwBaseWidth;
                errorMessage = string.Format(CultureInfo.InvariantCulture, "0.1 W1+W2 <= W2 \n(for your parameters: {0:####.##} <= W2)", diapasonStart);
                ErrorList.Add(errorMessage);
            }

            // f) d > D
            if (!(_figureParameters[1] > _figureParameters[5]))
            {
                diapasonStart = _figureParameters[1];
                diapasonEnd = _figureParameters[5];
                errorMessage = string.Format(CultureInfo.InvariantCulture, "d > D \n(for your parameters: {0:####.##} > {1:####.##})", diapasonStart, diapasonEnd);
                ErrorList.Add(errorMessage);
            }

            return ErrorList.Count == 0;
        }

        /// <summary>
        /// Validate double values in figure parameters.
        /// </summary>
        /// <returns>true if validation successful; false if error is caused while validation</returns>
        private bool ValidateDoubles()
        {
            foreach (double parameter in _figureParameters)
            {
                if (parameter <= 0)
                {
                    ErrorList.Add("Parameter must not be less or equal to zero");
                    return false;
                }
                if (parameter < 0.1)
                {
                    ErrorList.Add("Parameter must be greater than 0.1");
                    return false;
                }
                if (parameter >= 10000)
                {
                    ErrorList.Add("Parameter must be lower or equal to 10000");
                }
                if (!DoubleValidator.Validate(parameter))
                {
                    ErrorList.Add("Parameter is not a correct double value");
                    return false;
                }
            }

            return true;
        }

    }
}
