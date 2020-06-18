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
            Console.WriteLine("Insert number of clients for first bankomat.");
            bool success = int.TryParse(Console.ReadLine(), out int clients);

            if (success)
            {
                for (int i = 0; i < clients; i++)
                {
                    Thread t = new Thread(Withdraw);
                    t.Start();
                }
            }

            Console.ReadLine();
        }

        static void Withdraw()
        {
            int withdrawal = r.Next(100, 10000);

            lock (theLock)
            {
                if (sum1 - withdrawal >= 0)
                {
                    sum1 -= withdrawal;
                    Console.WriteLine("Remaining sum: " + sum);
                }
                else
                {
                    Console.WriteLine("Withdrawl sum is larger than sum remaining.");
                }
            }
        }
    }
}
