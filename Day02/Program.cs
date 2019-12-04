using System;
using System.Linq;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main()
        {
            Utils.WriteStart(2);
            Utils.WritePart(1);

            Process(new int[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, true);
            Console.WriteLine();
            Process(new int[] { 1, 0, 0, 0, 99 }, true);
            Console.WriteLine();
            Process(new int[] { 2, 3, 0, 3, 99 }, true);
            Console.WriteLine();
            Process(new int[] { 2, 4, 4, 5, 99, 0 }, true);
            Console.WriteLine();
            Process(new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, true);
            Console.WriteLine();

            var input = Utils.ReadInputAsIntArray().ToArray();
            var program = input;
            program[1] = 12;
            program[2] = 2;
            Process(program);

            Utils.WriteResult(input[0]);

            Utils.WritePart(2);

            for (int i = 0; i < 100; i++)
            {
                Console.Write($"{i}: ");
                for (int j = 0; j < 100; j++)
                {
                    Console.Write(".");
                    program = Utils.ReadInputAsIntArray().ToArray();
                    program[1] = i;
                    program[2] = j;
                    Process(program);
                    if (program[0] == 19690720) { break; }
                }
                Console.WriteLine();
                if (program[0] == 19690720) { break; }                
            }
            Console.WriteLine();

            Utils.WriteResult($"{program[0]}, verb = {program[1]}, noun = {program[2]}, r = {100 * program[1] + program[2]}");

            Utils.Wait();
        }

        static void Process(int[] program, bool print = false)
        {
            int i = 0;

            while (program[i] != 99)
            {
                if (print) Console.Write(program[i] + ": ");
                switch (program[i])
                {
                    case 1:                                      
                        program[program[i + 3]] = program[program[i + 1]] + program[program[i + 2]];
                        break;
                    case 2:
                        program[program[i + 3]] = program[program[i + 1]] * program[program[i + 2]];
                        break;
                    default:
                        throw new InvalidOperationException($"opcode = {program[i]}");
                }
                if (print) Print(program);
                i += 4;
            }
        }

        static void Print(int[] program)
        {            
            foreach (var code in program)
            {
                Console.Write($"{code} ");
            }
            Console.WriteLine();
        }
    }
}
