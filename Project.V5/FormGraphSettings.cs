using System;
using System.Windows.Forms;

namespace Project.V5
{
    public partial class FormGraphSettings : Form
    {
        public string ChartType { get; private set; }
        public string DataType { get; private set; }

        public FormGraphSettings()
        {
            InitializeComponent();
        }

        private void FormGraphSettings_Load(object sender, EventArgs e)
        {
            // Заполняем типы графиков
            comboBoxChartType_SGA.Items.AddRange(new string[] {
                "Column", "Line", "Spline", "Area", "Pie", "Bar"
            });
            comboBoxChartType_SGA.SelectedIndex = 0;

            // Заполняем типы данных
            comboBoxDataType_SGA.Items.AddRange(new string[] {
                "Статистика", "Выделенные данные", "Выбранный столбец"
            });
            comboBoxDataType_SGA.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxChartType_SGA.SelectedItem == null ||
                comboBoxDataType_SGA.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChartType = comboBoxChartType_SGA.SelectedItem.ToString();
            DataType = comboBoxDataType_SGA.SelectedItem.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}