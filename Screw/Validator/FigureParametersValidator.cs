using System.Collections.Generic;
using System.Globalization;
using Screw.Error;

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
        ///	a) H <= m
        ///	b) l + b <= H
        ///	c) H1' >= 0.55 W3
        ///	d) D <= n
        /// e) b <= l
        /// <returns>true if validation successful; false if error is caused while validation</returns>
        public bool Validate()
        {
            var screwHatHeight = _figureParameters[4]; 
            var screwBaseWidth = _figureParameters[2] + _figureParameters[3];   // W1 + W2

            var diapasonStart = default(double);
            var diapasonEnd = default(double);
            var errorMessage = default(string);

            if (!ValidateDoubles()) return false;

            // a) H <= m
            if (!( _figureParameters[1]<=screwHatHeight))
            {
                diapasonStart = screwHatHeight;
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Значение глубины шлица m не может быть больше или равно значению высоты шляпки H", diapasonStart);
                ErrorList.Add(errorMessage);
            }
            // b) l + b <= H 
            if ((screwBaseWidth <= screwHatHeight))
            {
                diapasonStart = screwBaseWidth;
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Значение параметра H не может быть больше чем l + b", diapasonStart);
                errorMessage += "\n(Этот параметр зависит от l и b)";
                ErrorList.Add(errorMessage);
            }

            // c) D <= n
            if (_figureParameters[0] <= _figureParameters[5])
            {
                diapasonStart = _figureParameters[0];
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Значение ширины шлица n не может быть больше или равно диаметру шляпки D", diapasonStart);
                ErrorList.Add(errorMessage);
            }

            // e) b <= l
            if (!(_figureParameters[2] <= _figureParameters[3]))
            {
                diapasonStart = _figureParameters[2];
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Длина гладкой части l больше/равна длины резьбы b \n(for your parameters: {0:####.##} => b", diapasonStart);
                ErrorList.Add(errorMessage);
            }

            return ErrorList.Count == 0;
        }

        /// <summary>
        /// Проверка двойных значений в параметрах фигуры.
        /// </summary>
        /// <returns> true, если проверка прошла успешно; false, если при проверке возникла ошибка</returns>
        private bool ValidateDoubles()
        {
            foreach (double parameter in _figureParameters)
            {
                if (parameter <= 0)
                {
                    ErrorList.Add("Параметр не может принимать значение 0");
                    return false;
                }
                if (parameter < 0.1)
                {
                    ErrorList.Add("Параметр должен быть больше 0.1");
                    return false;
                }
                if (parameter >= 10000)
                {
                    ErrorList.Add("Параметр должен быть меньше 10000");
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
