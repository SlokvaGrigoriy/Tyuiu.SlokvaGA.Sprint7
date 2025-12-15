using Project.V5.Lib;

namespace Project.V5.Test
{
    [TestClass]
    public sealed class DataServiceTest
    {
        [TestMethod]
        public void ValidLoadFromDataFile()
        {
            DataService ds = new DataService();
            string path = @"C:\Users\Input\source\repos\Tyuiu.SlokvaGA.Sprint7\Project.V5\bin\Debug\net8.0-windows\DB.csv";
            string[,] matrix = ds.LoadFromDataFile(path);

            Assert.IsNotNull(matrix, "Матрица не должна быть null");
            Assert.IsTrue(matrix.GetLength(0) > 0, "Количество строк должно быть больше 0");
            Assert.IsTrue(matrix.GetLength(1) > 0, "Количество столбцов должно быть больше 0");
        }
    }
}
