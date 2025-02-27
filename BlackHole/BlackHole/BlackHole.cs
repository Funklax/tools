using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace BlackHole
{
    class Program
    {
        // A candidate list of commands that can be added to the "gloop pool"
        private static readonly List<string> candidateCommands = new List<string> { "dir", "ipconfig", "ping", "tracert" };
        private static int wrongAnswerCount = 0;

        static void Main(string[] args)
        {
            Random rand = new Random();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("I've taken your computer hostage, and you must solve a math problem!");

                // Generate two random numbers between 1 and 15.
                int num1 = rand.Next(1, 16);
                int num2 = rand.Next(1, 16);

                // Randomly choose an operator: 0 = addition, 1 = subtraction, 2 = multiplication.
                int opChoice = rand.Next(0, 3);
                char op;
                int correctAnswer;

                switch (opChoice)
                {
                    case 0:
                        op = '+';
                        correctAnswer = num1 + num2;
                        break;
                    case 1:
                        op = '-';
                        correctAnswer = num1 - num2;
                        break;
                    case 2:
                        op = '*';
                        correctAnswer = num1 * num2;
                        break;
                    default:
                        op = '+';
                        correctAnswer = num1 + num2;
                        break;
                }

                Console.WriteLine($"Solve: {num1} {op} {num2} = ?");
                string input = Console.ReadLine();
                bool parsed = int.TryParse(input, out int userAnswer);

                if (parsed && userAnswer == correctAnswer)
                {
                    Console.WriteLine("Nice job!");
                    // Sleep for 2 minutes (120,000 ms)
                    Thread.Sleep(120000);
                }
                else
                {
                    // Filler response for a wrong answer.
                    Console.WriteLine("Wrong answer placeholder message");

                    // Select the next candidate command to add.
                    string commandToAdd = candidateCommands[wrongAnswerCount % candidateCommands.Count];
                    wrongAnswerCount++;

                    try
                    {
                        // Call RuleChange.exe with "add <command>" to add this command to the gloop pool.
                        var startInfo = new ProcessStartInfo
                        {
                            FileName = "RuleChange.exe",
                            Arguments = $"add {commandToAdd}",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        };
                        Process.Start(startInfo)?.WaitForExit();
                        Console.WriteLine($"Added '{commandToAdd}' to the gloop pool.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error calling RuleChange: " + ex.Message);
                    }

                    // Sleep for 5 minutes (300,000 ms)
                    Thread.Sleep(300000);
                }
            }
        }
    }
}
