namespace MicrowaveApp.UI
{
    partial class AddProgramForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblFood = new System.Windows.Forms.Label();
            this.txtFood = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.nudTime = new System.Windows.Forms.NumericUpDown();
            this.lblPower = new System.Windows.Forms.Label();
            this.nudPower = new System.Windows.Forms.NumericUpDown();
            this.lblChar = new System.Windows.Forms.Label();
            this.txtChar = new System.Windows.Forms.TextBox();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(20, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(101, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Nome do Programa:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(162, 17);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(208, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblFood
            // 
            this.lblFood.AutoSize = true;
            this.lblFood.Location = new System.Drawing.Point(20, 50);
            this.lblFood.Name = "lblFood";
            this.lblFood.Size = new System.Drawing.Size(45, 13);
            this.lblFood.TabIndex = 2;
            this.lblFood.Text = "Comida:";
            // 
            // txtFood
            // 
            this.txtFood.Location = new System.Drawing.Point(162, 47);
            this.txtFood.Name = "txtFood";
            this.txtFood.Size = new System.Drawing.Size(208, 20);
            this.txtFood.TabIndex = 3;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(20, 80);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(98, 13);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "Tempo (segundos):";
            // 
            // nudTime
            // 
            this.nudTime.Location = new System.Drawing.Point(163, 78);
            this.nudTime.Name = "nudTime";
            this.nudTime.Size = new System.Drawing.Size(60, 20);
            this.nudTime.TabIndex = 5;
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Location = new System.Drawing.Point(20, 110);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(52, 13);
            this.lblPower.TabIndex = 6;
            this.lblPower.Text = "Potência:";
            // 
            // nudPower
            // 
            this.nudPower.Location = new System.Drawing.Point(163, 108);
            this.nudPower.Name = "nudPower";
            this.nudPower.Size = new System.Drawing.Size(60, 20);
            this.nudPower.TabIndex = 7;
            // 
            // lblChar
            // 
            this.lblChar.AutoSize = true;
            this.lblChar.Location = new System.Drawing.Point(20, 140);
            this.lblChar.Name = "lblChar";
            this.lblChar.Size = new System.Drawing.Size(138, 13);
            this.lblChar.TabIndex = 8;
            this.lblChar.Text = "Carácter para aquecimento:";
            // 
            // txtChar
            // 
            this.txtChar.Location = new System.Drawing.Point(162, 137);
            this.txtChar.MaxLength = 1;
            this.txtChar.Name = "txtChar";
            this.txtChar.Size = new System.Drawing.Size(30, 20);
            this.txtChar.TabIndex = 9;
            this.txtChar.TextChanged += new System.EventHandler(this.txtChar_TextChanged);
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(20, 170);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(59, 13);
            this.lblInstructions.TabIndex = 10;
            this.lblInstructions.Text = "Instruções:";
            // 
            // txtInstructions
            // 
            this.txtInstructions.Location = new System.Drawing.Point(162, 167);
            this.txtInstructions.Multiline = true;
            this.txtInstructions.Name = "txtInstructions";
            this.txtInstructions.Size = new System.Drawing.Size(207, 60);
            this.txtInstructions.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(120, 240);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(220, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddProgramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 290);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtInstructions);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.txtChar);
            this.Controls.Add(this.lblChar);
            this.Controls.Add(this.nudPower);
            this.Controls.Add(this.lblPower);
            this.Controls.Add(this.nudTime);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.txtFood);
            this.Controls.Add(this.lblFood);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddProgramForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adicionar Programa personalizado";
            ((System.ComponentModel.ISupportInitialize)(this.nudTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblFood;
        private System.Windows.Forms.TextBox txtFood;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.NumericUpDown nudTime;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.NumericUpDown nudPower;
        private System.Windows.Forms.Label lblChar;
        private System.Windows.Forms.TextBox txtChar;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}