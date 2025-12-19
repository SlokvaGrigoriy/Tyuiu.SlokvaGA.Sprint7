namespace Project.V5
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            buttonGo_SGA = new Button();
            buttonHelp_SGA = new Button();
            labelSymbol_SGA = new Label();
            labelWelcome_SGA = new Label();
            labelWelcomeText_SGA = new Label();
            toolTipHelp_SGA = new ToolTip(components);
            SuspendLayout();
            // 
            // buttonGo_SGA
            // 
            buttonGo_SGA.Cursor = Cursors.Hand;
            buttonGo_SGA.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonGo_SGA.Location = new Point(300, 350);
            buttonGo_SGA.Name = "buttonGo_SGA";
            buttonGo_SGA.Size = new Size(92, 44);
            buttonGo_SGA.TabIndex = 0;
            buttonGo_SGA.Text = "Начать";
            toolTipHelp_SGA.SetToolTip(buttonGo_SGA, "Нажмите, чтобы перейти к программе");
            buttonGo_SGA.UseVisualStyleBackColor = true;
            buttonGo_SGA.Click += buttonGo_SGA_Click;
            buttonGo_SGA.MouseEnter += buttonGo_SGA_MouseEnter;
            // 
            // buttonHelp_SGA
            // 
            buttonHelp_SGA.Cursor = Cursors.Hand;
            buttonHelp_SGA.Image = (Image)resources.GetObject("buttonHelp_SGA.Image");
            buttonHelp_SGA.Location = new Point(398, 350);
            buttonHelp_SGA.Name = "buttonHelp_SGA";
            buttonHelp_SGA.Size = new Size(58, 44);
            buttonHelp_SGA.TabIndex = 1;
            toolTipHelp_SGA.SetToolTip(buttonHelp_SGA, "Нажмите, чтобы открыть сведения");
            buttonHelp_SGA.UseVisualStyleBackColor = true;
            buttonHelp_SGA.Click += buttonHelp_SGA_Click;
            buttonHelp_SGA.MouseEnter += buttonHelp_SGA_MouseEnter;
            // 
            // labelSymbol_SGA
            // 
            labelSymbol_SGA.AutoSize = true;
            labelSymbol_SGA.Font = new Font("Segoe UI", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelSymbol_SGA.ForeColor = Color.MidnightBlue;
            labelSymbol_SGA.Location = new Point(348, 35);
            labelSymbol_SGA.Name = "labelSymbol_SGA";
            labelSymbol_SGA.Size = new Size(68, 47);
            labelSymbol_SGA.TabIndex = 2;
            labelSymbol_SGA.Text = "🏬";
            // 
            // labelWelcome_SGA
            // 
            labelWelcome_SGA.AutoSize = true;
            labelWelcome_SGA.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelWelcome_SGA.ForeColor = Color.MidnightBlue;
            labelWelcome_SGA.Location = new Point(259, 91);
            labelWelcome_SGA.Name = "labelWelcome_SGA";
            labelWelcome_SGA.Size = new Size(269, 32);
            labelWelcome_SGA.TabIndex = 3;
            labelWelcome_SGA.Text = "ДОБРО ПОЖАЛОВАТЬ!";
            // 
            // labelWelcomeText_SGA
            // 
            labelWelcomeText_SGA.AutoSize = true;
            labelWelcomeText_SGA.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelWelcomeText_SGA.Location = new Point(28, 160);
            labelWelcomeText_SGA.Name = "labelWelcomeText_SGA";
            labelWelcomeText_SGA.Size = new Size(747, 125);
            labelWelcomeText_SGA.TabIndex = 4;
            labelWelcomeText_SGA.Text = resources.GetString("labelWelcomeText_SGA.Text");
            labelWelcomeText_SGA.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // toolTipHelp_SGA
            // 
            toolTipHelp_SGA.AutoPopDelay = 2000;
            toolTipHelp_SGA.InitialDelay = 200;
            toolTipHelp_SGA.IsBalloon = true;
            toolTipHelp_SGA.ReshowDelay = 40;
            toolTipHelp_SGA.ToolTipIcon = ToolTipIcon.Info;
            toolTipHelp_SGA.ToolTipTitle = "Подсказка";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelWelcomeText_SGA);
            Controls.Add(labelWelcome_SGA);
            Controls.Add(labelSymbol_SGA);
            Controls.Add(buttonHelp_SGA);
            Controls.Add(buttonGo_SGA);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormMain";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "🏬 Оптовая база";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonGo_SGA;
        private Button buttonHelp_SGA;
        private Label labelSymbol_SGA;
        private Label labelWelcome_SGA;
        private Label labelWelcomeText_SGA;
        private ToolTip toolTipHelp_SGA;
    }
}