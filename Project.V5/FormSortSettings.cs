using System;
using System.Windows.Forms;

namespace Project.V5
{
    public partial class FormSortSettings : Form
    {
        private DataGridView dataGridView;

        public string SelectedColumn { get; private set; }
        public string SortOrder { get; private set; }

        public FormSortSettings(DataGridView dgv)
        {
            InitializeComponent();
            dataGridView = dgv;
        }

        private void FormSortSettings_Load(object sender, EventArgs e)
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

            // Устанавливаем порядок сортировки по умолчанию
            radioButtonAsc_SGA.Checked = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxColumn_SGA.SelectedItem == null)
            {
                MessageBox.Show("Выберите столбец для сортировки!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectedColumn = comboBoxColumn_SGA.SelectedItem.ToString();
            SortOrder = radioButtonAsc_SGA.Checked ? "ASC" : "DESC";

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