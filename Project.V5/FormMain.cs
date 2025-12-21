using Project.V5.Lib;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project.V5
{
    public partial class FormMain : Form
    {
        // Фоновая обработка для операций
        private BackgroundWorker operationsWorker = new BackgroundWorker();
        private BackgroundWorker searchWorker = new BackgroundWorker(); // Отдельный для поиска

        public FormMain()
        {
            InitializeComponent();

            // Настройка фонового работника для основных операций
            operationsWorker.WorkerReportsProgress = true;
            operationsWorker.WorkerSupportsCancellation = false;
            operationsWorker.DoWork += OperationsWorker_DoWork_SGA;
            operationsWorker.ProgressChanged += OperationsWorker_ProgressChanged_SGA;
            operationsWorker.RunWorkerCompleted += OperationsWorker_RunWorkerCompleted_SGA;


            // Настройка фонового работника для поиска
            searchWorker.WorkerReportsProgress = true;
            searchWorker.WorkerSupportsCancellation = false;

            searchWorker.DoWork += SearchWorker_DoWork_SGA;
            searchWorker.ProgressChanged += SearchWorker_ProgressChanged_SGA;
            searchWorker.RunWorkerCompleted += SearchWorker_RunWorkerCompleted_SGA;


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

            // Прогресс-бар всегда видим
            progressBar_SGA.Visible = true;
            progressBar_SGA.Style = ProgressBarStyle.Marquee;
            progressBar_SGA.Minimum = 0;
            progressBar_SGA.Maximum = 100;
            progressBar_SGA.Value = 0;
        }

        // ========== МЕТОДЫ ДЛЯ УПРАВЛЕНИЯ ПРОГРЕСС-БАРОМ ==========

        private void ShowProgress_SGA(string message = "Выполнение операции...")
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowProgress_SGA(message)));
                return;
            }

            progressBar_SGA.Style = ProgressBarStyle.Marquee;
            progressBar_SGA.Value = 50;
            toolStripStatusLabel_SGA.Text = message;
            Application.DoEvents();
        }

        private void UpdateProgress_SGA(int percentage, string message = "")
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgress_SGA(percentage, message)));
                return;
            }

            progressBar_SGA.Style = ProgressBarStyle.Continuous;
            progressBar_SGA.Value = Math.Min(percentage, 100);
            if (!string.IsNullOrEmpty(message))
                toolStripStatusLabel_SGA.Text = message;
            Application.DoEvents();
        }

        private void HideProgress_SGA()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(HideProgress_SGA));
                return;
            }

            progressBar_SGA.Value = 0;
            toolStripStatusLabel_SGA.Text = "Готово";
        }

        private void ShowProgressForSearch_SGA()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ShowProgressForSearch_SGA));
                return;
            }

            progressBar_SGA.Style = ProgressBarStyle.Marquee;
            progressBar_SGA.Value = 50;
            toolStripStatusLabel_SGA.Text = "Поиск...";
            Application.DoEvents();
        }

        // ========== МЕТОДЫ ДЛЯ ОСНОВНЫХ ОПЕРАЦИЙ ==========

        private void OperationsWorker_DoWork_SGA(object? sender, DoWorkEventArgs e)
        {
            var args = e.Argument as Tuple<string, object?>;
            if (args == null) return;

            string operation = args.Item1;
            object? data = args.Item2;

            switch (operation)
            {
                case "OPEN_FILE":
                    OpenFileOperation_SGA(data as Tuple<DataService, string>);
                    break;
                case "SAVE_FILE":
                    SaveFileOperation_SGA(data as Tuple<string, StreamWriter>);
                    break;
                case "CALCULATE":
                    CalculateOperation_SGA();
                    break;
                case "FILTER":
                    FilterOperation_SGA(data as Tuple<string, string, string>);
                    break;
                case "SORT":
                    SortOperation_SGA(data as Tuple<string, string>);
                    break;
                case "BUILD_CHART":
                    BuildChartOperation_SGA(data as Tuple<string, string>);
                    break;
            }
        }

        private void OperationsWorker_ProgressChanged_SGA(object? sender, ProgressChangedEventArgs e)
        {
            UpdateProgress_SGA(e.ProgressPercentage);
        }

        private void OperationsWorker_RunWorkerCompleted_SGA(object? sender, RunWorkerCompletedEventArgs e)
        {
            HideProgress_SGA();

            if (e.Error != null)
            {
                MessageBox.Show($"Ошибка: {e.Error.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenFileOperation_SGA(Tuple<DataService, string>? data)
        {
            if (data == null) return;

            DataService ds = data.Item1;
            string filePath = data.Item2;

            try
            {

                operationsWorker.ReportProgress(10, "Загрузка файла...");
                string[,] fileData = ds.LoadFromDataFile(filePath);

                operationsWorker.ReportProgress(30, "Очистка таблицы...");
                Invoke(new Action(() =>
                {
                    dataGridViewMain_SGA.Rows.Clear();
                    dataGridViewMain_SGA.Columns.Clear();
                }));

                int rows = fileData.GetLength(0);
                int cols = fileData.GetLength(1);

                operationsWorker.ReportProgress(40, "Создание столбцов...");
                Invoke(new Action(() =>
                {
                    for (int j = 0; j < cols; j++)
                    {
                        dataGridViewMain_SGA.Columns.Add($"Column{j}", fileData[0, j]);
                    }
                }));

                operationsWorker.ReportProgress(50, "Заполнение данных...");
                for (int i = 1; i < rows; i++)
                {
                    int currentRow = i;
                    Invoke(new Action(() =>
                    {
                        string[] rowData = new string[cols];
                        for (int j = 0; j < cols; j++)
                        {
                            rowData[j] = fileData[currentRow, j];
                        }
                        dataGridViewMain_SGA.Rows.Add(rowData);
                    }));

                    // Обновляем прогресс
                    int progress = 50 + (int)((double)i / rows * 40);
                    operationsWorker.ReportProgress(progress, $"Загрузка строки {i} из {rows}");
                }

                operationsWorker.ReportProgress(95, "Настройка интерфейса...");
                Invoke(new Action(() =>
                {
                    dataGridViewMain_SGA.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }));

                operationsWorker.ReportProgress(100, "Завершено");

                Invoke(new Action(() =>
                {
                    MessageBox.Show($"Загружено строк: {rows - 1}, столбцов: {cols}",
                                  "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при открытии файла: {ex.Message}");
            }
        }

        private void SaveFileOperation_SGA(Tuple<string, StreamWriter>? data)
        {
            if (data == null) return;

            string filePath = data.Item1;
            StreamWriter sw = data.Item2;

            try
            {
                int totalRows = 0;
                Invoke(new Action(() =>
                {
                    totalRows = dataGridViewMain_SGA.Rows.Count;
                }));

                // Заголовки
                operationsWorker.ReportProgress(10, "Сохранение заголовков...");
                Invoke(new Action(() =>
                {
                    for (int i = 0; i < dataGridViewMain_SGA.Columns.Count; i++)
                    {
                        sw.Write(dataGridViewMain_SGA.Columns[i].HeaderText);
                        if (i < dataGridViewMain_SGA.Columns.Count - 1)
                            sw.Write(";");
                    }
                    sw.WriteLine();
                }));

                operationsWorker.ReportProgress(20, "Сохранение данных...");
                int savedRows = 0;

                Invoke(new Action(() =>
                {
                    for (int row = 0; row < totalRows; row++)
                    {
                        if (dataGridViewMain_SGA.Rows[row].IsNewRow)
                            continue;

                        for (int col = 0; col < dataGridViewMain_SGA.Columns.Count; col++)
                        {
                            object? value = dataGridViewMain_SGA.Rows[row].Cells[col].Value;
                            sw.Write(value?.ToString() ?? "");
                            if (col < dataGridViewMain_SGA.Columns.Count - 1)
                                sw.Write(";");
                        }
                        sw.WriteLine();

                        savedRows++;
                        int progress = 20 + (int)((double)savedRows / totalRows * 70);
                        operationsWorker.ReportProgress(progress, $"Сохранено строк: {savedRows}");
                    }
                }));

                operationsWorker.ReportProgress(100, "Сохранение завершено");
                sw.Close();

                Invoke(new Action(() =>
                {
                    MessageBox.Show("Файл сохранен успешно!", "Успех");
                }));
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при сохранении файла: {ex.Message}");
            }
        }

        private void CalculateOperation_SGA()
        {
            try
            {
                operationsWorker.ReportProgress(10, "Проверка выделенных ячеек...");

                int selectedCellsCount = 0;
                Invoke(new Action(() =>
                {
                    selectedCellsCount = dataGridViewMain_SGA.SelectedCells.Count;
                }));

                if (selectedCellsCount == 0)
                {
                    throw new Exception("Выделите ячейки с числовыми значениями!");
                }

                operationsWorker.ReportProgress(20, "Анализ данных...");

                List<double> numericValues = new List<double>();
                int textCellsCount = 0;
                int processedCells = 0;

                Invoke(new Action(() =>
                {
                    foreach (DataGridViewCell cell in dataGridViewMain_SGA.SelectedCells)
                    {
                        if (cell.Value != null && !cell.IsInEditMode)
                        {
                            string cellValue = cell.Value.ToString()!.Trim();

                            if (double.TryParse(cellValue, out double numericValue))
                            {
                                numericValues.Add(numericValue);
                            }
                            else
                            {
                                bool containsLetters = cellValue.Any(char.IsLetter);
                                if (containsLetters)
                                {
                                    textCellsCount++;
                                }
                                else
                                {
                                    string cleanedValue = new string(cellValue
                                        .Where(c => char.IsDigit(c) || c == '.' || c == ',' || c == '-')
                                        .ToArray());

                                    cleanedValue = cleanedValue.Replace(',', '.');

                                    if (double.TryParse(cleanedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double cleanedNumericValue))
                                    {
                                        numericValues.Add(cleanedNumericValue);
                                    }
                                }
                            }
                        }

                        processedCells++;
                        int progress = 20 + (int)((double)processedCells / selectedCellsCount * 60);
                        operationsWorker.ReportProgress(progress, $"Обработано ячеек: {processedCells}");
                    }
                }));

                operationsWorker.ReportProgress(85, "Вычисление результатов...");

                if (numericValues.Count == 0)
                {
                    string message = textCellsCount > 0 ?
                        $"В выделенных ячейках обнаружен текст. Текстовых ячеек: {textCellsCount}, Числовых ячеек: 0" :
                        "Не найдено числовых значений в выделенных ячейках!";

                    throw new Exception(message);
                }

                double minValue = numericValues.Min();
                double maxValue = numericValues.Max();
                double sumValue = numericValues.Sum();
                double averageValue = numericValues.Average();

                operationsWorker.ReportProgress(95, "Отображение результатов...");

                Invoke(new Action(() =>
                {
                    textBoxMin_SGA.Text = minValue.ToString("N2");
                    textBoxMax_SGA.Text = maxValue.ToString("N2");
                    textBoxSum_SGA.Text = sumValue.ToString("N2");
                    textBoxAVG_SGA.Text = averageValue.ToString("N2");
                }));

                operationsWorker.ReportProgress(100, "Завершено");

                if (textCellsCount > 0)
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Результаты рассчитаны по {numericValues.Count} числовым ячейкам.\n" +
                                      $"Пропущено {textCellsCount} текстовых ячеек.",
                                      "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при вычислении: {ex.Message}");
            }
        }

        private void FilterOperation_SGA(Tuple<string, string, string>? filterParams)
        {
            if (filterParams == null) return;

            string columnName = filterParams.Item1;
            string filterType = filterParams.Item2;
            string filterValue = filterParams.Item3;

            try
            {
                operationsWorker.ReportProgress(10, "Поиск столбца...");

                int columnIndex = -1;
                Invoke(new Action(() =>
                {
                    for (int i = 0; i < dataGridViewMain_SGA.Columns.Count; i++)
                    {
                        if (dataGridViewMain_SGA.Columns[i].HeaderText == columnName)
                        {
                            columnIndex = i;
                            break;
                        }
                    }
                }));

                if (columnIndex == -1)
                    throw new Exception("Столбец не найден");

                operationsWorker.ReportProgress(20, "Сброс предыдущего фильтра...");
                Invoke(new Action(() =>
                {
                    foreach (DataGridViewRow row in dataGridViewMain_SGA.Rows)
                    {
                        row.Visible = true;
                    }
                }));

                operationsWorker.ReportProgress(30, "Применение фильтра...");

                int totalRows = 0;
                int processedRows = 0;

                Invoke(new Action(() =>
                {
                    totalRows = dataGridViewMain_SGA.Rows.Count;
                }));

                Invoke(new Action(() =>
                {
                    for (int i = 0; i < totalRows; i++)
                    {
                        if (dataGridViewMain_SGA.Rows[i].IsNewRow)
                        {
                            dataGridViewMain_SGA.Rows[i].Visible = true;
                            continue;
                        }

                        string cellValue = dataGridViewMain_SGA.Rows[i].Cells[columnIndex].Value?.ToString() ?? "";
                        bool showRow = false;

                        switch (filterType)
                        {
                            case "Равно":
                                showRow = cellValue.Equals(filterValue, StringComparison.OrdinalIgnoreCase);
                                break;
                            case "Содержит":
                                showRow = cellValue.IndexOf(filterValue, StringComparison.OrdinalIgnoreCase) >= 0;
                                break;
                            case "Начинается с":
                                showRow = cellValue.StartsWith(filterValue, StringComparison.OrdinalIgnoreCase);
                                break;
                            case "Заканчивается на":
                                showRow = cellValue.EndsWith(filterValue, StringComparison.OrdinalIgnoreCase);
                                break;
                            case "Больше":
                                if (double.TryParse(cellValue, out double num1) && double.TryParse(filterValue, out double num2))
                                    showRow = num1 > num2;
                                break;
                            case "Меньше":
                                if (double.TryParse(cellValue, out num1) && double.TryParse(filterValue, out num2))
                                    showRow = num1 < num2;
                                break;
                            case "Не равно":
                                showRow = !cellValue.Equals(filterValue, StringComparison.OrdinalIgnoreCase);
                                break;
                        }

                        dataGridViewMain_SGA.Rows[i].Visible = showRow;

                        processedRows++;
                        int progress = 30 + (int)((double)processedRows / totalRows * 60);
                        operationsWorker.ReportProgress(progress, $"Обработано строк: {processedRows}");
                    }
                }));

                operationsWorker.ReportProgress(95, "Подсчет результатов...");

                int visibleRows = 0;
                Invoke(new Action(() =>
                {
                    visibleRows = dataGridViewMain_SGA.Rows.Cast<DataGridViewRow>()
                        .Count(row => !row.IsNewRow && row.Visible);
                }));

                operationsWorker.ReportProgress(100, "Завершено");

                Invoke(new Action(() =>
                {
                    MessageBox.Show($"Отфильтровано. Показано строк: {visibleRows}",
                                  "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при фильтрации: {ex.Message}");
            }
        }

        private void SortOperation_SGA(Tuple<string, string>? sortParams)
        {
            if (sortParams == null) return;

            string columnName = sortParams.Item1;
            string sortOrder = sortParams.Item2;

            try
            {
                operationsWorker.ReportProgress(10, "Подготовка данных...");

                int columnIndex = -1;
                Invoke(new Action(() =>
                {
                    for (int i = 0; i < dataGridViewMain_SGA.Columns.Count; i++)
                    {
                        if (dataGridViewMain_SGA.Columns[i].HeaderText == columnName)
                        {
                            columnIndex = i;
                            break;
                        }
                    }
                }));

                if (columnIndex == -1)
                    throw new Exception("Столбец не найден");

                operationsWorker.ReportProgress(20, "Сбор данных для сортировки...");

                List<DataGridViewRow> rowsToSort = new List<DataGridViewRow>();
                bool isNumericColumn = true;

                Invoke(new Action(() =>
                {
                    foreach (DataGridViewRow row in dataGridViewMain_SGA.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string cellValue = row.Cells[columnIndex].Value?.ToString() ?? "";

                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            if (!double.TryParse(cellValue, out _))
                            {
                                isNumericColumn = false;
                            }
                        }

                        rowsToSort.Add(row);
                    }
                }));

                operationsWorker.ReportProgress(40, "Сортировка...");

                if (isNumericColumn)
                {
                    if (sortOrder == "ASC")
                    {
                        rowsToSort = rowsToSort.OrderBy(row =>
                        {
                            string val = row.Cells[columnIndex].Value?.ToString() ?? "";
                            if (string.IsNullOrEmpty(val)) return double.MinValue;
                            return double.TryParse(val, out double num) ? num : double.MinValue;
                        }).ToList();
                    }
                    else
                    {
                        rowsToSort = rowsToSort.OrderByDescending(row =>
                        {
                            string val = row.Cells[columnIndex].Value?.ToString() ?? "";
                            if (string.IsNullOrEmpty(val)) return double.MaxValue;
                            return double.TryParse(val, out double num) ? num : double.MaxValue;
                        }).ToList();
                    }
                }
                else
                {
                    if (sortOrder == "ASC")
                    {
                        rowsToSort = rowsToSort.OrderBy(row =>
                            row.Cells[columnIndex].Value?.ToString() ?? "").ToList();
                    }
                    else
                    {
                        rowsToSort = rowsToSort.OrderByDescending(row =>
                            row.Cells[columnIndex].Value?.ToString() ?? "").ToList();
                    }
                }

                operationsWorker.ReportProgress(70, "Создание отсортированного списка...");

                List<object?[]> allRows = new List<object?[]>();
                int processedRows = 0;

                foreach (DataGridViewRow row in rowsToSort)
                {
                    object?[] rowData = new object?[dataGridViewMain_SGA.Columns.Count];
                    for (int i = 0; i < dataGridViewMain_SGA.Columns.Count; i++)
                    {
                        rowData[i] = row.Cells[i].Value;
                    }
                    allRows.Add(rowData);

                    processedRows++;
                    int progress = 70 + (int)((double)processedRows / rowsToSort.Count * 25);
                    operationsWorker.ReportProgress(progress, $"Обработано строк: {processedRows}");
                }

                operationsWorker.ReportProgress(95, "Обновление таблицы...");

                Invoke(new Action(() =>
                {
                    dataGridViewMain_SGA.Rows.Clear();

                    foreach (object?[] rowData in allRows)
                    {
                        dataGridViewMain_SGA.Rows.Add(rowData);
                    }
                }));

                operationsWorker.ReportProgress(100, "Завершено");

                Invoke(new Action(() =>
                {
                    MessageBox.Show($"Данные отсортированы по столбцу '{columnName}' ({sortOrder})",
                                  "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при сортировке: {ex.Message}");
            }
        }

        private void BuildChartOperation_SGA(Tuple<string, string>? chartParams)
        {
            if (chartParams == null) return;

            string chartType = chartParams.Item1;
            string dataType = chartParams.Item2;

            try
            {
                operationsWorker.ReportProgress(10, "Подготовка данных для графика...");

                // Очищаем предыдущий график
                Invoke(new Action(() =>
                {
                    chartGraph_SGA.Series.Clear();
                    chartGraph_SGA.ChartAreas[0].AxisX.Title = "";
                    chartGraph_SGA.ChartAreas[0].AxisY.Title = "";
                }));

                // Создаем новую серию
                Series series = new Series();
                series.ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), chartType);

                operationsWorker.ReportProgress(30, "Сбор данных...");

                if (dataType == "Статистика")
                {
                    // Строим график по статистическим данным из TextBox'ов
                    double[] values = new double[4];
                    Invoke(new Action(() =>
                    {
                        double.TryParse(textBoxMin_SGA.Text, out values[0]);
                        double.TryParse(textBoxMax_SGA.Text, out values[1]);
                        double.TryParse(textBoxSum_SGA.Text, out values[2]);
                        double.TryParse(textBoxAVG_SGA.Text, out values[3]);
                    }));

                    string[] labels = { "Минимум", "Максимум", "Сумма", "Среднее" };
                    for (int i = 0; i < values.Length; i++)
                    {
                        series.Points.AddXY(labels[i], values[i]);
                    }

                    Invoke(new Action(() =>
                    {
                        chartGraph_SGA.Titles[0].Text = "Статистика данных";
                        chartGraph_SGA.ChartAreas[0].AxisX.Title = "Параметр";
                        chartGraph_SGA.ChartAreas[0].AxisY.Title = "Значение";
                    }));
                }
                else if (dataType == "Выделенные данные")
                {
                    // Строим график по выделенным ячейкам
                    List<double> selectedValues = new List<double>();
                    Invoke(new Action(() =>
                    {
                        foreach (DataGridViewCell cell in dataGridViewMain_SGA.SelectedCells)
                        {
                            if (cell.Value != null && double.TryParse(cell.Value.ToString(), out double value))
                            {
                                selectedValues.Add(value);
                            }
                        }
                    }));

                    for (int i = 0; i < selectedValues.Count; i++)
                    {
                        series.Points.AddXY($"Точка {i + 1}", selectedValues[i]);
                    }

                    Invoke(new Action(() =>
                    {
                        chartGraph_SGA.Titles[0].Text = "Выделенные данные";
                        chartGraph_SGA.ChartAreas[0].AxisX.Title = "Позиция";
                        chartGraph_SGA.ChartAreas[0].AxisY.Title = "Значение";
                    }));
                }
                else if (dataType == "Выбранный столбец")
                {
                    // Строим график по выбранному столбцу
                    int columnIndex = 0;
                    Invoke(new Action(() =>
                    {
                        columnIndex = dataGridViewMain_SGA.CurrentCell?.ColumnIndex ?? 0;
                    }));

                    List<double> columnValues = new List<double>();
                    Invoke(new Action(() =>
                    {
                        for (int i = 0; i < dataGridViewMain_SGA.Rows.Count; i++)
                        {
                            if (!dataGridViewMain_SGA.Rows[i].IsNewRow)
                            {
                                if (dataGridViewMain_SGA.Rows[i].Cells[columnIndex].Value != null &&
                                    double.TryParse(dataGridViewMain_SGA.Rows[i].Cells[columnIndex].Value!.ToString(), out double value))
                                {
                                    columnValues.Add(value);
                                }
                            }
                        }
                    }));

                    for (int i = 0; i < columnValues.Count; i++)
                    {
                        series.Points.AddXY($"Строка {i + 1}", columnValues[i]);
                    }

                    string columnName = "";
                    Invoke(new Action(() =>
                    {
                        columnName = dataGridViewMain_SGA.Columns[columnIndex].HeaderText;
                        chartGraph_SGA.Titles[0].Text = $"Данные столбца: {columnName}";
                        chartGraph_SGA.ChartAreas[0].AxisX.Title = "Строка";
                        chartGraph_SGA.ChartAreas[0].AxisY.Title = "Значение";
                    }));
                }

                operationsWorker.ReportProgress(70, "Настройка графика...");

                // Добавляем серию на график
                Invoke(new Action(() =>
                {
                    chartGraph_SGA.Series.Add(series);
                }));

                operationsWorker.ReportProgress(90, "Форматирование...");

                // Настраиваем внешний вид
                Invoke(new Action(() =>
                {
                    chartGraph_SGA.Series[0].Color = Color.SteelBlue;
                    chartGraph_SGA.Series[0].BorderWidth = 2;
                    chartGraph_SGA.ChartAreas[0].AxisX.Interval = 1;
                    chartGraph_SGA.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                }));

                operationsWorker.ReportProgress(100, "График построен");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при построении графика: {ex.Message}");
            }
        }

        // ========== МЕТОДЫ ДЛЯ ПОИСКА ==========

        private void SearchWorker_DoWork_SGA(object? sender, DoWorkEventArgs e)
        {
            var args = e.Argument as Tuple<string?, bool>;
            if (args == null) return;

            string? searchText = args.Item1;
            bool resetOnly = args.Item2;

            try
            {
                if (resetOnly)
                {
                    // Только сброс выделения
                    searchWorker.ReportProgress(10, "Сброс выделения...");
                    ClearHighlightsInBackground_SGA();
                    searchWorker.ReportProgress(100, "Завершено");
                    return;
                }

                searchWorker.ReportProgress(10, "Сброс предыдущего выделения...");
                ClearHighlightsInBackground_SGA();

                searchWorker.ReportProgress(20, "Поиск совпадений...");

                int totalRows = 0;
                int totalCols = 0;

                Invoke(new Action(() =>
                {
                    totalRows = dataGridViewMain_SGA.Rows.Count;
                    totalCols = dataGridViewMain_SGA.Columns.Count;
                }));

                int foundCount = 0;
                DataGridViewCell? firstFoundCell = null;
                int firstFoundRow = -1;

                for (int i = 0; i < totalRows; i++)
                {
                    int currentRow = i;

                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            if (dataGridViewMain_SGA.Rows[currentRow].IsNewRow)
                                return;
                        }));
                    }
                    else
                    {
                        if (dataGridViewMain_SGA.Rows[currentRow].IsNewRow)
                            continue;
                    }

                    for (int j = 0; j < totalCols; j++)
                    {
                        int currentCol = j;

                        string cellValue = "";
                        if (InvokeRequired)
                        {
                            cellValue = (string)Invoke(new Func<string>(() =>
                            {
                                var cell = dataGridViewMain_SGA.Rows[currentRow].Cells[currentCol];
                                return cell.Value?.ToString() ?? "";
                            }))!;
                        }
                        else
                        {
                            cellValue = dataGridViewMain_SGA.Rows[currentRow].Cells[currentCol].Value?.ToString() ?? "";
                        }

                        if (!string.IsNullOrEmpty(cellValue) && !string.IsNullOrEmpty(searchText))
                        {
                            // Точное совпадение (учитывая пробелы)
                            if (cellValue.Trim().Equals(searchText, StringComparison.OrdinalIgnoreCase))
                            {
                                foundCount++;

                                // Выделяем ячейку
                                if (InvokeRequired)
                                {
                                    Invoke(new Action(() =>
                                    {
                                        var cell = dataGridViewMain_SGA.Rows[currentRow].Cells[currentCol];
                                        cell.Style.BackColor = Color.Yellow;

                                        // Запоминаем первую найденную ячейку
                                        if (firstFoundCell == null)
                                        {
                                            firstFoundCell = cell;
                                            firstFoundRow = currentRow;
                                        }
                                    }));
                                }
                                else
                                {
                                    var cell = dataGridViewMain_SGA.Rows[currentRow].Cells[currentCol];
                                    cell.Style.BackColor = Color.Yellow;

                                    if (firstFoundCell == null)
                                    {
                                        firstFoundCell = cell;
                                        firstFoundRow = currentRow;
                                    }
                                }
                            }
                        }
                    }

                    // Обновляем прогресс
                    int progress = 20 + (int)((double)i / totalRows * 70);
                    searchWorker.ReportProgress(progress, $"Поиск в строке {i + 1} из {totalRows}");
                }

                searchWorker.ReportProgress(95, "Прокрутка к результату...");

                // Прокручиваем к первой найденной ячейке
                if (firstFoundCell != null)
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            dataGridViewMain_SGA.CurrentCell = firstFoundCell;
                            dataGridViewMain_SGA.FirstDisplayedScrollingRowIndex = firstFoundRow;
                        }));
                    }
                    else
                    {
                        dataGridViewMain_SGA.CurrentCell = firstFoundCell;
                        dataGridViewMain_SGA.FirstDisplayedScrollingRowIndex = firstFoundRow;
                    }
                }

                searchWorker.ReportProgress(100, "Завершено");

                // Сохраняем результат поиска
                e.Result = new Tuple<int, DataGridViewCell?, string?>(foundCount, firstFoundCell, searchText);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при поиске: {ex.Message}");
            }
        }

        private void ClearHighlightsInBackground_SGA()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ClearHighlightsInBackground_SGA));
                return;
            }

            foreach (DataGridViewRow row in dataGridViewMain_SGA.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = SystemColors.Window;
                    cell.Style.ForeColor = SystemColors.WindowText;
                }
            }
        }

        private void SearchWorker_ProgressChanged_SGA(object? sender, ProgressChangedEventArgs e)
        {
            UpdateProgress_SGA(e.ProgressPercentage);
        }

        private void SearchWorker_RunWorkerCompleted_SGA(object? sender, RunWorkerCompletedEventArgs e)
        {
            HideProgress_SGA();

            if (e.Error != null)
            {
                MessageBox.Show($"Ошибка при поиске: {e.Error.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Если был поиск (не просто сброс), показываем результат
            if (e.Result != null && e.Result is Tuple<int, DataGridViewCell?, string?> result)
            {
                int foundCount = result.Item1;
                string? searchText = result.Item3;

                if (foundCount == 0)
                {
                    MessageBox.Show($"Текст '{searchText}' не найден", "Поиск");
                }
                else
                {
                    MessageBox.Show($"Найдено совпадений: {foundCount}", "Результат поиска",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // ========== ОБНОВЛЕННЫЕ МЕТОДЫ ОПЕРАЦИЙ ==========

        private void toolStripButtonOpen_SGA_Click(object sender, EventArgs e)
        {
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
                    DataService ds = new DataService();
                    var data = Tuple.Create(ds, openFileDialog.FileName);

                    ShowProgress_SGA("Открытие файла...");
                    operationsWorker.RunWorkerAsync(Tuple.Create("OPEN_FILE", (object?)data));
                }
            }
            catch (Exception ex)
            {
                HideProgress_SGA();
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
                    StreamWriter sw = new StreamWriter(saveDialog.FileName, false, new System.Text.UTF8Encoding(true));
                    var data = Tuple.Create(saveDialog.FileName, sw);

                    ShowProgress_SGA("Сохранение файла...");
                    operationsWorker.RunWorkerAsync(Tuple.Create("SAVE_FILE", (object?)data));
                }
                catch (Exception ex)
                {
                    HideProgress_SGA();
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                }
            }
        }

        private void toolStripButtonDone_SGA_Click(object sender, EventArgs e)
        {
            ShowProgress_SGA("Вычисление...");
            operationsWorker.RunWorkerAsync(Tuple.Create("CALCULATE", (object?)null));
        }

        private void toolStripButtonDel_SGA_Click(object sender, EventArgs e)
        {
            try
            {
                ShowProgress_SGA("Очистка таблицы...");

                // Очистка DataGridView
                dataGridViewMain_SGA.Rows.Clear();
                dataGridViewMain_SGA.Columns.Clear();
                dataGridViewMain_SGA.ColumnCount = 50;
                dataGridViewMain_SGA.Rows.Add(50);
                for (int i = 0; i < 50; i++)
                {
                    dataGridViewMain_SGA.Columns[i].Width = 25;
                }

                // Очистка TextBox'ов
                textBoxAVG_SGA.Clear();
                textBoxMax_SGA.Clear();
                textBoxMin_SGA.Clear();
                textBoxSum_SGA.Clear();

                // Очистка графика
                chartGraph_SGA.Series.Clear();

                HideProgress_SGA();
                MessageBox.Show("Данные очищены!", "Информация");
            }
            catch (Exception ex)
            {
                HideProgress_SGA();
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void buttonSort_SGA_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewMain_SGA.Rows.Count == 0 || dataGridViewMain_SGA.Columns.Count == 0)
                {
                    MessageBox.Show("Нет данных для сортировки!", "Информация",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (FormSortSettings sortForm = new FormSortSettings(dataGridViewMain_SGA))
                {
                    if (sortForm.ShowDialog() == DialogResult.OK)
                    {
                        string selectedColumn = sortForm.SelectedColumn;
                        string sortOrder = sortForm.SortOrder;

                        ShowProgress_SGA("Сортировка...");
                        var data = Tuple.Create(selectedColumn, sortOrder);
                        operationsWorker.RunWorkerAsync(Tuple.Create("SORT", (object?)data));
                    }
                }
            }
            catch (Exception ex)
            {
                HideProgress_SGA();
                MessageBox.Show($"Ошибка при сортировке:\n{ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonFilter_SGA_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewMain_SGA.Rows.Count == 0 || dataGridViewMain_SGA.Columns.Count == 0)
                {
                    MessageBox.Show("Нет данных для фильтрации!", "Информация",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (FormFilterSettings_SGA filterForm = new FormFilterSettings_SGA(dataGridViewMain_SGA))
                {
                    if (filterForm.ShowDialog() == DialogResult.OK)
                    {
                        string selectedColumn = filterForm.SelectedColumn;
                        string filterType = filterForm.FilterType;
                        string filterValue = filterForm.FilterValue;

                        ShowProgress_SGA("Фильтрация...");
                        var data = Tuple.Create(selectedColumn, filterType, filterValue);
                        operationsWorker.RunWorkerAsync(Tuple.Create("FILTER", (object?)data));
                    }
                }
            }
            catch (Exception ex)
            {
                HideProgress_SGA();
                MessageBox.Show($"Ошибка при фильтрации:\n{ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== ОБНОВЛЕННЫЙ МЕТОД ПОИСКА ==========

        private void PerformSearch_SGA()
        {
            string searchText = textBoxSearch_SGA.Text.Trim();

            // Если строка пустая - просто сбрасываем выделение
            if (string.IsNullOrEmpty(searchText))
            {
                ShowProgressForSearch_SGA();
                searchWorker.RunWorkerAsync(Tuple.Create((string?)null, true));
                return;
            }

            ShowProgressForSearch_SGA();
            searchWorker.RunWorkerAsync(Tuple.Create(searchText, false));
        }

        // ========== ОСТАВШИЕСЯ МЕТОДЫ ==========

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

            // Вызываем метод адаптации интерфейса
            ResizePanels();
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
                PerformSearch_SGA();
                e.Handled = true;
            }
        }

        private void ClearHighlights_SGA()
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

        // Метод для применения фильтра (синхронная версия для вызова из других мест)
        private void ApplyFilter_SGA(string columnName, string filterType, string filterValue)
        {
            // Получаем индекс столбца
            int columnIndex = -1;
            for (int i = 0; i < dataGridViewMain_SGA.Columns.Count; i++)
            {
                if (dataGridViewMain_SGA.Columns[i].HeaderText == columnName)
                {
                    columnIndex = i;
                    break;
                }
            }

            if (columnIndex == -1)
                return;

            // Показываем все строки перед фильтрацией
            foreach (DataGridViewRow row in dataGridViewMain_SGA.Rows)
            {
                row.Visible = true;
            }

            // Применяем фильтр
            foreach (DataGridViewRow row in dataGridViewMain_SGA.Rows)
            {
                if (row.IsNewRow)
                {
                    row.Visible = true;
                    continue;
                }

                string cellValue = row.Cells[columnIndex].Value?.ToString() ?? "";
                bool showRow = false;

                switch (filterType)
                {
                    case "Равно":
                        showRow = cellValue.Equals(filterValue, StringComparison.OrdinalIgnoreCase);
                        break;

                    case "Содержит":
                        showRow = cellValue.IndexOf(filterValue, StringComparison.OrdinalIgnoreCase) >= 0;
                        break;

                    case "Начинается с":
                        showRow = cellValue.StartsWith(filterValue, StringComparison.OrdinalIgnoreCase);
                        break;

                    case "Заканчивается на":
                        showRow = cellValue.EndsWith(filterValue, StringComparison.OrdinalIgnoreCase);
                        break;

                    case "Больше":
                        if (double.TryParse(cellValue, out double num1) && double.TryParse(filterValue, out double num2))
                            showRow = num1 > num2;
                        break;

                    case "Меньше":
                        if (double.TryParse(cellValue, out num1) && double.TryParse(filterValue, out num2))
                            showRow = num1 < num2;
                        break;

                    case "Не равно":
                        showRow = !cellValue.Equals(filterValue, StringComparison.OrdinalIgnoreCase);
                        break;
                }

                row.Visible = showRow;
            }
        }

        // ========== НОВЫЕ МЕТОДЫ ДЛЯ АДАПТИВНОГО ИНТЕРФЕЙСА ==========

        private void FormMain_Resize(object sender, EventArgs e)
        {
            ResizePanels();
        }

        private void ResizePanels()
        {
            if (panelButtons_SGA.Visible)
            {
                int formWidth = this.ClientSize.Width;
                int formHeight = this.ClientSize.Height;

                // Верхняя панель (кнопки) - фиксированная высота
                panelButtons_SGA.Width = formWidth;
                panelButtons_SGA.Height = 138;

                // Оставшаяся высота для нижних панелей
                int remainingHeight = formHeight - panelButtons_SGA.Height;

                // Левая панель (таблица) - 2/3 ширины
                int gridWidth = (int)(formWidth * 2.0 / 3.0);
                panelGrid_SGA.Location = new Point(0, panelButtons_SGA.Bottom);
                panelGrid_SGA.Size = new Size(gridWidth, remainingHeight);

                // Правая панель (график) - 1/3 ширины
                int chartWidth = formWidth - gridWidth;
                panelChart_SGA.Location = new Point(gridWidth, panelButtons_SGA.Bottom);
                panelChart_SGA.Size = new Size(chartWidth, remainingHeight);

                // Адаптация DataGridView
                dataGridViewMain_SGA.Location = new Point(0, 0);
                dataGridViewMain_SGA.Size = new Size(panelGrid_SGA.Width, panelGrid_SGA.Height);

                // Адаптация Chart
                chartGraph_SGA.Location = new Point(0, 0);
                chartGraph_SGA.Size = new Size(panelChart_SGA.Width, panelChart_SGA.Height);
            }
        }

        // ========== МЕТОДЫ ДЛЯ РАБОТЫ С ГРАФИКОМ ==========

        private void buttonGraph_SGA_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormGraphSettings graphForm = new FormGraphSettings())
                {
                    if (graphForm.ShowDialog() == DialogResult.OK)
                    {
                        ShowProgress_SGA("Построение графика...");
                        var data = Tuple.Create(graphForm.ChartType, graphForm.DataType);
                        operationsWorker.RunWorkerAsync(Tuple.Create("BUILD_CHART", (object?)data));
                    }
                }
            }
            catch (Exception ex)
            {
                HideProgress_SGA();
                MessageBox.Show($"Ошибка при построении графика:\n{ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== ОБРАБОТЧИКИ МЕНЮ ==========

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void новаяТаблицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonDel_SGA_Click(sender, e);
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Настройки программы", "Настройки",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain_SGA.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                Clipboard.SetDataObject(dataGridViewMain_SGA.GetClipboardContent());
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataObject? data = (DataObject?)Clipboard.GetDataObject();
            if (data != null && data.GetDataPresent(DataFormats.Text))
            {
                string? text = data.GetData(DataFormats.Text)?.ToString();
                if (text != null)
                {
                    string[] rows = text.Split('\n');
                    foreach (string row in rows)
                    {
                        if (row.Length > 0)
                        {
                            string[] cells = row.Split('\t');
                            dataGridViewMain_SGA.Rows.Add(cells);
                        }
                    }
                }
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            копироватьToolStripMenuItem_Click(sender, e);
            foreach (DataGridViewCell cell in dataGridViewMain_SGA.SelectedCells)
            {
                cell.Value = "";
            }
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridViewMain_SGA.SelectedCells)
            {
                cell.Value = "";
            }
        }

        private void отображатьСеткуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Меняем состояние сетки на противоположное текущему
            bool showGrid = !отображатьСеткуToolStripMenuItem.Checked;

            dataGridViewMain_SGA.GridColor = showGrid ? SystemColors.ControlDark : dataGridViewMain_SGA.BackColor;
            dataGridViewMain_SGA.CellBorderStyle = showGrid ?
                DataGridViewCellBorderStyle.Single : DataGridViewCellBorderStyle.None;

            // Обновляем состояние галочки
            отображатьСеткуToolStripMenuItem.Checked = showGrid;
        }

        private void показатьПанельСтатистикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Меняем видимость на противоположную
            bool visible = !показатьПанельСтатистикиToolStripMenuItem.Checked;

            labelMin_SGA.Visible = visible;
            labelMax_SGA.Visible = visible;
            labelSum_SGA.Visible = visible;
            labelAVG_SGA.Visible = visible;
            textBoxMin_SGA.Visible = visible;
            textBoxMax_SGA.Visible = visible;
            textBoxSum_SGA.Visible = visible;
            textBoxAVG_SGA.Visible = visible;

            // Обновляем состояние галочки
            показатьПанельСтатистикиToolStripMenuItem.Checked = visible;
        }
        private void показатьПанельИнструментовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool visible = !показатьПанельИнструментовToolStripMenuItem.Checked;

            toolStripButtons_SGA.Visible = visible;

            показатьПанельИнструментовToolStripMenuItem.Checked = visible;
        }
        private void показатьСтрокуСостоянияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool visible = !показатьСтрокуСостоянияToolStripMenuItem.Checked;

            progressBar_SGA.Visible = visible;

            показатьСтрокуСостоянияToolStripMenuItem.Checked = visible;
        }
        private void темнаяТемаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool darkTheme = !темнаяТемаToolStripMenuItem.Checked;
            темнаяТемаToolStripMenuItem.Checked = darkTheme;

            if (darkTheme)
            {
                // Основные цвета темной темы
                Color darkBackground = Color.FromArgb(30, 30, 30);
                Color darkPanel = Color.FromArgb(45, 45, 48);
                Color darkControl = Color.FromArgb(37, 37, 38);
                Color darkText = Color.White;
                Color darkGrid = Color.FromArgb(64, 64, 64);
                Color darkHighlight = Color.FromArgb(62, 62, 66);

                // Основная форма
                this.BackColor = darkBackground;
                this.ForeColor = darkText;

                // Панели
                panelButtons_SGA.BackColor = darkPanel;
                panelGrid_SGA.BackColor = darkPanel;
                panelChart_SGA.BackColor = darkPanel;

                // DataGridView
                dataGridViewMain_SGA.BackgroundColor = darkControl;
                dataGridViewMain_SGA.GridColor = darkGrid;
                dataGridViewMain_SGA.DefaultCellStyle.BackColor = darkControl;
                dataGridViewMain_SGA.DefaultCellStyle.ForeColor = darkText;
                dataGridViewMain_SGA.DefaultCellStyle.SelectionBackColor = darkHighlight;
                dataGridViewMain_SGA.DefaultCellStyle.SelectionForeColor = darkText;
                dataGridViewMain_SGA.ColumnHeadersDefaultCellStyle.BackColor = darkControl;
                dataGridViewMain_SGA.ColumnHeadersDefaultCellStyle.ForeColor = darkText;
                dataGridViewMain_SGA.RowHeadersDefaultCellStyle.BackColor = darkControl;
                dataGridViewMain_SGA.RowHeadersDefaultCellStyle.ForeColor = darkText;
                dataGridViewMain_SGA.EnableHeadersVisualStyles = false;

                // Chart
                chartGraph_SGA.BackColor = darkControl;
                chartGraph_SGA.ChartAreas[0].BackColor = darkControl;
                chartGraph_SGA.ChartAreas[0].AxisX.LineColor = darkText;
                chartGraph_SGA.ChartAreas[0].AxisY.LineColor = darkText;
                chartGraph_SGA.ChartAreas[0].AxisX.MajorGrid.LineColor = darkGrid;
                chartGraph_SGA.ChartAreas[0].AxisY.MajorGrid.LineColor = darkGrid;
                chartGraph_SGA.ChartAreas[0].AxisX.LabelStyle.ForeColor = darkText;
                chartGraph_SGA.ChartAreas[0].AxisY.LabelStyle.ForeColor = darkText;
                chartGraph_SGA.ChartAreas[0].AxisX.TitleForeColor = darkText;
                chartGraph_SGA.ChartAreas[0].AxisY.TitleForeColor = darkText;
                chartGraph_SGA.Titles[0].ForeColor = darkText;
                chartGraph_SGA.ForeColor = darkText;

                // MenuStrip
                menuStripButtons_SGA.BackColor = darkControl;
                menuStripButtons_SGA.ForeColor = darkText;
                menuStripButtons_SGA.RenderMode = ToolStripRenderMode.Professional;
                menuStripButtons_SGA.Renderer = new DarkMenuRenderer();
                новыйФайлToolStripMenuItem.ForeColor = SystemColors.Control;
                открытьToolStripMenuItem.ForeColor = SystemColors.Control;
                сохранитьToolStripMenuItem.ForeColor = SystemColors.Control;
                сохранитьКакToolStripMenuItem.ForeColor = SystemColors.Control;
                печатьToolStripMenuItem.ForeColor = SystemColors.Control;
                предварительныйПросмотрToolStripMenuItem.ForeColor = SystemColors.Control;
                выходToolStripMenuItem.ForeColor = SystemColors.Control;
                отменитьToolStripMenuItem.ForeColor = SystemColors.Control;
                повторитьToolStripMenuItem.ForeColor = SystemColors.Control;
                вырезатьToolStripMenuItem.ForeColor = SystemColors.Control;
                копироватьToolStripMenuItem.ForeColor = SystemColors.Control;
                вставитьToolStripMenuItem.ForeColor = SystemColors.Control;
                выделитьВсеToolStripMenuItem.ForeColor = SystemColors.Control;
                очиститьToolStripMenuItem.ForeColor = SystemColors.Control;
                отображатьСеткуToolStripMenuItem.ForeColor = SystemColors.Control;
                показатьПанельСтатистикиToolStripMenuItem.ForeColor = SystemColors.Control;
                показатьПанельИнструментовToolStripMenuItem.ForeColor = SystemColors.Control;
                показатьСтрокуСостоянияToolStripMenuItem.ForeColor = SystemColors.Control;
                увеличитьШрифтToolStripMenuItem.ForeColor = SystemColors.Control;
                уменьшитьШрифтToolStripMenuItem.ForeColor = SystemColors.Control;
                темнаяТемаToolStripMenuItem.ForeColor = SystemColors.Control;
                содержаниеToolStripMenuItem.ForeColor = SystemColors.Control;
                индексToolStripMenuItem.ForeColor = SystemColors.Control;
                поискToolStripMenuItem.ForeColor = SystemColors.Control;
                оПрограммеToolStripMenuItem.ForeColor = SystemColors.Control;
                руководствоПользователяToolStripMenuItem.ForeColor = SystemColors.Control;
                справкаToolStripMenuItem1.ForeColor = SystemColors.Control;

                // ToolStrip
                toolStripButtons_SGA.BackColor = darkControl;
                toolStripButtons_SGA.ForeColor = darkText;
                toolStripButtons_SGA.RenderMode = ToolStripRenderMode.Professional;
                toolStripButtons_SGA.Renderer = new DarkToolStripRenderer();

                // Кнопки
                foreach (Control control in panelButtons_SGA.Controls)
                {
                    if (control is Button button)
                    {
                        button.BackColor = darkControl;
                        button.ForeColor = darkText;
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderColor = darkGrid;
                    }
                    else if (control is TextBox textBox)
                    {
                        textBox.BackColor = darkControl;
                        textBox.ForeColor = darkText;
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (control is Label label)
                    {
                        label.ForeColor = darkText;
                    }
                }

                // DateTimePicker
                dateTimePicker_SGA.BackColor = darkControl;
                dateTimePicker_SGA.ForeColor = darkText;
                dateTimePicker_SGA.CalendarMonthBackground = darkControl;
                dateTimePicker_SGA.CalendarTitleBackColor = darkControl;
                dateTimePicker_SGA.CalendarTitleForeColor = darkText;
                dateTimePicker_SGA.CalendarTrailingForeColor = Color.Gray;

                // ProgressBar
                progressBar_SGA.BackColor = darkControl;
                progressBar_SGA.ForeColor = Color.SteelBlue;

            }
            else
            {
                // Светлая тема (возврат к стандартным значениям)
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;

                // Панели
                panelButtons_SGA.BackColor = SystemColors.Control;
                panelGrid_SGA.BackColor = SystemColors.Control;
                panelChart_SGA.BackColor = SystemColors.Control;

                // DataGridView
                dataGridViewMain_SGA.BackgroundColor = SystemColors.ControlDark;
                dataGridViewMain_SGA.GridColor = SystemColors.WindowFrame;
                dataGridViewMain_SGA.DefaultCellStyle.BackColor = SystemColors.Window;
                dataGridViewMain_SGA.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                dataGridViewMain_SGA.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                dataGridViewMain_SGA.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
                dataGridViewMain_SGA.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                dataGridViewMain_SGA.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                dataGridViewMain_SGA.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                dataGridViewMain_SGA.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                dataGridViewMain_SGA.EnableHeadersVisualStyles = true;

                // Chart
                chartGraph_SGA.BackColor = SystemColors.Control;
                chartGraph_SGA.ChartAreas[0].BackColor = SystemColors.Control;
                chartGraph_SGA.ChartAreas[0].AxisX.LineColor = Color.Black;
                chartGraph_SGA.ChartAreas[0].AxisY.LineColor = Color.Black;
                chartGraph_SGA.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chartGraph_SGA.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chartGraph_SGA.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                chartGraph_SGA.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                chartGraph_SGA.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                chartGraph_SGA.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
                chartGraph_SGA.Titles[0].ForeColor = Color.Black;
                chartGraph_SGA.ForeColor = Color.Black;

                // MenuStrip и ToolStrip
                menuStripButtons_SGA.BackColor = SystemColors.MenuBar;
                menuStripButtons_SGA.ForeColor = SystemColors.MenuText;
                menuStripButtons_SGA.RenderMode = ToolStripRenderMode.System;
                новыйФайлToolStripMenuItem.ForeColor = SystemColors.ControlText;
                открытьToolStripMenuItem.ForeColor = SystemColors.ControlText;
                сохранитьToolStripMenuItem.ForeColor = SystemColors.ControlText;
                сохранитьКакToolStripMenuItem.ForeColor = SystemColors.ControlText;
                печатьToolStripMenuItem.ForeColor = SystemColors.ControlText;
                предварительныйПросмотрToolStripMenuItem.ForeColor = SystemColors.ControlText;
                выходToolStripMenuItem.ForeColor = SystemColors.ControlText;
                отменитьToolStripMenuItem.ForeColor = SystemColors.ControlText;
                повторитьToolStripMenuItem.ForeColor = SystemColors.ControlText;
                вырезатьToolStripMenuItem.ForeColor = SystemColors.ControlText;
                копироватьToolStripMenuItem.ForeColor = SystemColors.ControlText;
                вставитьToolStripMenuItem.ForeColor = SystemColors.ControlText;
                выделитьВсеToolStripMenuItem.ForeColor = SystemColors.ControlText;
                очиститьToolStripMenuItem.ForeColor = SystemColors.ControlText;
                отображатьСеткуToolStripMenuItem.ForeColor = SystemColors.ControlText;
                показатьПанельСтатистикиToolStripMenuItem.ForeColor = SystemColors.ControlText;
                показатьПанельИнструментовToolStripMenuItem.ForeColor = SystemColors.ControlText;
                показатьСтрокуСостоянияToolStripMenuItem.ForeColor = SystemColors.ControlText;
                увеличитьШрифтToolStripMenuItem.ForeColor = SystemColors.ControlText;
                уменьшитьШрифтToolStripMenuItem.ForeColor = SystemColors.ControlText;
                темнаяТемаToolStripMenuItem.ForeColor = SystemColors.ControlText;
                содержаниеToolStripMenuItem.ForeColor = SystemColors.ControlText;
                индексToolStripMenuItem.ForeColor = SystemColors.ControlText;
                поискToolStripMenuItem.ForeColor = SystemColors.ControlText;
                оПрограммеToolStripMenuItem.ForeColor = SystemColors.ControlText;
                руководствоПользователяToolStripMenuItem.ForeColor = SystemColors.ControlText;
                справкаToolStripMenuItem1.ForeColor = SystemColors.ControlText;

                toolStripButtons_SGA.BackColor = SystemColors.Control;
                toolStripButtons_SGA.ForeColor = SystemColors.ControlText;
                toolStripButtons_SGA.RenderMode = ToolStripRenderMode.System;

                // Восстановление стандартного вида контролов
                foreach (Control control in panelButtons_SGA.Controls)
                {
                    if (control is Button button)
                    {
                        button.BackColor = SystemColors.Control;
                        button.ForeColor = SystemColors.ControlText;
                        button.FlatStyle = FlatStyle.Standard;
                    }
                    else if (control is TextBox textBox)
                    {
                        textBox.BackColor = SystemColors.Window;
                        textBox.ForeColor = SystemColors.WindowText;
                        textBox.BorderStyle = BorderStyle.Fixed3D;
                    }
                    else if (control is Label label)
                    {
                        label.ForeColor = SystemColors.ControlText;
                    }
                }

                // DateTimePicker
                dateTimePicker_SGA.BackColor = SystemColors.Window;
                dateTimePicker_SGA.ForeColor = SystemColors.WindowText;
                dateTimePicker_SGA.CalendarMonthBackground = SystemColors.Window;
                dateTimePicker_SGA.CalendarTitleBackColor = SystemColors.ActiveCaption;
                dateTimePicker_SGA.CalendarTitleForeColor = SystemColors.ActiveCaptionText;
                dateTimePicker_SGA.CalendarTrailingForeColor = SystemColors.GrayText;

                // ProgressBar
                progressBar_SGA.BackColor = SystemColors.Control;
                progressBar_SGA.ForeColor = SystemColors.Highlight;
            }
        }

        // Классы для рендеринга темного меню и тулбара
        public class DarkMenuRenderer : ToolStripProfessionalRenderer
        {
            public DarkMenuRenderer() : base(new DarkMenuColors()) { }
        }

        public class DarkToolStripRenderer : ToolStripProfessionalRenderer
        {
            public DarkToolStripRenderer() : base(new DarkMenuColors()) { }
        }

        public class DarkMenuColors : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin => Color.FromArgb(45, 45, 48);
            public override Color MenuStripGradientEnd => Color.FromArgb(45, 45, 48);
            public override Color MenuItemSelected => Color.FromArgb(62, 62, 66);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(62, 62, 66);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(62, 62, 66);
            public override Color MenuItemBorder => Color.FromArgb(62, 62, 66);
            public override Color MenuBorder => Color.FromArgb(64, 64, 64);
            public override Color MenuItemPressedGradientBegin => Color.FromArgb(45, 45, 48);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(45, 45, 48);
            public override Color ToolStripDropDownBackground => Color.FromArgb(45, 45, 48);
            public override Color ImageMarginGradientBegin => Color.FromArgb(45, 45, 48);
            public override Color ImageMarginGradientEnd => Color.FromArgb(45, 45, 48);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(45, 45, 48);
            public override Color SeparatorDark => Color.FromArgb(64, 64, 64);
            public override Color SeparatorLight => Color.FromArgb(64, 64, 64);
        }

        private void увеличитьШрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Увеличение шрифта
            Font currentFont = dataGridViewMain_SGA.DefaultCellStyle.Font ?? new Font("Microsoft Sans Serif", 8);
            dataGridViewMain_SGA.DefaultCellStyle.Font = new Font(currentFont.FontFamily, currentFont.Size + 1);
        }

        private void уменьшитьШрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Уменьшение шрифта
            Font currentFont = dataGridViewMain_SGA.DefaultCellStyle.Font ?? new Font("Microsoft Sans Serif", 8);
            if (currentFont.Size > 6)
            {
                dataGridViewMain_SGA.DefaultCellStyle.Font = new Font(currentFont.FontFamily, currentFont.Size - 1);
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Оптовая база данных v1.0\n\n" +
                          "Программа для управления и анализа данных оптовой базы.\n" +
                          "Функции:\n" +
                          "- Загрузка и сохранение CSV файлов\n" +
                          "- Сортировка и фильтрация данных\n" +
                          "- Статистический анализ\n" +
                          "- Построение графиков\n\n" +
                          "Разработчик: Слоква Г. А.\n" +
                          "Версия: 1.0.0\n" +
                          "Дата: 21.12.2025",
                          "О программе",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void руководствоПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Руководство пользователя\n\n" +
                          "1. Нажмите 'Начать' для входа в программу\n" +
                          "2. Используйте меню 'Файл' для открытия/сохранения CSV\n" +
                          "3. Выделите ячейки и нажмите 'Выполнить' для анализа\n" +
                          "4. Используйте кнопки 'Сортировка' и 'Фильтрация'\n" +
                          "5. Нажмите 'График' для визуализации данных\n" +
                          "6. Используйте поиск для быстрого нахождения данных\n\n" +
                          "Горячие клавиши:\n" +
                          "Ctrl+O - Открыть файл\n" +
                          "Ctrl+S - Сохранить файл\n" +
                          "Ctrl+C - Копировать\n" +
                          "Ctrl+V - Вставить\n" +
                          "Delete - Удалить",
                          "Руководство пользователя",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://example.com/help",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть справку: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewMain_SGA_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}