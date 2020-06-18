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
        static int sum = 10000;

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
                    int j = 1;
                    for (int i = 1; i <= clients1; i++)
                    {
                        Thread t1 = new Thread(Withdraw);
                        t1.Name = "First ATM";
                        t1.Start();
                        t1.Join();

                        while(j<=clients2)
                        {
                            j++;
                            Thread t2 = new Thread(Withdraw);
                            t2.Name = "Second ATM";
                            t2.Start();
                            t2.Join();
                            
                            if(i<clients1)
                            {
                                break;
                            }
                        }
                    }
                }

                input = Console.ReadLine();
            } while (input != "~");

            
        }

        static void Withdraw()
        {
            int withdrawal = r.Next(100, 200);

            lock (theLock)
            {
                if (sum - withdrawal >= 0)
                {
                    sum -= withdrawal;
                    Console.WriteLine(Thread.CurrentThread.Name + "Remaining sum in a bank: " + sum);
                }
                else
                {
                    Console.WriteLine("Withdrawl sum is larger than sum remaining.");
                }
            }
        }
    }
}
