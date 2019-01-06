using System.Collections.Generic;
using System.Windows.Forms;

namespace Screw.Error
{
    /// <summary>
    /// User input error catcher
    /// </summary>
    class UserInputErrorCatcher
    {
        /// <summary>
        /// Catch errors
        /// </summary>
        public void CatchError(List<string> errors)
        {
            string errorMessage = "Error during user input. \n Please check these cases: \n\n";

            foreach (string error in errors)
            {
                errorMessage += error + "\n\n";
            }

            MessageBox.Show(errorMessage, "User input error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
