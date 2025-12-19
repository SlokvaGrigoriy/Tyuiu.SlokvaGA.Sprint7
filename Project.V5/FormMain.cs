namespace Project.V5
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonGo_SGA_MouseEnter(object sender, EventArgs e)
        {
            toolTipHelp_SGA.ToolTipTitle = "Начать";
        }

        private void buttonHelp_SGA_MouseEnter(object sender, EventArgs e)
        {
            toolTipHelp_SGA.ToolTipTitle = "Информация";
        }

        private void buttonGo_SGA_Click(object sender, EventArgs e)
        {
            buttonGo_SGA.Visible = false;
            buttonHelp_SGA.Visible = false;
            labelSymbol_SGA.Visible = false;
            labelWelcomeText_SGA.Visible = false;
            labelWelcome_SGA.Visible = false;
        }

        private void buttonHelp_SGA_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа разработана 21.12.2025", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}