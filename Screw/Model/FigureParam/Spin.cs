using Kompas6API5;
using Kompas6Constants3D;
using Screw.Error;
using Screw.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Model.FigureParam
{
    /// <summary>
	/// Spin class.
	/// Presents spin parameters of 2D document.
	/// </summary>
	class Spin
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
        /// Figure param
        /// </summary>
        public ksEntity Entity
        {
            get;
            private set;
        }

        /// <summary>
        /// Spin step
        /// </summary>
        public double SpinStep
        {
            get;
            private set;
        }

        /// <summary>
        /// Spin parameter by spin faces (begin and end), spin location point, diameter size and by spin step
        /// </summary>
        /// <param name="parameters">Parameters of spin</param>
        public Spin(SpinParameters parameters)
        {
            if (parameters.BeginSpinFace == null
                || parameters.EndSpinFace == null
                || parameters.SpinLocationPoint.LastErrorCode != ErrorCodes.OK
                || parameters.DiameterSize == default(double)
                || !DoubleValidator.Validate(parameters.DiameterSize)
                || parameters.SpinStep == default(double)
                || !DoubleValidator.Validate(parameters.SpinStep)
            )
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return;
            }

            if (parameters.DiameterSize <= default(double)
                || !DoubleValidator.Validate(parameters.DiameterSize)
                || !DoubleValidator.Validate(parameters.SpinLocationPoint.X)
                || !DoubleValidator.Validate(parameters.SpinLocationPoint.Y)
            )
            {
                LastErrorCode = ErrorCodes.DoubleValueValidationError;
                return;
            }

            if (!CreateSpin(parameters))
            {
                return;
            }
        }

        /// <summary>
        /// Create spin definition by begin and end entity, diameter and spin height
        /// </summary>
        /// <param name="parameters">Spin parameters</param>
        private bool CreateSpin(SpinParameters parameters)
        {
            // Create spin entity
            var spin = (ksEntity)parameters.Document3DPart.NewEntity((short)Obj3dType.o3d_cylindricSpiral);
            if (spin == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return false;
            }

            // Create spin definition by spin entity
            var spinDefinition = (ksCylindricSpiralDefinition)spin.GetDefinition();
            if (spinDefinition == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return false;
            }

            // Set base plane of spin
            spinDefinition.SetPlane(parameters.BeginSpinFace);

            spinDefinition.buildDir = true;                 // Normal spin build directory
            spinDefinition.buildMode = 1;                   // Build by thread step and height
            spinDefinition.diamType = 0;                    // Diameter by size
            spinDefinition.diam = parameters.DiameterSize;
            spinDefinition.heightType = 1;                  // Height by object
            spinDefinition.SetHeightObject(parameters.EndSpinFace);
            spinDefinition.turnDir = true;                  // Clockwise direction
            spinDefinition.step = parameters.SpinStep;      // Spin step

            // Create sketch on document
            if (!spinDefinition.SetLocation(parameters.SpinLocationPoint.X, parameters.SpinLocationPoint.Y)
                || !spin.Create()
            )
            {
                LastErrorCode = ErrorCodes.EntityCreateError;
                return false;
            }

            Entity = spin;
            SpinStep = spinDefinition.step;

            return true;
        }
    }
}
