using Screw.Validator;
using Screw.Error;

namespace Screw.Model.Point
{
    /// <summary>
    /// Class KompasPoint3D.
    /// Presents three-dimensional point with X, Y and Z coordinates.
    /// </summary>
    public struct KompasPoint3D
    {
        /// <summary>
        /// Last error code getter
        /// </summary>
        public ErrorCodes LastErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// X coordinate of point
        /// </summary>
        public double X
        {
            get;
            set;
        }

        /// <summary>
        /// Y coordinate of point
        /// </summary>
        public double Y
        {
            get;
            set;
        }

        /// <summary>
        /// Z coordinate of point
        /// </summary>
        public double Z
        {
            get;
            set;
        }

        /// <summary>
        /// Kompas 3D Point
        /// </summary>
        /// <param name="xc">X coordinate of point</param>
        /// <param name="yc">Y coordinate of point</param>
        /// <param name="zc">Z coordinate of point</param>
        public KompasPoint3D(double xc, double yc, double zc)
        {
            X = default(double);
            Y = default(double);
            Z = default(double);
            LastErrorCode = ErrorCodes.OK;

            if (!DoubleValidator.Validate(xc)
                || !DoubleValidator.Validate(yc)
                || !DoubleValidator.Validate(zc)
            )
            {
                LastErrorCode = ErrorCodes.ArgumentInvalid;
                return;
            }

            X = xc;
            Y = yc;
            Z = zc;
        }
    }
}
