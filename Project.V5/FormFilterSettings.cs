using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.V5
{
    public partial class FormFilterSettings_SGA : Form
    {
        private DataGridView dataGridView;
        public string SelectedColumn { get; private set; }
        public string FilterType { get; private set; }
        public string FilterValue { get; private set; }
        public FormFilterSettings_SGA(DataGridView dgv)
        {
            InitializeComponent();
            dataGridView = dgv;
        }
        private void FormFilterSettings_Load(object sender, EventArgs e)
        {
            // Заполняем ComboBox столбцами из DataGridView
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                comboBoxColumn_SGA.Items.Add(column.HeaderText);
            }

            if (comboBoxColumn_SGA.Items.Count > 0)
            {
                comboBoxColumn_SGA.SelectedIndex = 0;
            }

            // Заполняем типы фильтров
            comboBoxFilterType_SGA.Items.AddRange(new string[] {
                "Равно", "Содержит", "Начинается с", "Заканчивается на",
                "Больше", "Меньше", "Не равно"
            });
            comboBoxFilterType_SGA.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxColumn_SGA.SelectedItem == null ||
                comboBoxFilterType_SGA.SelectedItem == null ||
                string.IsNullOrWhiteSpace(textBoxValue_SGA.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectedColumn = comboBoxColumn_SGA.SelectedItem.ToString();
            FilterType = comboBoxFilterType_SGA.SelectedItem.ToString();
            FilterValue = textBoxValue_SGA.Text.Trim();

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
