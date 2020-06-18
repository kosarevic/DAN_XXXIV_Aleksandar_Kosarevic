using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    /// <summary>
    /// Application simulates use of two ATMs in a bank.
    /// </summary>
    class Program
    {
        //Static random is made to make sure random numbers remain unique.
        static readonly Random r = new Random();
        //Dummy object for purpose of locking same method used between multiple threads.
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
                
                //Validation for user inputs.
                if (success && clients1 >= 0 && clients2 >= 0)
                {
                    int j = 1;
                    //Following is the implementation of nested loops
                    //First loop creates and initiates first generated thread.
                    for (int i = 1; i <= clients1; i++)
                    {
                        //Thread sleep is set to minimal value in order to better simulate line of clients in application.
                        //If set to 0 or missing, sometimes happens that previous member of the line comes in front of intended one.
                        Thread.Sleep(1);
                        Thread t1 = new Thread(Withdraw);
                        t1.Name = string.Format("Client {0} on first ATM ", i);
                        t1.Start();
                        //Sencond loop crates and initiates second generated thread.
                        while (j <= clients2)
                        {
                            Thread.Sleep(1);
                            Thread t2 = new Thread(Withdraw);
                            t2.Name = string.Format("Client {0} on second ATM ", j);

                            t2.Start();
                            j++;
                            //Loop breaks here while first loop has clients in the line, othervise it continues untill second line is empty.
                            if (i < clients1)
                            {
                                break;
                            }
                        }
                    }
                    //Additional loop made, in case user inserts 0 members in the first line.
                    //Because of that, nested loop made prviously will not initiate at all.
                    if (clients1 == 0)
                    {
                        while (j <= clients2)
                        {
                            Thread t2 = new Thread(Withdraw);
                            t2.Name = string.Format("Client {0} on second ATM ", j);
                            t2.Start();
                            j++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ivalid input, please try again.");
                }
                //Pause in execution added so that menu for the user waits for final threads to complete.
                Thread.Sleep(30);

                Console.WriteLine();
                Console.WriteLine("Input any key to repeat, or input ~ to exit application.");
                input = Console.ReadLine();

            } while (input != "~");


        }

        /// <summary>
        /// Method responsible for reducing total amount present in the bank, for each client respectivly.
        /// </summary>
        static void Withdraw()
        {
            //Random value generated for each client.
            int withdrawal = r.Next(100, 10000);
            //Lock added for purpose of deniying access to two threads simultaniously.
            lock (theLock)
            {
                if (sum - withdrawal >= 0)
                {
                    sum -= withdrawal;
                    Console.WriteLine(Thread.CurrentThread.Name +"raises "+ withdrawal + ", remaining sum in a bank: " + sum);
                }
                else
                {
                    Console.WriteLine(Thread.CurrentThread.Name + "tries to raise {0} but it is larger than sum remaining.", withdrawal);
                }
            }
        }
    }
}
