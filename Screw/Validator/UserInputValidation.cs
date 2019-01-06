using System;
using System.Windows.Forms;

namespace Screw.Validator
{
    /// <summary>
    /// Класс для обработчика событий "CheckNumberKeyPressed".
    /// </summary>
    class UserInputValidation
    {
        /// <summary>
        /// Проверяет на ввод только цифры.
        /// </summary>
        /// If отсутствует в [0-9] или введен разделитель -- установить событие обработано.
        ///Разделитель - точка или запятая -> вводимый номер должен содержать только 1 точку или только 1 запятую.
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CheckNumberKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsControl(e.KeyChar))
                && !(Char.IsDigit(e.KeyChar))
                && !((e.KeyChar == '.') && (((TextBox)sender).Text.IndexOf(".") == -1) && (((TextBox)sender).Text.IndexOf(",") == -1))
                && !((e.KeyChar == ',') && (((TextBox)sender).Text.IndexOf(",") == -1) && (((TextBox)sender).Text.IndexOf(".") == -1))
            )
            {
                e.Handled = true;
            }
            /* В противном случае проверка в порядке */
        }
    }
}
