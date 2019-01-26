using System.Collections.Generic;
using System.Windows.Forms;

namespace Screw.Error
{
    /// <summary>
    /// ошибки ввода пользователем
    /// </summary>
    public class UserInputErrorCatcher
    {
        /// <summary>
        /// Catch errors
        /// </summary>
        public void CatchError(List<string> errors)
        {
            string errorMessage = "Ошибка при вводе пользователем. \n Пожалуйста, проверьте эти данные: \n\n";

            foreach (string error in errors)
            {
                errorMessage += error + "\n\n";
            }

            MessageBox.Show(errorMessage, "Ошибка ввода пользователем",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
