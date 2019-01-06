using System;
using Screw.Error;
using Screw.Validator;

namespace Screw.Model.Point
{
    /// <summary>
    /// Class KompasPoint2D.
    /// Presents two-dimensional point with X and Y coordinates.
    /// </summary>
    public struct KompasPoint2D 
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
        /// Kompas 2D Point
        /// </summary>
        /// <param name="xc">X coordinate</param>
        /// <param name="yc">Y coordinate</param>
        public KompasPoint2D(double xc, double yc)
        {
            X = default(double);
            Y = default(double);
            LastErrorCode = ErrorCodes.OK;

            if (!DoubleValidator.Validate(xc)
                || !DoubleValidator.Validate(yc)
            )
            {
                LastErrorCode = ErrorCodes.ArgumentInvalid;
                return;
            }

            X = xc;
            Y = yc;
        }

    }
}
