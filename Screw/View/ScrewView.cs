using System;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;
using Screw.Error;
using Screw.Manager;
using Screw.Validator;
using Screw.Model;

namespace Screw
{

    /// <summary>
    /// Main form of program
    /// </summary>
    public partial class ScrewView : Form
    {
        /// <summary>
        /// Kompas object manager
        /// </summary>
        private KompasApplication _kompasApp;

        /// <summary>
        /// Figure build manager
        /// </summary>
        private IManagable _buildManager;

        /// <summary>
        /// Figure parameters
        /// </summary>
        private List<double> _figureParameters;

        /// <summary>
        /// screw view form constructor
        /// </summary>
        public ScrewView()
        {
            InitializeComponent();

            CloseKompas3D.Enabled = false;
            RunButton.Enabled = false;
            SetAllInputsEnabledState(false);

            // Set culture info for current thread
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Set state to all inputs "Enabled" property
        /// </summary>
        /// <param name="state">State of "Enabled" property</param>
        private void SetAllInputsEnabledState(bool state)
        {
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(GroupBox))
                {
                    foreach (Control combobox in control.Controls)
                    {
                        if (combobox.GetType() == typeof(ComboBox))
                        {
                            combobox.Enabled = state;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Set figure parameters
        /// </summary>
        /// <returns>true если операция прошла успешно; false в случае ошибки</returns>
        private bool SetFigureParameters()
        {
            try
            {
                var screwHatWidth = Convert.ToDouble(ScrewHatWidth.Text);
                var screwHatInnerDiameter = Convert.ToDouble(this.screwHatInnerDiameter.Text);
                var screwBaseSmoothWidth = Convert.ToDouble(ScrewBaseSmoothWidth.Text);
                var screwBaseThreadWidth = Convert.ToDouble(ScrewBaseThreadWidth.Text);
                var nutHeight = Convert.ToDouble(NutHeight.Text);
                var nutThreadDiameter = Convert.ToDouble(NutThreadDiameter.Text);

                var parameters = new List<double>() {screwHatWidth, screwHatInnerDiameter, screwBaseSmoothWidth,
                screwBaseThreadWidth, nutHeight, nutThreadDiameter };

                var validator = new FigureParametersValidator(parameters);
                if (validator.LastErrorCode != ErrorCodes.OK)
                {
                    return false;
                }

                if (!validator.Validate())
                {
                    var errorCatcher = new UserInputErrorCatcher();
                    errorCatcher.CatchError(validator.ErrorList);

                    return false;
                }

                _figureParameters = parameters;
            }
            catch
            {
                MessageBox.Show("Есть несколько пустых или недействительных полей. Пожалуйста, заполните их правильно и попробуйте снова. ", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Посылает элемент управления в класс для проверки пользовательского ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckNumberKeyPressed(object sender, KeyPressEventArgs e)
        {
            UserInputValidation.CheckNumberKeyPressed(sender, e);
        }

        private void LoadKompas3D_Click(object sender, EventArgs e)
        {
            if (LoadKompas3D.Enabled)
            {
                var errorCatcher = new ErrorCatcher();

                // Создать образец приложения Kompas
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

                SetAllInputsEnabledState(true);

                RunButton.Enabled = true;

                LoadKompas3D.Enabled = false;
                CloseKompas3D.Enabled = true;
            }
        }


        /// <summary>
        ///Проверка пользовательских параметров ввода и построение фигуры после этого
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunButton_Click_1(object sender, EventArgs e)
        {
            var errorCatcher = new ErrorCatcher();

            if (_kompasApp == null)
            {
                MessageBox.Show("Сначала загрузите KOMPAS 3D.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            if (!SetFigureParameters())
            {
                return;
            }

            _kompasApp.Parameters = _figureParameters;

            // Create 3D document
            if (!_kompasApp.CreateDocument3D())
            {
                return;
            }

            // Create build manager
            BuildManager _buildManager = new BuildManager(_kompasApp);
            if (_buildManager == null)
            {
                errorCatcher.CatchError(ErrorCodes.ManagerCreatingError);
                return;
            }
            if (_buildManager.LastErrorCode != ErrorCodes.OK)
            {
                errorCatcher.CatchError(_buildManager.LastErrorCode);
                return;
            }

            _buildManager.CreateDetail();

            if (_buildManager.LastErrorCode != ErrorCodes.OK)
            {
                errorCatcher.CatchError(_buildManager.LastErrorCode);
            }
            else
            {
                errorCatcher.CatchSuccess();
            }
        }

        /// <summary>
        /// Установка стандартных параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Defaults_Click_1(object sender, EventArgs e)
        {
            ScrewHatWidth.Text = "33";
            screwHatInnerDiameter.Text = "5";
            ScrewBaseSmoothWidth.Text = "10";
            ScrewBaseThreadWidth.Text = "75";
            NutHeight.Text = "11";
            NutThreadDiameter.Text = "7";
        }

        /// <summary>
        /// Unset Kompas 3D object from controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseKompas3D_Click(object sender, EventArgs e)
        {
            if (!LoadKompas3D.Enabled)
            {
                _kompasApp.DestructApp();

                SetAllInputsEnabledState(false);

                RunButton.Enabled = false;

                LoadKompas3D.Enabled = true;
                CloseKompas3D.Enabled = false;
            }
        }
    }
}
