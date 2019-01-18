using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Kompas6API5;
using Kompas6Constants3D;
using Kompas6Constants;
using Screw;
using Screw.Error;
using Screw.Validator;

namespace Screw
{
    /// <summary>
    /// Kompas application manager class
    /// </summary>
    public class KompasApplication
    {
        /// <summary>
        /// Get Kompas 3D object
        /// </summary>
        public KompasObject KompasObject
        {
            get;
            private set;
        }

        /// <summary>
        /// 3D document of build
        /// </summary>
        public ksDocument3D Document3D
        {
            get;
            private set;
        }

        /// <summary>
        /// Screw part of build
        /// </summary>
        public ksPart ScrewPart
        {
            get;
            private set;
        }

        /// <summary>
        /// Step of thread of figures in build
        /// </summary>
        public double ThreadStep
        {
            get;
            set;
        }

        /// <summary>
        /// Parameters of figures in build
        /// </summary>
        public List<double> Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// Last error code
        /// </summary>
        public ErrorCodes LastErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Create object of application
        /// </summary>
        public KompasApplication()
        {
            if (!GetActiveApp())
            {
                if (!CreateNewApp())
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Create 3D document
        /// </summary>
        /// <returns>true if operation successful; false in case of error</returns>
        public bool CreateDocument3D()
        {
            Document3D = (ksDocument3D)KompasObject.Document3D();

            // Create build
            if (!Document3D.Create(false/*visible*/, false/*build*/))
            {
                LastErrorCode = ErrorCodes.Document3DCreateError;
                return false;
            }

            // Create screw detail on 3D document
            ScrewPart = (ksPart)Document3D.GetPart((short)Part_Type.pTop_Part);

            if (ScrewPart == null)
            {
                LastErrorCode = ErrorCodes.Document3DGetPartError;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get already active Kompas application
        /// </summary>
        /// <returns>true if operation successful, false in case of error</returns>
        private bool GetActiveApp()
        {
            // Try to get an active app
            if (KompasObject == null)
            {
                try
                {
                    KompasObject = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
                }
                catch
                {
                    return false;
                }
            }

            // Else application not loaded -- return
            if (KompasObject == null)
            {
                return false;
            }

            KompasObject.Visible = true;
            KompasObject.ActivateControllerAPI();

            return true;
        }

        /// <summary>
        /// Create new Kompas application
        /// </summary>
        /// <returns>true if operation successful, false in case of error</returns>
        private bool CreateNewApp()
        {
            Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
            KompasObject = (KompasObject)Activator.CreateInstance(t);

            if (KompasObject == null)
            {
                LastErrorCode = ErrorCodes.KompasApplicationCreatingError;
                return false;
            }

            KompasObject.Visible = true;
            KompasObject.ActivateControllerAPI();

            return true;
        }

        /// <summary>
        /// Unset object of program
        /// </summary>
        public void DestructApp()
        {
            KompasObject.Quit();
            KompasObject = null;

            Document3D = null;
            ScrewPart = null;

            LastErrorCode = ErrorCodes.OK;
        }
    }
}
