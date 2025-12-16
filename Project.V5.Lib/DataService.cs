using System;
using System.IO;

namespace Project.V5.Lib
{
    public class DataService
    {
        public string[,] LoadFromDataFile(string path)
        {
            // чтение строк из файла(каждый элемент массива - одна строка из файла)
            string[] lines = File.ReadAllLines(path);

            // rows (строки) = количество строк в файле
            int rows = lines.Length;

            // columns (столбцы) = количество элементов в первой строке, разделённых запятыми
            string[] firstLine = lines[0].Split(',');
            int columns = firstLine.Length;

            // создание матрицы (двумерного массива)
            string[,] matrix = new string[rows, columns];

            // заполнение матрицы
            for (int i = 0; i < rows; i++)
            {
                // разбиваем текущую строку на элементы по запятой
                string[] elem = lines[i].Split(',');

                // внутренний цикл: проходим по столбцам
                for (int j = 0; j < columns; j++)
                {
                    // заполняем ячейку матрицы
                    if (j < elem.Length)
                    {
                        matrix[i, j] = elem[j];
                    }
                    else
                    {
                        // сделано для того,чтобы заполнить пустую клетку пустотой(избежать ошибку)
                        matrix[i, j] = "";
                    }
                }
            }
            return matrix;
        }
    }
}