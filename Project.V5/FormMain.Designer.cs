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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            buttonGo_SGA = new Button();
            buttonHelp_SGA = new Button();
            labelSymbol_SGA = new Label();
            labelWelcome_SGA = new Label();
            labelWelcomeText_SGA = new Label();
            toolTipHelp_SGA = new ToolTip(components);
            panelButtons_SGA = new Panel();
            buttonGraph_SGA = new Button();
            buttonFilter_SGA = new Button();
            buttonSort_SGA = new Button();
            progressBar_SGA = new ProgressBar();
            labelAVG_SGA = new Label();
            labelSum_SGA = new Label();
            labelMax_SGA = new Label();
            labelMin_SGA = new Label();
            textBoxSum_SGA = new TextBox();
            textBoxAVG_SGA = new TextBox();
            textBoxMax_SGA = new TextBox();
            textBoxMin_SGA = new TextBox();
            pictureBoxSearch_SGA = new PictureBox();
            dateTimePicker_SGA = new DateTimePicker();
            textBoxSearch_SGA = new TextBox();
            toolStripButtons_SGA = new ToolStrip();
            toolStripButtonOpen_SGA = new ToolStripButton();
            toolStripButtonSave_SGA = new ToolStripButton();
            toolStripButtonDone_SGA = new ToolStripButton();
            toolStripButtonDel_SGA = new ToolStripButton();
            menuStripButtons_SGA = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            правкаToolStripMenuItem = new ToolStripMenuItem();
            видToolStripMenuItem = new ToolStripMenuItem();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            оПриложенииToolStripMenuItem = new ToolStripMenuItem();
            руководствоToolStripMenuItem = new ToolStripMenuItem();
            panelGrid_SGA = new Panel();
            dataGridViewMain_SGA = new DataGridView();
            panelChart_SGA = new Panel();
            chartGraph_SGA = new System.Windows.Forms.DataVisualization.Charting.Chart();
            openFileDialog_SGA = new OpenFileDialog();
            saveFileDialog_SGA = new SaveFileDialog();
            panelButtons_SGA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSearch_SGA).BeginInit();
            toolStripButtons_SGA.SuspendLayout();
            menuStripButtons_SGA.SuspendLayout();
            panelGrid_SGA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain_SGA).BeginInit();
            panelChart_SGA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartGraph_SGA).BeginInit();
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
            // panelButtons_SGA
            // 
            panelButtons_SGA.Controls.Add(buttonGraph_SGA);
            panelButtons_SGA.Controls.Add(buttonFilter_SGA);
            panelButtons_SGA.Controls.Add(buttonSort_SGA);
            panelButtons_SGA.Controls.Add(progressBar_SGA);
            panelButtons_SGA.Controls.Add(labelAVG_SGA);
            panelButtons_SGA.Controls.Add(labelSum_SGA);
            panelButtons_SGA.Controls.Add(labelMax_SGA);
            panelButtons_SGA.Controls.Add(labelMin_SGA);
            panelButtons_SGA.Controls.Add(textBoxSum_SGA);
            panelButtons_SGA.Controls.Add(textBoxAVG_SGA);
            panelButtons_SGA.Controls.Add(textBoxMax_SGA);
            panelButtons_SGA.Controls.Add(textBoxMin_SGA);
            panelButtons_SGA.Controls.Add(pictureBoxSearch_SGA);
            panelButtons_SGA.Controls.Add(dateTimePicker_SGA);
            panelButtons_SGA.Controls.Add(textBoxSearch_SGA);
            panelButtons_SGA.Controls.Add(toolStripButtons_SGA);
            panelButtons_SGA.Controls.Add(menuStripButtons_SGA);
            panelButtons_SGA.Location = new Point(0, 0);
            panelButtons_SGA.Name = "panelButtons_SGA";
            panelButtons_SGA.Size = new Size(800, 138);
            panelButtons_SGA.TabIndex = 5;
            // 
            // buttonGraph_SGA
            // 
            buttonGraph_SGA.Cursor = Cursors.Hand;
            buttonGraph_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonGraph_SGA.Location = new Point(248, 53);
            buttonGraph_SGA.Name = "buttonGraph_SGA";
            buttonGraph_SGA.Size = new Size(100, 44);
            buttonGraph_SGA.TabIndex = 7;
            buttonGraph_SGA.Text = "График";
            buttonGraph_SGA.UseVisualStyleBackColor = true;
            // 
            // buttonFilter_SGA
            // 
            buttonFilter_SGA.Cursor = Cursors.Hand;
            buttonFilter_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonFilter_SGA.Location = new Point(131, 53);
            buttonFilter_SGA.Name = "buttonFilter_SGA";
            buttonFilter_SGA.Size = new Size(100, 44);
            buttonFilter_SGA.TabIndex = 20;
            buttonFilter_SGA.Text = "Фильтрация";
            buttonFilter_SGA.UseVisualStyleBackColor = true;
            // 
            // buttonSort_SGA
            // 
            buttonSort_SGA.Cursor = Cursors.Hand;
            buttonSort_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonSort_SGA.Location = new Point(12, 53);
            buttonSort_SGA.Name = "buttonSort_SGA";
            buttonSort_SGA.Size = new Size(100, 44);
            buttonSort_SGA.TabIndex = 7;
            buttonSort_SGA.Text = "Сортировка";
            buttonSort_SGA.UseVisualStyleBackColor = true;
            // 
            // progressBar_SGA
            // 
            progressBar_SGA.Location = new Point(351, 24);
            progressBar_SGA.Name = "progressBar_SGA";
            progressBar_SGA.Size = new Size(275, 25);
            progressBar_SGA.TabIndex = 19;
            // 
            // labelAVG_SGA
            // 
            labelAVG_SGA.AutoSize = true;
            labelAVG_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelAVG_SGA.Location = new Point(516, 110);
            labelAVG_SGA.Name = "labelAVG_SGA";
            labelAVG_SGA.Size = new Size(60, 17);
            labelAVG_SGA.TabIndex = 18;
            labelAVG_SGA.Text = "среднее:";
            // 
            // labelSum_SGA
            // 
            labelSum_SGA.AutoSize = true;
            labelSum_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelSum_SGA.Location = new Point(337, 112);
            labelSum_SGA.Name = "labelSum_SGA";
            labelSum_SGA.Size = new Size(48, 17);
            labelSum_SGA.TabIndex = 17;
            labelSum_SGA.Text = "сумма:";
            // 
            // labelMax_SGA
            // 
            labelMax_SGA.AutoSize = true;
            labelMax_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelMax_SGA.Location = new Point(172, 112);
            labelMax_SGA.Name = "labelMax_SGA";
            labelMax_SGA.Size = new Size(35, 17);
            labelMax_SGA.TabIndex = 16;
            labelMax_SGA.Text = "max:";
            // 
            // labelMin_SGA
            // 
            labelMin_SGA.AutoSize = true;
            labelMin_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelMin_SGA.Location = new Point(3, 112);
            labelMin_SGA.Name = "labelMin_SGA";
            labelMin_SGA.Size = new Size(32, 17);
            labelMin_SGA.TabIndex = 15;
            labelMin_SGA.Text = "min:";
            // 
            // textBoxSum_SGA
            // 
            textBoxSum_SGA.Location = new Point(391, 109);
            textBoxSum_SGA.Name = "textBoxSum_SGA";
            textBoxSum_SGA.ReadOnly = true;
            textBoxSum_SGA.Size = new Size(100, 23);
            textBoxSum_SGA.TabIndex = 14;
            // 
            // textBoxAVG_SGA
            // 
            textBoxAVG_SGA.Location = new Point(584, 109);
            textBoxAVG_SGA.Name = "textBoxAVG_SGA";
            textBoxAVG_SGA.ReadOnly = true;
            textBoxAVG_SGA.Size = new Size(100, 23);
            textBoxAVG_SGA.TabIndex = 12;
            // 
            // textBoxMax_SGA
            // 
            textBoxMax_SGA.Location = new Point(213, 109);
            textBoxMax_SGA.Name = "textBoxMax_SGA";
            textBoxMax_SGA.ReadOnly = true;
            textBoxMax_SGA.Size = new Size(100, 23);
            textBoxMax_SGA.TabIndex = 11;
            // 
            // textBoxMin_SGA
            // 
            textBoxMin_SGA.Location = new Point(40, 109);
            textBoxMin_SGA.Name = "textBoxMin_SGA";
            textBoxMin_SGA.ReadOnly = true;
            textBoxMin_SGA.Size = new Size(100, 23);
            textBoxMin_SGA.TabIndex = 10;
            // 
            // pictureBoxSearch_SGA
            // 
            pictureBoxSearch_SGA.Image = (Image)resources.GetObject("pictureBoxSearch_SGA.Image");
            pictureBoxSearch_SGA.Location = new Point(632, 24);
            pictureBoxSearch_SGA.Name = "pictureBoxSearch_SGA";
            pictureBoxSearch_SGA.Size = new Size(25, 23);
            pictureBoxSearch_SGA.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxSearch_SGA.TabIndex = 9;
            pictureBoxSearch_SGA.TabStop = false;
            // 
            // dateTimePicker_SGA
            // 
            dateTimePicker_SGA.ImeMode = ImeMode.On;
            dateTimePicker_SGA.Location = new Point(663, 53);
            dateTimePicker_SGA.Name = "dateTimePicker_SGA";
            dateTimePicker_SGA.Size = new Size(137, 23);
            dateTimePicker_SGA.TabIndex = 8;
            // 
            // textBoxSearch_SGA
            // 
            textBoxSearch_SGA.Location = new Point(663, 24);
            textBoxSearch_SGA.Name = "textBoxSearch_SGA";
            textBoxSearch_SGA.Size = new Size(137, 23);
            textBoxSearch_SGA.TabIndex = 7;
            textBoxSearch_SGA.KeyDown += textBoxSearch_SGA_KeyDown;
            // 
            // toolStripButtons_SGA
            // 
            toolStripButtons_SGA.Dock = DockStyle.None;
            toolStripButtons_SGA.Items.AddRange(new ToolStripItem[] { toolStripButtonOpen_SGA, toolStripButtonSave_SGA, toolStripButtonDone_SGA, toolStripButtonDel_SGA });
            toolStripButtons_SGA.Location = new Point(0, 24);
            toolStripButtons_SGA.Name = "toolStripButtons_SGA";
            toolStripButtons_SGA.Size = new Size(348, 25);
            toolStripButtons_SGA.TabIndex = 6;
            toolStripButtons_SGA.Text = "toolStrip1";
            // 
            // toolStripButtonOpen_SGA
            // 
            toolStripButtonOpen_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            toolStripButtonOpen_SGA.Image = (Image)resources.GetObject("toolStripButtonOpen_SGA.Image");
            toolStripButtonOpen_SGA.ImageTransparentColor = Color.Magenta;
            toolStripButtonOpen_SGA.Name = "toolStripButtonOpen_SGA";
            toolStripButtonOpen_SGA.Size = new Size(78, 22);
            toolStripButtonOpen_SGA.Tag = "";
            toolStripButtonOpen_SGA.Text = "Открыть";
            toolStripButtonOpen_SGA.Click += toolStripButtonOpen_SGA_Click;
            // 
            // toolStripButtonSave_SGA
            // 
            toolStripButtonSave_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            toolStripButtonSave_SGA.Image = (Image)resources.GetObject("toolStripButtonSave_SGA.Image");
            toolStripButtonSave_SGA.ImageTransparentColor = Color.Magenta;
            toolStripButtonSave_SGA.Name = "toolStripButtonSave_SGA";
            toolStripButtonSave_SGA.Size = new Size(91, 22);
            toolStripButtonSave_SGA.Text = "Сохранить";
            toolStripButtonSave_SGA.Click += toolStripButtonSave_SGA_Click;
            // 
            // toolStripButtonDone_SGA
            // 
            toolStripButtonDone_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            toolStripButtonDone_SGA.Image = (Image)resources.GetObject("toolStripButtonDone_SGA.Image");
            toolStripButtonDone_SGA.ImageTransparentColor = Color.Magenta;
            toolStripButtonDone_SGA.Name = "toolStripButtonDone_SGA";
            toolStripButtonDone_SGA.Size = new Size(92, 22);
            toolStripButtonDone_SGA.Text = "Выполнить";
            toolStripButtonDone_SGA.Click += toolStripButtonDone_SGA_Click;
            // 
            // toolStripButtonDel_SGA
            // 
            toolStripButtonDel_SGA.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            toolStripButtonDel_SGA.Image = (Image)resources.GetObject("toolStripButtonDel_SGA.Image");
            toolStripButtonDel_SGA.ImageTransparentColor = Color.Magenta;
            toolStripButtonDel_SGA.Name = "toolStripButtonDel_SGA";
            toolStripButtonDel_SGA.Size = new Size(75, 22);
            toolStripButtonDel_SGA.Text = "Удалить";
            toolStripButtonDel_SGA.Click += toolStripButtonDel_SGA_Click;
            // 
            // menuStripButtons_SGA
            // 
            menuStripButtons_SGA.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, правкаToolStripMenuItem, видToolStripMenuItem, справкаToolStripMenuItem });
            menuStripButtons_SGA.Location = new Point(0, 0);
            menuStripButtons_SGA.Name = "menuStripButtons_SGA";
            menuStripButtons_SGA.Size = new Size(800, 24);
            menuStripButtons_SGA.TabIndex = 0;
            menuStripButtons_SGA.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // правкаToolStripMenuItem
            // 
            правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            правкаToolStripMenuItem.Size = new Size(39, 20);
            правкаToolStripMenuItem.Text = "Вид";
            // 
            // видToolStripMenuItem
            // 
            видToolStripMenuItem.Name = "видToolStripMenuItem";
            видToolStripMenuItem.Size = new Size(59, 20);
            видToolStripMenuItem.Text = "Правка";
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { оПриложенииToolStripMenuItem, руководствоToolStripMenuItem });
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(65, 20);
            справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПриложенииToolStripMenuItem
            // 
            оПриложенииToolStripMenuItem.Name = "оПриложенииToolStripMenuItem";
            оПриложенииToolStripMenuItem.Size = new Size(149, 22);
            оПриложенииToolStripMenuItem.Text = "О программе";
            оПриложенииToolStripMenuItem.ToolTipText = "Нажмите, чтобы открыть информацию о приложении";
            // 
            // руководствоToolStripMenuItem
            // 
            руководствоToolStripMenuItem.Name = "руководствоToolStripMenuItem";
            руководствоToolStripMenuItem.Size = new Size(149, 22);
            руководствоToolStripMenuItem.Text = "Руководство";
            руководствоToolStripMenuItem.ToolTipText = "Нажмите, чтобы открыть руководство пользователя";
            // 
            // panelGrid_SGA
            // 
            panelGrid_SGA.Controls.Add(dataGridViewMain_SGA);
            panelGrid_SGA.Location = new Point(0, 138);
            panelGrid_SGA.Name = "panelGrid_SGA";
            panelGrid_SGA.Size = new Size(491, 312);
            panelGrid_SGA.TabIndex = 6;
            // 
            // dataGridViewMain_SGA
            // 
            dataGridViewMain_SGA.Location = new Point(0, 0);
            dataGridViewMain_SGA.Name = "dataGridViewMain_SGA";
            dataGridViewMain_SGA.RowHeadersVisible = false;
            dataGridViewMain_SGA.Size = new Size(491, 312);
            dataGridViewMain_SGA.TabIndex = 7;
            // 
            // panelChart_SGA
            // 
            panelChart_SGA.Controls.Add(chartGraph_SGA);
            panelChart_SGA.Location = new Point(491, 138);
            panelChart_SGA.Name = "panelChart_SGA";
            panelChart_SGA.Size = new Size(309, 312);
            panelChart_SGA.TabIndex = 9;
            // 
            // chartGraph_SGA
            // 
            chartArea2.Name = "ChartArea1";
            chartGraph_SGA.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartGraph_SGA.Legends.Add(legend2);
            chartGraph_SGA.Location = new Point(0, 0);
            chartGraph_SGA.Name = "chartGraph_SGA";
            chartGraph_SGA.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartGraph_SGA.Series.Add(series2);
            chartGraph_SGA.Size = new Size(312, 312);
            chartGraph_SGA.TabIndex = 8;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelChart_SGA);
            Controls.Add(panelGrid_SGA);
            Controls.Add(panelButtons_SGA);
            Controls.Add(labelWelcomeText_SGA);
            Controls.Add(labelWelcome_SGA);
            Controls.Add(labelSymbol_SGA);
            Controls.Add(buttonHelp_SGA);
            Controls.Add(buttonGo_SGA);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStripButtons_SGA;
            MaximizeBox = false;
            Name = "FormMain";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "🏬 Оптовая база";
            panelButtons_SGA.ResumeLayout(false);
            panelButtons_SGA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSearch_SGA).EndInit();
            toolStripButtons_SGA.ResumeLayout(false);
            toolStripButtons_SGA.PerformLayout();
            menuStripButtons_SGA.ResumeLayout(false);
            menuStripButtons_SGA.PerformLayout();
            panelGrid_SGA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain_SGA).EndInit();
            panelChart_SGA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartGraph_SGA).EndInit();
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
        private Panel panelButtons_SGA;
        private ToolStrip toolStripButtons_SGA;
        private MenuStrip menuStripButtons_SGA;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem правкаToolStripMenuItem;
        private ToolStripMenuItem видToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem оПриложенииToolStripMenuItem;
        private ToolStripMenuItem руководствоToolStripMenuItem;
        private Panel panelGrid_SGA;
        private DataGridView dataGridViewMain_SGA;
        private ToolStripButton toolStripButtonOpen_SGA;
        private ToolStripButton toolStripButtonSave_SGA;
        private ToolStripButton toolStripButtonDel_SGA;
        private ToolStripButton toolStripButtonDone_SGA;
        private PictureBox pictureBoxSearch_SGA;
        private DateTimePicker dateTimePicker_SGA;
        private TextBox textBoxSearch_SGA;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGraph_SGA;
        private TextBox textBoxAVG_SGA;
        private TextBox textBoxMax_SGA;
        private TextBox textBoxMin_SGA;
        private Label labelSum_SGA;
        private Label labelMax_SGA;
        private Label labelMin_SGA;
        private TextBox textBoxSum_SGA;
        private Label labelAVG_SGA;
        private ProgressBar progressBar_SGA;
        private OpenFileDialog openFileDialog_SGA;
        private SaveFileDialog saveFileDialog_SGA;
        private Button buttonSort_SGA;
        private Button buttonGraph_SGA;
        private Button buttonFilter_SGA;
        private Panel panelChart_SGA;
    }
}