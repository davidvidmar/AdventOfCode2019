using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public static class Utils
    {
        public static void WriteStart(int day)
        {
            Console.WriteLine($"Advent of Code 2019 - Day {day}");
        }

        public static void WriteResult(int value)
        {
            WriteResult(value.ToString());
        }

        public static void WriteResult(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nResult = {value}\n");
            Console.ResetColor();
        }

        public static void WritePart(int part)
        {
            Console.ForegroundColor = ConsoleColor.White;            
            Console.WriteLine($"\nPart {part}\n");
            Console.ResetColor();
        }

        public static void Wait()
        {            
            Console.WriteLine("Press any key...");
            Console.ReadKey();         
        }

        public static IEnumerable<Int32> ReadInputAsIntLines(string filename = "input.txt")
        {
            return from q in File.ReadAllLines(filename) select Convert.ToInt32(q);
        }

        public static IEnumerable<Int32> ReadInputAsIntArray(string filename = "input.txt")
        {
            return from q in File.ReadAllText(filename).Split(',') select Convert.ToInt32(q);
        }
    }
}
