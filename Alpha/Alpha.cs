using System;
using System.Diagnostics;
using System.Threading;

namespace WatchdogAlpha
{
    class Alpha
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Check if Payload is running by looking for processes named "Payload"
                Process[] payloadProcesses = Process.GetProcessesByName("Payload");
                if (payloadProcesses.Length == 0)
                {
                    Console.WriteLine("Payload not running. Starting Payload...");
                    try
                    {
                        Process.Start("Payload.exe");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error starting Payload: " + ex.Message);
                    }
                }
                Thread.Sleep(10000); // wait for 10 seconds before checking again
            }
        }
    }
}