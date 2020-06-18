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
        private static readonly object theLock = new object();
        static int sum = 10000;

        static void Main(string[] args)
        {
            string input;
            bool success;

            do
            {
                Console.WriteLine("Insert number of clients for first ATM.");
                success = int.TryParse(Console.ReadLine(), out int clients1);

                Console.WriteLine("Insert number of clients for second ATM.");
                success = int.TryParse(Console.ReadLine(), out int clients2);
                Console.WriteLine();

                if (success)
                {
                    int j = 1;
                    for (int i = 1; i <= clients1; i++)
                    {
                        Thread.Sleep(1);
                        Thread t1 = new Thread(Withdraw);
                        t1.Name = string.Format("Client {0} on first ATM raises ", i);
                        t1.Start();
                        
                        while (j <= clients2)
                        {
                            Thread.Sleep(1);
                            Thread t2 = new Thread(Withdraw);
                            t2.Name = string.Format("Client {0} on second ATM raises ", j);

                            t2.Start();
                            j++;

                            if (i < clients1)
                            {
                                break;
                            }
                        }
                    }

                    if(clients1 == 0)
                    {
                        while (j <= clients2)
                        {
                            Thread t2 = new Thread(Withdraw);
                            t2.Name = string.Format("Client {0} on second ATM raises ", j);
                            t2.Start();
                            j++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ivalid input, please try again.");
                }

                Thread.Sleep(10);

                Console.WriteLine();
                Console.WriteLine("Input any key to repeat, or input ~ to exit application.");
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
                    Console.WriteLine(Thread.CurrentThread.Name + withdrawal + ", remaining sum in a bank: " + sum);
                }
                else
                {
                    Console.WriteLine("Withdrawl sum is larger than sum remaining.");
                }
            }
        }
    }
}
