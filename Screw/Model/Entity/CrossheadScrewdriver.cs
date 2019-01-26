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
        /// Builds crosshead screwdriver.
        /// </summary>
        /// <returns>Screwdriver entity</returns>
        public override ksEntity BuildScrewdriver()
        {

            var Diameter = _kompasApp.Parameters[0];
            var Hate = _kompasApp.Parameters[5];

            var width = 0.3 * Diameter;
            var height = 0.95 * Hate;

            var parameters = new double[2] { width, height };
          
            var entity = CreateCutou(parameters);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }
     }
    
}
