using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TareasMirandaXavier
{
    class LogExample
    {
        static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("Starting...");
            LogExample logExample = new LogExample();
            Console.WriteLine("Step 1: Reading Log Items...");
            var lineCount = logExample.ReadAllLogs();
            Console.WriteLine("{0:n0} Log Items Read", lineCount);
            Console.WriteLine("Step 2: Counting Unique IPs...");
            var ipCount = logExample.CountUniqueIPs();
            Console.WriteLine("Number of unique IPs: " + ipCount);
            Console.WriteLine("Time elapsed: {0:0.0} seconds", Math.Round(stopWatch.ElapsedMilliseconds / 1000.0, 2));
            Console.ReadLine();
        }

        int ReadAllLogs()
        {
            var logReader = new LogReader();
            var linesSeen = 0;
            foreach (var line in logReader)
            {
                var ip = line.GetIP();
                linesSeen++;
            }
            return linesSeen;
        }

        //////complejidad cuadratica
        int CountUniqueIPs()
        {
            var logReader = new LogReader();

            //var ipsSeen = new List<string>();
            //El HashSet es un repositorio donde se guardan registros unicos.
            var ipsSeen = new HashSet<string>();
            foreach (var logLine in logReader) // 0(n)
            {
                var ip = logLine.GetIP();
                if (!ipsSeen.Contains(ip)) // 0(n) contains List -> 0(1) constains HashSet
                {
                    ipsSeen.Add(ip);
                }
            }
            return ipsSeen.Count;
        }

      
    }
}
