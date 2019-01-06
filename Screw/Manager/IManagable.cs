namespace Screw.Manager
{
    /// <summary>
    /// Build manager interface
    /// </summary>
    interface IManagable
    {
        /// <summary>
        /// Create detail in Kompas application
        /// </summary>
        /// <returns>true if operation successful; false in case of error</returns>
        bool CreateDetail();
    }
}
