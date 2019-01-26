using Kompas6API5;
using Kompas6Constants3D;
using Screw.Model.Point;
using Screw.Error;
using Screw.Validator;

namespace Screw.Model.FigureParam
{
    /// <summary>
    /// Parameters for class  "Spin"
    /// </summary>
    public struct SpinParameters
    {
        /// <summary>
        /// Part of document with detail
        /// </summary>
        public ksPart Document3DPart;

        /// <summary>
        /// Begin face of spin
        /// </summary>
        public ksEntity BeginSpinFace;

        /// <summary>
        /// End face of spin
        /// </summary>
        public ksEntity EndSpinFace;

        /// <summary>
        /// Spin location point
        /// </summary>
        public KompasPoint2D SpinLocationPoint;

        /// <summary>
        /// Size of diameter of spin
        /// </summary>
        public double DiameterSize;

        /// <summary>
        /// Step of spin
        /// </summary>
        public double SpinStep;

        /// <summary>
        /// Spin parameters contstructor
        /// </summary>
        /// <param name="document3DPart">Part of document with detail</param>
        /// <param name="beginSpinFace">Begin face of spin</param>
        /// <param name="endSpinFace">End face of spin</param>
        /// <param name="spinLocationPoint">Spin location point</param>
        /// <param name="diameterSize">Size of diameter of spin</param>
        /// <param name="spinStep">Step of spin</param>
        public SpinParameters(ksPart document3DPart, ksEntity beginSpinFace, ksEntity endSpinFace, KompasPoint2D spinLocationPoint,
            double diameterSize, double spinStep)
        {
            Document3DPart = document3DPart;
            BeginSpinFace = beginSpinFace;
            EndSpinFace = endSpinFace;
            SpinLocationPoint = spinLocationPoint;
            DiameterSize = diameterSize;
            SpinStep = spinStep;
        }
    }
}
