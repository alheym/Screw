using Kompas6API5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Manager
{
    class KompasObjectManager
    {
        /// <summary>
		/// Kompas 3D object
		/// </summary>
		private KompasObject _kompas = null;

        /// <summary>
        /// Kompas 3D object
        /// </summary>
        public KompasObject KompasObject
        {
            get { return _kompas; }
        }

        /// <summary>
        /// Создание объекта программы
        /// </summary>
        public bool CreateKompasObject()
        {
            if (_kompas == null)
            {
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                _kompas = (KompasObject)Activator.CreateInstance(t);
            }
            _kompas.Visible = true;
            _kompas.ActivateControllerAPI();

            if (_kompas == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Unset object of program
        /// </summary>
        public void UnsetKompasObject()
        {
            _kompas.Quit();
            _kompas = null;
        }
    }
}
