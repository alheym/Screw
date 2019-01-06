using Kompas6API5;
using Kompas6Constants;
using Screw.Model.Point;
using Screw.Error;
using Screw.Validator;

namespace Screw.Model.FigureParam
{
    /// <summary>
    /// Rectangle parameter.
    /// Represents parameters of rectangle of 2D document.
    /// </summary>
    public class RectangleParameter
    {
        /// <summary>
        /// Kompas application
        /// </summary>
        private KompasApplication _kompasApp;

        /// <summary>
        /// Rectangle parameters
        /// </summary>
        private ksRectangleParam _rectangleParam;

        /// <summary>
        /// Last error code
        /// </summary>
        private ErrorCodes _lastErrorCode = ErrorCodes.OK;

        /// <summary>
        /// Get rectangle parameter
        /// </summary>
        public ksRectangleParam FigureParam
        {
            get { return _rectangleParam; }
        }

        /// <summary>
        /// Last error code getter
        /// </summary>
        public ErrorCodes LastErrorCode
        {
            get { return _lastErrorCode; }
        }

        /// <summary>
        /// Set rectangle param
        /// </summary>
        /// <param name="kompas">KompasObject</param>
        /// <param name="width">Rectangle width</param>
        /// <param name="height">Rectangle height</param>
        /// <param name="point2D">2D point of rectangle position on sketch</param>
        public RectangleParameter(KompasApplication kompasApp, double width, double height, KompasPoint2D point2D)
        {
            if (kompasApp == null
                || point2D.LastErrorCode != ErrorCodes.OK
            )
            {
                _lastErrorCode = ErrorCodes.ArgumentNull;
                return;
            }

            if (width <= 0.0
                || !DoubleValidator.Validate(width)
                || height <= 0.0
                || !DoubleValidator.Validate(height)
                || !DoubleValidator.Validate(point2D.X)
                || !DoubleValidator.Validate(point2D.Y)
            )
            {
                _lastErrorCode = ErrorCodes.ArgumentInvalid;
                return;
            }


            ksRectangleParam rectangleParam;

            rectangleParam = kompasApp.KompasObject.GetParamStruct((short)StructType2DEnum.ko_RectangleParam);
            rectangleParam.width = width;
            rectangleParam.height = height;
            rectangleParam.ang = 0;
            rectangleParam.style = 1;           // Line style
            rectangleParam.x = point2D.X;
            rectangleParam.y = point2D.Y;

            if (rectangleParam == null)
            {
                _lastErrorCode = ErrorCodes.EntityDefinitionNull;
                return;
            }
            _rectangleParam = rectangleParam;
        }
    }
}
