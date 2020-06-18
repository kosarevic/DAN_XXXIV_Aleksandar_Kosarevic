using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        static readonly Random r = new Random();
        private static object theLock = new object();
        static int sum1 = 10000;
        static int sum2 = 10000;

        static void Main(string[] args)
        {
            string input;
            do
            {
                Console.WriteLine("Insert number of clients for first ATM.");
                bool success = int.TryParse(Console.ReadLine(), out int clients1);

                Console.WriteLine("Inser number of clients for second ATM.");
                success = int.TryParse(Console.ReadLine(), out int clients2);

                if (success)
                {
                    for (int i = 0; i < clients1; i++)
                    {
                        Thread t = new Thread(Withdraw);
                        t.Start();
                    }
                }

                input = Console.ReadLine();
            } while (input != "~");

            
        }

        static void Withdraw()
        {
            int withdrawal = r.Next(100, 10000);

            lock (theLock)
            {
                if (sum1 - withdrawal >= 0)
                {
                    sum1 -= withdrawal;
                    Console.WriteLine("Remaining sum on first ATM: " + sum1);
                }
                else
                {
                    Console.WriteLine("Withdrawl sum is larger than sum remaining.");
                }
            }
        }
    }
}
