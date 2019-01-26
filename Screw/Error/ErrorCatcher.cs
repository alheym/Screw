using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screw.Error
{
   public class ErrorCatcher
    {
        /// <summary>
        /// Показать сообщение об ошибке при построении
        /// </summary>
        public void CatchError(ErrorCodes errorCode)
        {
            if (errorCode == ErrorCodes.OK)
            {
                return;
            }

            MessageBox.Show("Ошибка при построении фигуры. \nКод ошибки: " + errorCode, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Показать сообщение о успешном построении, в случае отсутствия ошибок
        /// </summary>
        public void CatchSuccess()
        {
            MessageBox.Show("Построение завершено.", "Успешно",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
