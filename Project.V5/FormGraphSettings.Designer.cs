namespace Project.V5
{
    partial class FormGraphSettings
    {
        private ComboBox comboBoxChartType_SGA;
        private Label labelChartType_SGA;
        private ComboBox comboBoxDataType_SGA;
        private Label labelDataType_SGA;
        private Button buttonOK_SGA;
        private Button buttonCancel_SGA;

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
            comboBoxChartType_SGA = new ComboBox();
            labelChartType_SGA = new Label();
            comboBoxDataType_SGA = new ComboBox();
            labelDataType_SGA = new Label();
            buttonOK_SGA = new Button();
            buttonCancel_SGA = new Button();
            SuspendLayout();
            // 
            // comboBoxChartType_SGA
            // 
            comboBoxChartType_SGA.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxChartType_SGA.FormattingEnabled = true;
            comboBoxChartType_SGA.Location = new Point(12, 30);
            comboBoxChartType_SGA.Name = "comboBoxChartType_SGA";
            comboBoxChartType_SGA.Size = new Size(260, 24);
            comboBoxChartType_SGA.TabIndex = 0;
            // 
            // labelChartType_SGA
            // 
            labelChartType_SGA.AutoSize = true;
            labelChartType_SGA.Location = new Point(12, 10);
            labelChartType_SGA.Name = "labelChartType_SGA";
            labelChartType_SGA.Size = new Size(79, 16);
            labelChartType_SGA.TabIndex = 1;
            labelChartType_SGA.Text = "Тип графика:";
            // 
            // comboBoxDataType_SGA
            // 
            comboBoxDataType_SGA.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDataType_SGA.FormattingEnabled = true;
            comboBoxDataType_SGA.Location = new Point(12, 85);
            comboBoxDataType_SGA.Name = "comboBoxDataType_SGA";
            comboBoxDataType_SGA.Size = new Size(260, 24);
            comboBoxDataType_SGA.TabIndex = 1;
            // 
            // labelDataType_SGA
            // 
            labelDataType_SGA.AutoSize = true;
            labelDataType_SGA.Location = new Point(12, 65);
            labelDataType_SGA.Name = "labelDataType_SGA";
            labelDataType_SGA.Size = new Size(88, 16);
            labelDataType_SGA.TabIndex = 3;
            labelDataType_SGA.Text = "Данные для графика:";
            // 
            // buttonOK_SGA
            // 
            buttonOK_SGA.Location = new Point(97, 130);
            buttonOK_SGA.Name = "buttonOK_SGA";
            buttonOK_SGA.Size = new Size(94, 30);
            buttonOK_SGA.TabIndex = 3;
            buttonOK_SGA.Text = "Построить";
            buttonOK_SGA.UseVisualStyleBackColor = true;
            buttonOK_SGA.Click += buttonOK_Click;
            // 
            // buttonCancel_SGA
            // 
            buttonCancel_SGA.DialogResult = DialogResult.Cancel;
            buttonCancel_SGA.Location = new Point(197, 130);
            buttonCancel_SGA.Name = "buttonCancel_SGA";
            buttonCancel_SGA.Size = new Size(75, 30);
            buttonCancel_SGA.TabIndex = 4;
            buttonCancel_SGA.Text = "Отмена";
            buttonCancel_SGA.UseVisualStyleBackColor = true;
            buttonCancel_SGA.Click += buttonCancel_Click;
            // 
            // FormGraphSettings
            // 
            AcceptButton = buttonOK_SGA;
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel_SGA;
            ClientSize = new Size(284, 171);
            Controls.Add(buttonCancel_SGA);
            Controls.Add(buttonOK_SGA);
            Controls.Add(labelDataType_SGA);
            Controls.Add(comboBoxDataType_SGA);
            Controls.Add(labelChartType_SGA);
            Controls.Add(comboBoxChartType_SGA);
            Font = new Font("Microsoft Sans Serif", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormGraphSettings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Настройки графика";
            Load += FormGraphSettings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}