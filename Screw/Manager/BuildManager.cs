using System;
using Kompas6API5;
using Kompas6Constants3D;
using Kompas6Constants;
using Screw.Model.Point;
using Screw.Model.FigureParam;
using Screw.Model.Entity;
using Screw.Error;
using Screw.Validator;

namespace Screw.Manager
{
    /// <summary>
    /// Build manager.
    /// Manages creation of build with screw and nut.
    /// </summary>
    public class BuildManager : IManagable
    {
        /// <summary>
        /// Kompas application specimen
        /// </summary>
        private KompasApplication _kompasApp;

        /// <summary>
        /// Last error code
        /// </summary>
        public ErrorCodes LastErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Build Manager constructor
        /// </summary>
        public BuildManager(KompasApplication kompasApp)
        {
            if (kompasApp == null)
            {
                LastErrorCode = ErrorCodes.ArgumentNull;
                return;
            }

            _kompasApp = kompasApp;
        }

        /// <summary>
        /// Create test figure
        /// </summary>
        /// <returns>true if operation successful, false in case of error</returns>
        public bool CreateDetail()
        {
            if (!CreateScrew()) return false;

            //if (!CreateNut()) return false;

            return true;
        }

        /// <summary>
        /// Create screw with hat and base
        /// </summary>
        /// <returns>true if operation successful; false in case of error</returns>
        private bool CreateScrew()
        {
            var screwManager = new ScrewManager(_kompasApp);
            if (screwManager.LastErrorCode != ErrorCodes.OK)
            {
                LastErrorCode = screwManager.LastErrorCode;
                return false;
            }

            if (!screwManager.CreateDetail())
            {
                LastErrorCode = screwManager.LastErrorCode;
                return false;
            }

            _kompasApp.ThreadStep = screwManager.ThreadStep;

            return true;
        }
    }
}
