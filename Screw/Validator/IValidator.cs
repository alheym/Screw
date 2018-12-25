using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Validator
{
    /// <summary>
	/// Validator interface for classes which are validating parameters.
	/// </summary>
	interface IValidator
    {
        /// <summary>
        /// Validate parameter
        /// </summary>
        /// <returns>true in case of successful validation; false in other case</returns>
        bool Validate();
    }
}
