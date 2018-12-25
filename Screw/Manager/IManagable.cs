using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Manager
{
    /// <summary>
    /// Build manager interface
    /// </summary>
    interface IManagable
    {
        /// <summary>
        /// Создание детали в Компасе
        /// </summary>
        /// <returns>true - если операция прошла успешно, false - в случае ошибки</returns>
        bool CreateDetail();
    }
}
