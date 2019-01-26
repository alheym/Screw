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
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Значение глубины шлица m не должно быть больше или равно значению высоты шляпки H",
                    diapasonStart);
                ErrorList.Add(errorMessage);
            }
            // b) l + b <= H 
            if ((screwBaseWidth <= screwHatHeight))
            {
                diapasonStart = screwBaseWidth;
                diapasonEnd = screwHatHeight;
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Значение параметра H не должно быть больше чем l + b", diapasonStart);
                errorMessage += "\n(Этот параметр зависит от l и b)";
                ErrorList.Add(errorMessage);
            }

            // c) D <= n
            if (_figureParameters[0] <= _figureParameters[5])
            {
                diapasonStart = _figureParameters[0];
                diapasonEnd = _figureParameters[5];
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Значение ширины шлица n не должно быть больше или равно диаметру шляпки D", diapasonStart);
                ErrorList.Add(errorMessage);
            }

            // e) b <= l
            if (!(_figureParameters[2] <= _figureParameters[3]))
            {
                diapasonStart = _figureParameters[2];
                diapasonEnd = _figureParameters[3];
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Длина гладкой части l не должно быть больше длины резьбы b", diapasonStart);
                ErrorList.Add(errorMessage);
            }

            // b > 100
            if (_figureParameters[3] > 100)
            {
                diapasonStart = 100;
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Длина резьбы не должна быть больше 100", diapasonStart);
                ErrorList.Add(errorMessage);
            }

            // l > 35
            if (_figureParameters[2] > 35)
            {
                diapasonStart = 35;
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Длина гладкой части не должна быть больше 35", diapasonStart);
                ErrorList.Add(errorMessage);
            }

            // D > 45
            if (_figureParameters[0] > 45)
            {
                diapasonStart = 45;
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Диаметр шляпки не должен быть больше 45", diapasonStart);
                ErrorList.Add(errorMessage);
            }

            // H > 20
            if (_figureParameters[4] > 20)
            {
                diapasonStart = 20;
                errorMessage = string.Format(CultureInfo.InvariantCulture, "Длина шляпки не должна быть больше 20", diapasonStart);
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
                if (parameter >= 1000)
                {
                    ErrorList.Add("Параметр должен быть меньше 1000");
                }
                if (!DoubleValidator.Validate(parameter))
                {
                    ErrorList.Add("Некорректное значение парамета");
                    return false;
                }
            }

            return true;
        }

    }
}
