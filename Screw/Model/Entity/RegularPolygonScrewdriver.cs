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
    class RegularPolygonScrewdriver : ScrewdriverBase
    {
        public RegularPolygonScrewdriver(KompasApplication kompasApp)
        {
            _kompasApp = kompasApp;
        }

        public override ksEntity BuildScrewdriver()
        {
            var H = _kompasApp.Parameters[5];


            var offsetX = 0;
            var offsetY = 0;

            var width = H;


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
