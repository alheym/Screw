using System;

namespace Screw.Validator
{
    /// <summary>
    /// Validator of double values.
    /// </summary>
    static class DoubleValidator
    {
        /// <summary>
        /// Validate double value.
        /// </summary>
        /// <param name="value">Double value that will be validated</param>
        /// <returns>true in case of successful validation; false in other case</returns>
        public static bool Validate(double value)
        {
            if (value < Double.MinValue
                || value > Double.MaxValue
                || Double.IsInfinity(value)
                || Double.IsNaN(value)
            )
            {
                return false;   // fail
            }

            return true;    // pass
        }
    }
}
