using Kompas6API5;
using Kompas6Constants;
using Screw.Error;
using Screw.Manager;
using Screw.Model.Point;
using Screw.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Model.FigureParam
{
    /// <summary>
	/// Regular polygon parameter.
	/// Represents parameters of regular polygon of 2D document.
	/// </summary>
	public class RegularPolygonParameter
    {
        /// <summary>
        /// Last error code
        /// </summary>
        public ErrorCodes LastErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Object with parameters of regular polygon
        /// </summary>
        public ksRegularPolygonParam FigureParam
        {
            get;
            private set;
        }

        /// <summary>
        /// Get regular polygon param
        /// </summary>
        /// <param name="_kompas">Kompas object</param>
        /// <param name="anglesCount">Angles count</param>
        /// <param name="inscribedCircleRadius">Inscribed circle radius</param>
        /// <param name="point2D">Two-dimensional point of figure base</param>
        public RegularPolygonParameter(KompasApplication kompasApp, int anglesCount,
                                            double inscribedCircleRadius, KompasPoint2D point2D)
        {
            if (kompasApp == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return;
            }

            // Angles amount must be from 3 to 12
            if (anglesCount <= 2
                || anglesCount >= 13
                || !DoubleValidator.Validate(anglesCount)
                || inscribedCircleRadius <= 0.0
                || !DoubleValidator.Validate(inscribedCircleRadius)
                || !DoubleValidator.Validate(point2D.X)
                || !DoubleValidator.Validate(point2D.Y)
            )
            {
                LastErrorCode = ErrorCodes.ArgumentInvalid;
                return;
            }

            // Regular polygon is base of hat
            ksRegularPolygonParam polyParam;
            polyParam = kompasApp.KompasObject.GetParamStruct((short)StructType2DEnum.ko_RegularPolygonParam);

            if (polyParam == null)
            {
                LastErrorCode = ErrorCodes.EntityDefinitionNull;
                return;
            }

            polyParam.count = anglesCount;                      // Count of angles
            polyParam.ang = 0;                                  // Radius-vector angle from center to first top
            polyParam.describe = true;                          // The polygon is DEscribed
            polyParam.radius = inscribedCircleRadius;           // INscribed circle radius, / W3 /
            polyParam.style = 1;                                // Line style
            polyParam.xc = point2D.X; polyParam.yc = point2D.Y; // Plane coordinates

            FigureParam = polyParam;
        }
    }
}
