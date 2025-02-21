using System;
using System.Diagnostics;
using System.Threading;

namespace WatchdogBeta
{
    class Beta
    {
        static void Main(string[] args)
        {
            // Start the RuleChange process in a separate thread.
            Thread ruleChangeThread = new Thread(() =>
            {
                try
                {
                    Process ruleChangeProcess = Process.Start("RuleChange.exe");
                    // Optionally wait for it to exit
                    ruleChangeProcess.WaitForExit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error starting RuleChange process: " + ex.Message);
                }
            });
            ruleChangeThread.Start();

            // Now, continuously check if WatchdogAlpha is running every 10 seconds.
            while (true)
            {
                Process[] alphaProcesses = Process.GetProcessesByName("WatchdogAlpha");
                if (alphaProcesses.Length == 0)
                {
                    Console.WriteLine("WatchdogAlpha not running. Starting WatchdogAlpha...");
                    try
                    {
                        Process.Start("WatchdogAlpha.exe");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error starting WatchdogAlpha: " + ex.Message);
                    }
                }
                Thread.Sleep(10000); // wait for 10 seconds before checking again
            }
        }
    }
}