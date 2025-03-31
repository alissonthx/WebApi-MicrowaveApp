using System;
using System.Windows.Forms;
using MicrowaveApp.Business;
using MicrowaveApp.Business.Exceptions;
using MicrowaveApp.Business.Models;
using MicrowaveApp.Business.Services;

namespace MicrowaveApp.UI
{
    public partial class MainForm : Form
    {
        private readonly Microwave _microwave;
        private readonly ProgramService _programService;

        public MainForm()
        {
            InitializeComponent();
            _microwave = new Microwave();
            _programService = new ProgramService(new JsonProgramRepository());
            SetupEventHandlers();
            LoadPredefinedPrograms();
        }

        private void SetupEventHandlers()
        {
            _microwave.HeatingProgressChanged += UpdateProgressDisplay;
            _microwave.HeatingStarted += OnHeatingStarted;
            _microwave.HeatingPaused += OnHeatingPaused;
            _microwave.HeatingResumed += OnHeatingResumed;
            _microwave.HeatingCanceled += OnHeatingCanceled;
            _microwave.HeatingFinished += OnHeatingFinished;
        }

        private void LoadPredefinedPrograms()
        {
            cmbPrograms.DataSource = _programService.GetPredefinedPrograms();
            cmbPrograms.DataSource = _programService.GetCustomPrograms();
            cmbPrograms.DisplayMember = "Name";
            cmbPrograms.ValueMember = "Name";
        }

        #region Buttons
        private void btnStartProgram_Click(object sender, EventArgs e)
        {
            var selectedProgram = (PredefinedProgram)cmbPrograms.SelectedItem;
            _microwave.StartPredefinedProgram(selectedProgram);

            // Disable manual controlls
            txtTime.Enabled = false;
            txtPower.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                int time = string.IsNullOrEmpty(txtTime.Text) ? 0 : int.Parse(txtTime.Text);
                int power = string.IsNullOrEmpty(txtPower.Text) ? 10 : int.Parse(txtPower.Text);

                _microwave.StartHeating(time, power);
            }
            catch (InvalidTimeException ex)
            {
                MessageBox.Show(ex.Message, "Tempo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidPowerException ex)
            {
                MessageBox.Show(ex.Message, "Potência inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, insira valores numéricos válidos", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuickStart_Click(object sender, EventArgs e)
        {
            _microwave.QuickStart();
        }

        private void btnPauseCancel_Click(object sender, EventArgs e)
        {
            _microwave.PauseOrCancel();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (txtTime.Focused)
            {
                txtTime.Text += btn.Text;
            }
            else if (txtPower.Focused)
            {
                txtPower.Text += btn.Text;
            }
        }

        private void btnAddProgram_Click(object sender, EventArgs e)
        {
            var addProgramForm = new AddProgramForm();

            // modal dialog to add a new program
            addProgramForm.ShowDialog();
        }

        #endregion

        private void UpdateProgressDisplay(string progress)
        {
            if (lblProgress.InvokeRequired)
            {
                lblProgress.Invoke(new Action<string>(UpdateProgressDisplay), progress);
            }
            else
            {
                lblProgress.Text = progress;
            }
        }

        private void OnHeatingStarted()
        {
            UpdateButtonStates();
        }

        private void OnHeatingPaused()
        {
            UpdateButtonStates();
        }

        private void OnHeatingResumed()
        {
            UpdateButtonStates();
        }

        private void OnHeatingCanceled()
        {
            UpdateButtonStates();
            lblProgress.Text = "Aquecimento cancelado";
        }

        private void OnHeatingFinished()
        {
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            btnStart.Enabled = !_microwave.IsRunning || _microwave.IsPaused;
            btnPauseCancel.Text = _microwave.IsRunning && !_microwave.IsPaused ? "Pausar" : "Cancelar";
        }
    }
}
