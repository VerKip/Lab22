using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(Sum);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(Max);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            task1.Start();
            Console.ReadKey();
        }


        static int[] GetArray(object o)
        {
            int n=(int) o;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 10);
                Console.Write("{0}", array[i]);
            }
            return array;
        }

        static int Sum(Task<int[]> task)
        {
            int[] array = task.Result;
            int S = 0;
            for (int i = 0; i < array.Count(); i++)
            {
               S+=array[i];
            }
            Console.WriteLine($"Sum is {S}");
            return S;

        }

        static int Max(Task<int[]> task)
        {
            int[] array = task.Result;
            int Max = array[0];
            foreach (int a in array)
            {
                if (a > Max)
                    Max = a;
            }
            Console.WriteLine();
            Console.WriteLine($"Max is {Max}");
            return Max;
        }
    }
}
