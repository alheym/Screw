using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Screw.Error;
using Screw.Manager;

namespace Screw
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Kompas object manager
        /// </summary>
        private KompasApplication _kompasApp;

        ///// <summary>
        ///// Figure build manager
        ///// </summary>
        //private IManagable _buildManager;

        ///// <summary>
        ///// Figure parameters
        ///// </summary>
        //private List<double> _figureParameters;

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadKompas3D_Click(object sender, EventArgs e)
        {
            if (LoadKompas3D.Enabled)
            {
                var errorCatcher = new ErrorCatcher();
                // Create Kompas application specimen
                _kompasApp = new KompasApplication();

                if (_kompasApp == null)
                {
                    errorCatcher.CatchError(ErrorCodes.KompasObjectCreatingError);
                }
                if (_kompasApp.LastErrorCode != ErrorCodes.OK)
                {
                    errorCatcher.CatchError(_kompasApp.LastErrorCode);
                    return;
                }

                //SetAllInputsEnabledState(true);

                RunButton.Enabled = true;

                LoadKompas3D.Enabled = false;
                CloseKompas3D.Enabled = true;
            }
        }

        private void CloseKompas3D_Click(object sender, EventArgs e)
        {
            if (!LoadKompas3D.Enabled)
            {
                _kompasApp.DestructApp();

                //SetAllInputsEnabledState(false);

                RunButton.Enabled = false;

                LoadKompas3D.Enabled = true;
                CloseKompas3D.Enabled = false;
            }
        }
    }
}
