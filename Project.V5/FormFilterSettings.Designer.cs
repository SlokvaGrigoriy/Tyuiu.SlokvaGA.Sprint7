namespace Project.V5
{
    partial class FormFilterSettings_SGA
    {
        private ComboBox comboBoxColumn_SGA;
        private Label labelColumn_SGA;
        private ComboBox comboBoxFilterType_SGA;
        private Label labelType_SGA;
        private TextBox textBoxValue_SGA;
        private Label labelMean_SGA;
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
            comboBoxColumn_SGA = new ComboBox();
            labelColumn_SGA = new Label();
            comboBoxFilterType_SGA = new ComboBox();
            labelType_SGA = new Label();
            textBoxValue_SGA = new TextBox();
            labelMean_SGA = new Label();
            buttonOK_SGA = new Button();
            buttonCancel_SGA = new Button();
            SuspendLayout();
            // 
            // comboBoxColumn_SGA
            // 
            comboBoxColumn_SGA.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxColumn_SGA.FormattingEnabled = true;
            comboBoxColumn_SGA.Location = new Point(12, 30);
            comboBoxColumn_SGA.Name = "comboBoxColumn_SGA";
            comboBoxColumn_SGA.Size = new Size(260, 24);
            comboBoxColumn_SGA.TabIndex = 0;
            // 
            // labelColumn_SGA
            // 
            labelColumn_SGA.AutoSize = true;
            labelColumn_SGA.Location = new Point(12, 10);
            labelColumn_SGA.Name = "labelColumn_SGA";
            labelColumn_SGA.Size = new Size(66, 16);
            labelColumn_SGA.TabIndex = 1;
            labelColumn_SGA.Text = "Столбец:";
            // 
            // comboBoxFilterType_SGA
            // 
            comboBoxFilterType_SGA.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFilterType_SGA.FormattingEnabled = true;
            comboBoxFilterType_SGA.Location = new Point(12, 85);
            comboBoxFilterType_SGA.Name = "comboBoxFilterType_SGA";
            comboBoxFilterType_SGA.Size = new Size(260, 24);
            comboBoxFilterType_SGA.TabIndex = 1;
            // 
            // labelType_SGA
            // 
            labelType_SGA.AutoSize = true;
            labelType_SGA.Location = new Point(12, 65);
            labelType_SGA.Name = "labelType_SGA";
            labelType_SGA.Size = new Size(95, 16);
            labelType_SGA.TabIndex = 3;
            labelType_SGA.Text = "Тип фильтра:";
            // 
            // textBoxValue_SGA
            // 
            textBoxValue_SGA.Location = new Point(12, 140);
            textBoxValue_SGA.Name = "textBoxValue_SGA";
            textBoxValue_SGA.Size = new Size(260, 22);
            textBoxValue_SGA.TabIndex = 2;
            // 
            // labelMean_SGA
            // 
            labelMean_SGA.AutoSize = true;
            labelMean_SGA.Location = new Point(12, 120);
            labelMean_SGA.Name = "labelMean_SGA";
            labelMean_SGA.Size = new Size(75, 16);
            labelMean_SGA.TabIndex = 5;
            labelMean_SGA.Text = "Значение:";
            // 
            // buttonOK_SGA
            // 
            buttonOK_SGA.Location = new Point(102, 180);
            buttonOK_SGA.Name = "buttonOK_SGA";
            buttonOK_SGA.Size = new Size(89, 30);
            buttonOK_SGA.TabIndex = 3;
            buttonOK_SGA.Text = "Применить";
            buttonOK_SGA.UseVisualStyleBackColor = true;
            buttonOK_SGA.Click += buttonOK_Click;
            // 
            // buttonCancel_SGA
            // 
            buttonCancel_SGA.DialogResult = DialogResult.Cancel;
            buttonCancel_SGA.Location = new Point(197, 180);
            buttonCancel_SGA.Name = "buttonCancel_SGA";
            buttonCancel_SGA.Size = new Size(75, 30);
            buttonCancel_SGA.TabIndex = 4;
            buttonCancel_SGA.Text = "Отмена";
            buttonCancel_SGA.UseVisualStyleBackColor = true;
            buttonCancel_SGA.Click += buttonCancel_Click;
            // 
            // FormFilterSettings_SGA
            // 
            AcceptButton = buttonOK_SGA;
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel_SGA;
            ClientSize = new Size(284, 221);
            Controls.Add(buttonCancel_SGA);
            Controls.Add(buttonOK_SGA);
            Controls.Add(labelMean_SGA);
            Controls.Add(textBoxValue_SGA);
            Controls.Add(labelType_SGA);
            Controls.Add(comboBoxFilterType_SGA);
            Controls.Add(labelColumn_SGA);
            Controls.Add(comboBoxColumn_SGA);
            Font = new Font("Microsoft Sans Serif", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormFilterSettings_SGA";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Настройки фильтрации";
            Load += FormFilterSettings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}