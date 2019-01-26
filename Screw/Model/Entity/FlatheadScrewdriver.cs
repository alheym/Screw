using Kompas6API5;
using Kompas6Constants3D;
using Screw.Error;
using Screw.Model.FigureParam;
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
    /// Flathead screwdriver.
    /// </summary>
     class FlatheadScrewdriver : ScrewdriverBase
    {

        /// <summary>
        /// Flathead builder.
        /// </summary>
        /// <param name="kompasApp">Kompas application object</param>
        public FlatheadScrewdriver(KompasApplication kompasApp)
        {
            _kompasApp = kompasApp;
        }

        /// <summary>
        /// Builds flathead screwdriver.
        /// </summary>
        /// <returns>Screwdriver entity</returns>
        public override ksEntity BuildScrewdriver()
        {
            var Diameter = _kompasApp.Parameters[0];
            var Hate = _kompasApp.Parameters[5];


            var offsetX = -0.6 * Diameter; // параметр отвечающий за сдвиг щлица по оси Х
            var offsetY = -0.4 * Hate;
            

            var width = 1.2 * Diameter; // параметр отвечающий за длинну шлица (свдиг по оси Х умножить на 2)
            var height = 0.8 * Hate;
           
            var parameters = new double[4]{ offsetX, offsetY, width, height };

            var entity = CreateCutout(parameters);
            if (entity == null)
            {
                return null;
                
            }
            return entity;
        }
        
        
    }
}
