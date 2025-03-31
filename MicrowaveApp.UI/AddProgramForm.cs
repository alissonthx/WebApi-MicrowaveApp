using MicrowaveApp.Business.Models;
using System;
using System.Windows.Forms;

namespace MicrowaveApp.UI
{
    public partial class AddProgramForm : Form
    {
        public CustomProgram CreatedProgram { get; private set; }

        public AddProgramForm()
        {
            InitializeComponent();
            nudTime.Minimum = 1;
            nudTime.Maximum = 120;
            nudPower.Minimum = 1;
            nudPower.Maximum = 10;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            CreatedProgram = new CustomProgram
            {
                Name = txtName.Text,
                Food = txtFood.Text,
                TimeInSeconds = (int)nudTime.Value,
                Power = (int)nudPower.Value,
                HeatingCharacter = txtChar.Text[0],
                Instructions = txtInstructions.Text,
                IsPredefined = false
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Program name is required!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtChar.Text))
            {
                ShowError("Heating character is required!");
                return false;
            }

            return true;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtChar_TextChanged(object sender, EventArgs e)
        {
            if (txtChar.Text.Length > 1) txtChar.Text = txtChar.Text.Substring(0, 1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}