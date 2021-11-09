using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab21
{
    class Program
    {
        static int[,] array = new int[10, 10];//Создаем двумерный массив размером 10ч10. По умолчанию он заполнен нулями.
        static object locker = new object();
        static object locker1 = new object();
        static void Main(string[] args)
        {
            while (array[9,0]==0)//Здесь я "схитрил") Т.к. садовники двигаются в левый нижний угол массива, то индекс [9,0] будет заполнен последним
            {                    //в этот "угол" придет первым тот садовник, который первым делает "ход".
                Thread gad1 = new Thread(gardener1);
                Thread gad2 = new Thread(gardener2);

                gad1.Start();
                gad2.Start();
            }

            for (int i = 0; i < 10; i++)//Выведем на экран массив
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        static void gardener1()
        {
            lock (locker)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (array[i, j] == 0)
                        {
                            array[i, j] = 1;
                            Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        static void gardener2()
        {
            lock (locker1)
            {
                for (int i = 9; i > 0; i--)
                {
                    for (int j = 9; j > 0; j--)
                    {
                        if (array[j, i] == 0)
                        {
                            array[j, i] = 5;
                            Thread.Sleep(100);
                        }
                    }
                }
            }
        }
    }
}
