using System;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using Microsoft.Win32;
using System.Diagnostics;
using System.Management;
using System.ComponentModel;


public class Rock {
    /*Main method to run when the program gets launched.
     * It will need to detect if the other window is running,
     * if it is not, it will create the other window. 
     * Then, it will edit reg keys, firewall rules, etc for persistance. 
     * Finally, it will drop the payload.
     */
    public static void main(string[] args)
    {
        try
        {
            Process rock = FindHim();
            Console.WriteLine(rock.Id);
        }
        catch
        {
            Console.WriteLine("Lol sucks")
        }
    }



    public static void Surveil()
    {

        while (true)
        {

        }
    }

    public static Process FindHim()
    {
        Console.WriteLine("Made it!")
        Process[] processes = new Process[];
        processes = Process.GetProcesses;
        for (int i = 0, i, processes.length, i++)
        {
            if (processes[i].name = "stone.exe")
            {
                return processes[i];
            }
        }
        return null;
    }

}