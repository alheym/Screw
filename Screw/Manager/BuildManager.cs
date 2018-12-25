using Screw.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Manager
{
    class BuildManager
    {
        /// <summary>
        /// экземпляр приложения Компас
        /// </summary>
        private KompasApplication _kompasApp;

        /// <summary>
        /// Код последей ошибки
        /// </summary>
        public ErrorCodes LastErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Конструктор BuildManager
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
        /// Создание тестовой фигуры
        /// </summary>
        /// <returns>true - если операция прошла успешно, false - в случае ошибки</returns>
        public bool CreateDetail()
        {
            if (!CreateScrew()) return false;

            return true;
        }

        /// <summary>
        /// Создание винта с шляпкой и основанием
        /// </summary>
        /// <returns>true - если операция прошла успешно, false - в случае ошибки</returns>
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
