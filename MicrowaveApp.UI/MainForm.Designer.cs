namespace MicrowaveApp.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.btnQuickStart = new System.Windows.Forms.Button();
            this.btnPauseCancel = new System.Windows.Forms.Button();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtPower = new System.Windows.Forms.TextBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.lblProgram = new System.Windows.Forms.Label();
            this.cmbPrograms = new System.Windows.Forms.ComboBox();
            this.btnAddProgram = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Enabled = true;
            this.btnStart.Location = new System.Drawing.Point(20, 169);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 40);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Iniciar";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnQuickStart
            // 
            this.btnQuickStart.Enabled = true;
            this.btnQuickStart.Location = new System.Drawing.Point(140, 169);
            this.btnQuickStart.Name = "btnQuickStart";
            this.btnQuickStart.Size = new System.Drawing.Size(120, 40);
            this.btnQuickStart.TabIndex = 4;
            this.btnQuickStart.Text = "Início Rápido(30s)";
            this.btnQuickStart.UseVisualStyleBackColor = true;
            // 
            // btnPauseCancel
            // 
            this.btnPauseCancel.Enabled = true;
            this.btnPauseCancel.Location = new System.Drawing.Point(280, 169);
            this.btnPauseCancel.Name = "btnPauseCancel";
            this.btnPauseCancel.Size = new System.Drawing.Size(100, 40);
            this.btnPauseCancel.TabIndex = 5;
            this.btnPauseCancel.Text = "Pausar/Cancelar";
            this.btnPauseCancel.UseVisualStyleBackColor = true;
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(120, 30);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(100, 20);
            this.txtTime.TabIndex = 1;
            // 
            // txtPower
            // 
            this.txtPower.Location = new System.Drawing.Point(120, 70);
            this.txtPower.Name = "txtPower";
            this.txtPower.Size = new System.Drawing.Size(100, 20);
            this.txtPower.TabIndex = 2;
            this.txtPower.Text = "10";
            // 
            // lblProgress
            // 
            this.lblProgress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProgress.Location = new System.Drawing.Point(20, 243);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(360, 80);
            this.lblProgress.TabIndex = 6;
            this.lblProgress.Text = "Autenticação bem sucedida.";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(20, 33);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(98, 13);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "Tempo (segundos):";
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Location = new System.Drawing.Point(20, 73);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(52, 13);
            this.lblPower.TabIndex = 0;
            this.lblPower.Text = "Potência:";
            // 
            // lblProgram
            // 
            this.lblProgram.AutoSize = true;
            this.lblProgram.Location = new System.Drawing.Point(22, 110);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(55, 13);
            this.lblProgram.TabIndex = 11;
            this.lblProgram.Text = "Programa:";
            // 
            // cmbPrograms
            // 
            this.cmbPrograms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrograms.Enabled = false;
            this.cmbPrograms.FormattingEnabled = true;
            this.cmbPrograms.Location = new System.Drawing.Point(120, 107);
            this.cmbPrograms.Name = "cmbPrograms";
            this.cmbPrograms.Size = new System.Drawing.Size(213, 21);
            this.cmbPrograms.TabIndex = 10;
            // 
            // btnAddProgram
            // 
            this.btnAddProgram.Location = new System.Drawing.Point(347, 107);
            this.btnAddProgram.Name = "btnAddProgram";
            this.btnAddProgram.Size = new System.Drawing.Size(33, 23);
            this.btnAddProgram.TabIndex = 12;
            this.btnAddProgram.Text = "+";
            this.btnAddProgram.UseVisualStyleBackColor = true;
            this.btnAddProgram.Click += new System.EventHandler(this.btnAddProgram_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 353);
            this.Controls.Add(this.btnAddProgram);
            this.Controls.Add(this.lblProgram);
            this.Controls.Add(this.cmbPrograms);
            this.Controls.Add(this.lblPower);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.txtPower);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.btnPauseCancel);
            this.Controls.Add(this.btnQuickStart);
            this.Controls.Add(this.btnStart);
            this.Name = "MainForm";
            this.Text = "Microondas Digital (Nível 4)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnQuickStart;
        private System.Windows.Forms.Button btnPauseCancel;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtPower;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.ComboBox cmbPrograms;
        private System.Windows.Forms.Button btnAddProgram;
    }
}