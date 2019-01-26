using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants3D;
using Screw.Error;
using Screw.Model.FigureParam;
using Screw.Model.Point;

namespace Screw.Model.Entity
{
    /// <summary>
    /// Regular Polygon screwdriver.
    /// </summary>
    class RegularPolygonScrewdriver : ScrewdriverBase
    {
        /// <summary>
        ///  Regular Polygon builder.
        /// </summary>
        /// <param name="kompasApp">Kompas application object</param>
        public RegularPolygonScrewdriver(KompasApplication kompasApp)
        {
            _kompasApp = kompasApp;
        }

        /// <summary>
        /// Builds Regular Polygon screwdriver.
        /// </summary>
        /// <returns>Screwdriver entity</returns>
        public override ksEntity BuildScrewdriver()
        {
            var Hate = _kompasApp.Parameters[5];


            var offsetX = 0;
            var offsetY = 0;

            var width = Hate;


            var parameters = new double[3] { offsetX, offsetY, width };

            var entity = CreateCut(parameters);
            if (entity == null)
            {
                return null;
            }
            return entity;



        }   
    }

}
