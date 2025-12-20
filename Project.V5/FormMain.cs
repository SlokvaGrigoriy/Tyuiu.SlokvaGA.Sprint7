using Project.V5.Lib;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace Project.V5
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            dataGridViewMain_SGA.ColumnCount = 50;
            dataGridViewMain_SGA.Rows.Add(50);
            for (int i = 0; i < 50; i++)
            {
                dataGridViewMain_SGA.Columns[i].Width = 25;
            }
            panelButtons_SGA.Visible = false;
            panelGrid_SGA.Visible = false;
            panelChart_SGA.Visible = false;
            openFileDialog_SGA.Filter = "Значения, разделённые запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
            saveFileDialog_SGA.Filter = "Значения, разделённые запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
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
            MaximizeBox = false;
            WindowState = FormWindowState.Maximized;
            panelButtons_SGA.Visible = true;
            panelGrid_SGA.Visible = true;
            panelChart_SGA.Visible = true;
            MaximizeBox = Enabled;
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void buttonHelp_SGA_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа разработана 21.12.2025", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBoxSearch_SGA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        private void PerformSearch()
        {
            string searchText = textBoxSearch_SGA.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                ClearHighlights();
                return;
            }

            // Сброс предыдущего выделения
            ClearHighlights();

            // Поиск точного совпадения
            bool found = false;
            for (int i = 0; i < dataGridViewMain_SGA.Rows.Count; i++)
            {
                if (dataGridViewMain_SGA.Rows[i].IsNewRow)
                    continue;

                for (int j = 0; j < dataGridViewMain_SGA.Columns.Count; j++)
                {
                    var cell = dataGridViewMain_SGA.Rows[i].Cells[j];
                    if (cell.Value != null)
                    {
                        string cellValue = cell.Value.ToString();

                        // Точное совпадение (учитывая пробелы)
                        if (cellValue.Trim().Equals(searchText, StringComparison.OrdinalIgnoreCase))
                        {
                            cell.Style.BackColor = Color.Yellow;
                            found = true;

                            // Прокручиваем к найденной ячейке
                            dataGridViewMain_SGA.CurrentCell = cell;
                            dataGridViewMain_SGA.FirstDisplayedScrollingRowIndex = i;
                        }
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show($"Текст '{searchText}' не найден", "Поиск");
            }
        }

        private void ClearHighlights()
        {
            foreach (DataGridViewRow row in dataGridViewMain_SGA.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = SystemColors.Window;
                    cell.Style.ForeColor = SystemColors.WindowText;
                }
            }
        }

        private void toolStripButtonOpen_SGA_Click(object sender, EventArgs e)
        {
            DataService ds = new DataService();
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    Title = "Выберите CSV файл",
                    RestoreDirectory = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[,] data = ds.LoadFromDataFile(openFileDialog.FileName);

                    // Очистка DataGridView
                    dataGridViewMain_SGA.Rows.Clear();
                    dataGridViewMain_SGA.Columns.Clear();

                    int rows = data.GetLength(0);
                    int cols = data.GetLength(1);

                    // Создание столбцов
                    for (int j = 0; j < cols; j++)
                    {
                        // Используем первую строку как заголовки столбцов
                        dataGridViewMain_SGA.Columns.Add($"Column{j}", data[0, j]);
                    }

                    // Заполнение строк (начиная со второй, если первая - заголовки)
                    for (int i = 1; i < rows; i++)
                    {
                        string[] rowData = new string[cols];
                        for (int j = 0; j < cols; j++)
                        {
                            rowData[j] = data[i, j];
                        }
                        dataGridViewMain_SGA.Rows.Add(rowData);
                    }

                    // Автоматическая ширина столбцов
                    dataGridViewMain_SGA.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    MessageBox.Show($"Загружено строк: {rows - 1}, столбцов: {cols}",
                                  "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonSave_SGA_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Сохранить файл"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveDialog.FileName, false, new System.Text.UTF8Encoding(true)))
                    {
                        // Заголовки
                        for (int i = 0; i < dataGridViewMain_SGA.Columns.Count; i++)
                        {
                            sw.Write(dataGridViewMain_SGA.Columns[i].HeaderText);
                            if (i < dataGridViewMain_SGA.Columns.Count - 1)
                                sw.Write(";");
                        }
                        sw.WriteLine();

                        // Данные
                        for (int row = 0; row < dataGridViewMain_SGA.Rows.Count; row++)
                        {
                            // Пропускаем пустую строку для добавления новых записей
                            if (dataGridViewMain_SGA.Rows[row].IsNewRow)
                                continue;

                            for (int col = 0; col < dataGridViewMain_SGA.Columns.Count; col++)
                            {
                                object value = dataGridViewMain_SGA.Rows[row].Cells[col].Value;
                                sw.Write(value?.ToString() ?? "");

                                if (col < dataGridViewMain_SGA.Columns.Count - 1)
                                    sw.Write(";");
                            }
                            sw.WriteLine();
                        }
                    }

                    MessageBox.Show("Файл сохранен успешно!", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                }
            }
        }

        private void toolStripButtonDel_SGA_Click(object sender, EventArgs e)
        {
            dataGridViewMain_SGA.Rows.Clear();
            dataGridViewMain_SGA.Columns.Clear();
            dataGridViewMain_SGA.ColumnCount = 50;
            dataGridViewMain_SGA.Rows.Add(50);
            for (int i = 0; i < 50; i++)
            {
                dataGridViewMain_SGA.Columns[i].Width = 25;
            }
            textBoxAVG_SGA.Clear();
            textBoxMax_SGA.Clear();
            textBoxMin_SGA.Clear();
            textBoxSum_SGA.Clear();
        }

        private void toolStripButtonDone_SGA_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, есть ли выделенные ячейки
                if (dataGridViewMain_SGA.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Выделите ячейки с числовыми значениями!", "Информация",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Список для хранения числовых значений
                List<double> numericValues = new List<double>();
                int textCellsCount = 0; // Счетчик текстовых ячеек

                // Проходим по всем выделенным ячейкам
                foreach (DataGridViewCell cell in dataGridViewMain_SGA.SelectedCells)
                {
                    if (cell.Value != null && !cell.IsInEditMode)
                    {
                        string cellValue = cell.Value.ToString().Trim();

                        // Пробуем преобразовать в число
                        if (double.TryParse(cellValue, out double numericValue))
                        {
                            numericValues.Add(numericValue);
                        }
                        else
                        {
                            // Проверяем, содержит ли ячейка буквы (текст)
                            bool containsLetters = cellValue.Any(char.IsLetter);
                            if (containsLetters)
                            {
                                textCellsCount++; // Увеличиваем счетчик текстовых ячеек
                                continue; // Пропускаем текстовые ячейки
                            }

                            // Пробуем извлечь числа из строки (если нет букв)
                            // Убираем все нецифровые символы, кроме точки и минуса
                            string cleanedValue = new string(cellValue
                                .Where(c => char.IsDigit(c) || c == '.' || c == ',' || c == '-')
                                .ToArray());

                            // Заменяем запятую на точку для корректного парсинга
                            cleanedValue = cleanedValue.Replace(',', '.');

                            if (double.TryParse(cleanedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double cleanedNumericValue))
                            {
                                numericValues.Add(cleanedNumericValue);
                            }
                        }
                    }
                }

                // Проверяем, нашли ли мы числовые значения
                if (numericValues.Count == 0)
                {
                    if (textCellsCount > 0)
                    {
                        MessageBox.Show($"В выделенных ячейках обнаружен текст. \n" +
                                      $"Текстовых ячеек: {textCellsCount}\n" +
                                      $"Числовых ячеек: 0", "Информация",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не найдено числовых значений в выделенных ячейках!", "Информация",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Очищаем TextBox'ы
                    textBoxMin_SGA.Clear();
                    textBoxMax_SGA.Clear();
                    textBoxSum_SGA.Clear();
                    textBoxAVG_SGA.Clear();
                    return;
                }

                // Вычисляем статистику
                double minValue = numericValues.Min();
                double maxValue = numericValues.Max();
                double sumValue = numericValues.Sum();
                double averageValue = numericValues.Average();

                // Выводим результаты в TextBox'ы
                textBoxMin_SGA.Text = minValue.ToString("N2");
                textBoxMax_SGA.Text = maxValue.ToString("N2");
                textBoxSum_SGA.Text = sumValue.ToString("N2");
                textBoxAVG_SGA.Text = averageValue.ToString("N2");

                // Показываем информацию о текстовых ячейках
                if (textCellsCount > 0)
                {
                    MessageBox.Show($"Результаты рассчитаны по {numericValues.Count} числовым ячейкам.\n" +
                                  $"Пропущено {textCellsCount} текстовых ячеек.",
                                  "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислении: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}