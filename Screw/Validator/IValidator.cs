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
