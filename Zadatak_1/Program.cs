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

        static void Main(string[] args)
        {
            int sum = 10000;

            Console.WriteLine("Insert number of clients for first bankomat.");
            bool success = int.TryParse(Console.ReadLine(), out int clients);

            if(success)
            {
                for (int i = 0; i < clients; i++)
                {
                    Thread t = new Thread();
                }
            }
        }

        static void Withdraw()
        {

        }
    }
}
