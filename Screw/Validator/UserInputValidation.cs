using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Validator
{
    /// <summary>
	/// Class for event handler "CheckNumberKeyPressed".
	/// </summary>
	class UserInputValidation
    {
        /// <summary>
        /// Checks for entering only numbers. He's public!
        /// </summary>
        /// If not in [0-9] or delimiter was entered -- set event handled.
        /// Delimiter is dot or comma -> input number must contain only 1 dot or only 1 comma.
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
            /* Otherwise validation is OK */
        }
    }
}
