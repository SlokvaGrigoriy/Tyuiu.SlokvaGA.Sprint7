namespace Project.V5
{
    partial class FormSortSettings
    {
        private ComboBox comboBoxColumn_SGA;
        private Label labelColumn_SGA;
        private GroupBox groupBoxorder_SGA;
        private RadioButton radioButtonDesc_SGA;
        private RadioButton radioButtonAsc_SGA;
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
            groupBoxorder_SGA = new GroupBox();
            radioButtonDesc_SGA = new RadioButton();
            radioButtonAsc_SGA = new RadioButton();
            buttonOK_SGA = new Button();
            buttonCancel_SGA = new Button();
            groupBoxorder_SGA.SuspendLayout();
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
            // groupBoxorder_SGA
            // 
            groupBoxorder_SGA.Controls.Add(radioButtonDesc_SGA);
            groupBoxorder_SGA.Controls.Add(radioButtonAsc_SGA);
            groupBoxorder_SGA.Location = new Point(12, 70);
            groupBoxorder_SGA.Name = "groupBoxorder_SGA";
            groupBoxorder_SGA.Size = new Size(260, 80);
            groupBoxorder_SGA.TabIndex = 2;
            groupBoxorder_SGA.TabStop = false;
            groupBoxorder_SGA.Text = "Порядок сортировки";
            // 
            // radioButtonDesc_SGA
            // 
            radioButtonDesc_SGA.AutoSize = true;
            radioButtonDesc_SGA.Location = new Point(20, 50);
            radioButtonDesc_SGA.Name = "radioButtonDesc_SGA";
            radioButtonDesc_SGA.Size = new Size(91, 20);
            radioButtonDesc_SGA.TabIndex = 1;
            radioButtonDesc_SGA.Text = "Убывание";
            radioButtonDesc_SGA.UseVisualStyleBackColor = true;
            // 
            // radioButtonAsc_SGA
            // 
            radioButtonAsc_SGA.AutoSize = true;
            radioButtonAsc_SGA.Checked = true;
            radioButtonAsc_SGA.Location = new Point(20, 25);
            radioButtonAsc_SGA.Name = "radioButtonAsc_SGA";
            radioButtonAsc_SGA.Size = new Size(112, 20);
            radioButtonAsc_SGA.TabIndex = 0;
            radioButtonAsc_SGA.TabStop = true;
            radioButtonAsc_SGA.Text = "Возрастание";
            radioButtonAsc_SGA.UseVisualStyleBackColor = true;
            // 
            // buttonOK_SGA
            // 
            buttonOK_SGA.Location = new Point(97, 170);
            buttonOK_SGA.Name = "buttonOK_SGA";
            buttonOK_SGA.Size = new Size(94, 30);
            buttonOK_SGA.TabIndex = 3;
            buttonOK_SGA.Text = "Применить";
            buttonOK_SGA.UseVisualStyleBackColor = true;
            buttonOK_SGA.Click += buttonOK_Click;
            // 
            // buttonCancel_SGA
            // 
            buttonCancel_SGA.DialogResult = DialogResult.Cancel;
            buttonCancel_SGA.Location = new Point(197, 170);
            buttonCancel_SGA.Name = "buttonCancel_SGA";
            buttonCancel_SGA.Size = new Size(75, 30);
            buttonCancel_SGA.TabIndex = 4;
            buttonCancel_SGA.Text = "Отмена";
            buttonCancel_SGA.UseVisualStyleBackColor = true;
            buttonCancel_SGA.Click += buttonCancel_Click;
            // 
            // FormSortSettings
            // 
            AcceptButton = buttonOK_SGA;
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel_SGA;
            ClientSize = new Size(284, 211);
            Controls.Add(buttonCancel_SGA);
            Controls.Add(buttonOK_SGA);
            Controls.Add(groupBoxorder_SGA);
            Controls.Add(labelColumn_SGA);
            Controls.Add(comboBoxColumn_SGA);
            Font = new Font("Microsoft Sans Serif", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSortSettings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Настройки сортировки";
            Load += FormSortSettings_Load;
            groupBoxorder_SGA.ResumeLayout(false);
            groupBoxorder_SGA.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}