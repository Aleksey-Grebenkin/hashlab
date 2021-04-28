using System;

namespace HashConsole
{
    class Program
    {
        static readonly Random rnd = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Введите мат. ожидание:");
            double lambda = 1.0 / Convert.ToDouble(Console.ReadLine());
            var table = new HashTable.HashTable();

            for (int i = 0; i < 100; i++)
            {
                int val = (int)Math.Round(GenerateExp(lambda), 0);
                try
                {
                    var col = table.Add(val);
                    Console.WriteLine($"Добавили число: {val}, кол-во коллизий: {col}, сумма: {table.Count}");
                }
                catch (ArgumentException) { i--; }
            }
        }

        private static double GenerateExp(double lambda)
        {
            return -Math.Log(rnd.NextDouble()) / lambda;
        }
    }
}
