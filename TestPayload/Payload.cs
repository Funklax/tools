using System;
using System.Threading;

namespace Payload
{
    class Payload
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Payload working!");
                Thread.Sleep(5000); // wait for 5 seconds
            }
        }
    }
}