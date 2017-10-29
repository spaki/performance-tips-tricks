using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace PerformanceTipsTricks
{
    class Program
    {
        static void Main(string[] args)
        {
            Benchmark(ConcatStrings, nameof(ConcatStrings), 1000000);
            Benchmark(ConcatStringsOptimized, nameof(ConcatStringsOptimized), 1000000);

            Benchmark(CompareStrings, nameof(CompareStrings), 1000000);
            Benchmark(CompareStringsOptimized1, nameof(CompareStringsOptimized1), 1000000);
            Benchmark(CompareStringsOptimized2, nameof(CompareStringsOptimized2), 1000000);

            Benchmark(ExceptionHandler, nameof(ExceptionHandler), 10);
            Benchmark(ExceptionHandlerOptimized, nameof(ExceptionHandlerOptimized), 10);

            Benchmark(ReadMatrix1, nameof(ReadMatrix1), 10);
            Benchmark(ReadMatrix2, nameof(ReadMatrix2), 10);

            Benchmark(AddToList, nameof(AddToList), 10);
            Benchmark(AddToListOptimized, nameof(AddToListOptimized), 10);

            Benchmark(SumArrayValueWithFor, nameof(SumArrayValueWithFor), 1000000);
            Benchmark(SumArrayValueWithForeach, nameof(SumArrayValueWithForeach), 1000000);

            Benchmark(CompareSwitchAndDictionary1, nameof(CompareSwitchAndDictionary1), 1000000);
            Benchmark(CompareSwitchAndDictionary2, nameof(CompareSwitchAndDictionary2), 1000000);

            Console.WriteLine();
            Console.WriteLine("end...");
            Console.ReadLine();
        }

        static void Benchmark(Action action, string actionName, int maxInteractions)
        {
            var stopwatch = new Stopwatch();

            // stabilizing system
            Thread.Sleep(1000);

            stopwatch.Start();

            // call action N times
            for (int i = 0; i < maxInteractions; i++)
                action();

            stopwatch.Stop();

            // log result
            Console.WriteLine($"{actionName} took {stopwatch.ElapsedMilliseconds} milliseconds");
        }


        static void CompareStrings()
        {
            var result = "aaa".ToLower() == "AAA".ToLower();
        }

        static void CompareStringsOptimized1()
        {
            var result = string.Compare("aaa", "AAA") == 0;
        }

        static void CompareStringsOptimized2()
        {
            var result = "aaa".Equals("AAA", StringComparison.CurrentCultureIgnoreCase);
        }


        static void ConcatStrings()
        {
            var result = "a";
            for (int i = 0; i < 10; i++)
                result += "a";
        }

        static void ConcatStringsOptimized()
        {
            var builder = new StringBuilder("a");

            for (int i = 0; i < 10; i++)
                builder.Append("a");

            var result = builder.ToString();
        }


        static void ExceptionHandler()
        {
            var collection = new[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a" };

            foreach (var item in collection)
            {
                try
                {
                    var value = Convert.ToInt32(item);
                }
                catch { }
            }
        }

        static void ExceptionHandlerOptimized()
        {
            var collection = new[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a" };

            foreach (var item in collection)
                int.TryParse(item, out int value);
        }


        static void ReadMatrix1()
        {
            var size = 10000;
            var matrix = new int[size, size];
            var result = 0;

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (matrix[j, i] > 0)
                        result++;
        }

        static void ReadMatrix2()
        {
            var size = 10000;
            var matrix = new int[size, size];
            var result = 0;

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (matrix[i, j] > 0)
                        result++;
        }

        static void AddToList()
        {
            var items = new int[100000000];
            var list = new List<int>();

            foreach (var item in items)
                list.Add(item);
        }

        static void AddToListOptimized()
        {
            var items = new int[100000000];
            var list = new List<int>();
            list.AddRange(items);
        }

        static void SumArrayValueWithFor()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sum = 0;

            for (var i = 0; i < items.Length; i++)
            {
                sum += items[i];
            }
        }

        static void SumArrayValueWithForeach()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sum = 0;

           foreach(var item in items)
           {
                sum += item;
           }
        }

        static void CompareSwitchAndDictionary1()
        {
            var type = "A";
            var value = string.Empty;

            switch (type)
            {
                case "A":
                    value = "I'm A";
                    break;
                case "B":
                    value = "I'm B";
                    break;
            }
        }

        static void CompareSwitchAndDictionary2()
        {
            var dictionary = new Dictionary<string, string>()
            {
                { "A", "I'm A" },
                { "B", "I'm B" },
            };

            var value = dictionary["A"];
        }
    }
}
