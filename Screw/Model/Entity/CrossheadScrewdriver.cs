using Kompas6API5;
using Kompas6Constants3D;
using Screw.Error;
using Screw.Model.FigureParam;
using Screw.Model.Point;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Model.Entity
{
    /// <summary>
	/// Crosshead screwdriver.
	/// </summary>
	class CrossheadScrewdriver : ScrewdriverBase
    {
        /// <summary>
        /// Screwdriver builder.
        /// </summary>
        /// <param name="kompasApp">Kompas application object</param>
        public CrossheadScrewdriver(KompasApplication kompasApp)
        {
            _kompasApp = kompasApp;
        }

        /// <summary>
        /// Builds flathead screwdriver.
        /// </summary>
        /// <returns>Screwdriver entity</returns>
        public override ksEntity BuildScrewdriver()
        {
            var D = _kompasApp.Parameters[0];
            var H = _kompasApp.Parameters[5];


            var offsetX = -0.35 * D;
            var offsetY = -0.4 * H;

            var width = 0.7 * D;
            var height = 0.8 * H;

            var parameters = new double[4] { offsetX, offsetY, width, height };

            var entity = CreateCutout(parameters);
            if (entity == null)
            {
                return null;
            }

            // Reverse parameters to build crosshead rectangle
            parameters = new double[4] { offsetY, offsetX, height, width };

            entity = CreateCutout(parameters);
            if (entity == null)
            {
                return null;
            }

            return entity;


        }
    }
    
}
