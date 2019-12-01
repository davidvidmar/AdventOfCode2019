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

        public static IEnumerable<Int32> ReadAsInt(string v)
        {
            return from q in File.ReadAllLines("input.txt") select Convert.ToInt32(q);
        }
    }
}
