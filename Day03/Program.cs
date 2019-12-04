using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    class Program
    {
        private static readonly char _start = 'O';
        private static readonly char _cross = 'X';

        private static readonly char _path1 = '·';
        private static readonly char _path2 = '@';

        private static readonly int _sizeX = 25000;
        private static readonly int _sizeY = 15000;

        //private static readonly int _sizeX = 50;
        //private static readonly int _sizeY = 70;

        private static readonly int _offset = 1000000;

        static void Main()
        {
            Utils.WriteStart(3);

            //Utils.WritePart(1);

            //Console.WriteLine("Example 1: ");
            //Console.WriteLine("Result: " +
            //    Process("R8,U5,L5,D3".Split(','),
            //            "U7,R6,D4,L4".Split(','), 100, 100, false) + " = 6\n");

            //Console.WriteLine("Example 2: ");
            //Console.WriteLine("Result: " +
            //    Process("R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(','),
            //            "U62,R66,U55,R34,D71,R55,D58,R83".Split(','), 10, 30, false) + " = 159\n");

            //Console.WriteLine("Example 3: ");
            //Console.WriteLine("Result: " +
            //    Process("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51".Split(','),
            //            "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split(','), 5000, 5000) + " = 135\n");

            //Console.WriteLine("Real thing: ");
            var input = Utils.ReadInputAsStringArrays().ToArray();
            //var result = Process(input[0], input[1], 5000, 5000, false);
            //Utils.WriteResult(result);

            Utils.WritePart(2);

            Console.WriteLine("Example 1: ");
            Console.WriteLine("Result: " +
                Process2("R8,U5,L5,D3".Split(','),
                         "U7,R6,D4,L4".Split(','), 10, 10, false) + " = 30\n");

            Console.WriteLine("Example 2: ");
            Console.WriteLine("Result: " +
                Process2("R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(','),
                        "U62,R66,U55,R34,D71,R55,D58,R83".Split(','), 10, 30, false) + " = 610\n");

            Console.WriteLine("Example 3: ");
            Console.WriteLine("Result: " +
                Process2("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51".Split(','),
                        "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split(','), 5000, 5000) + " = 410\n");

            var result = Process2(input[0], input[1], 5000, 5000, false);
            Utils.WriteResult(result);

            Utils.Wait();            
        }

        #region Part1

        private static int Process(string[] wire1, string[] wire2, int startX, int startY, bool print = false)
        {
            var board = new char[_sizeX, _sizeY];

            TraceWire(board, wire1, startX, startY, _path1, print);
            TraceWire(board, wire2, startX, startY, _path2, print);
            
            return FindFirstIntersection(board, startX, startY, print);
        }

        private static void TraceWire(char[,] board, string[] wire, int startX, int startY, char c, bool print = false)
        {
            var position = new int[2];

            position[0] = startX;
            position[1] = startY;
            board[position[0], position[1]] = _start;

            foreach (string move in wire)
            {
                if (print) Console.WriteLine(move);                

                var direction = move[0];
                var length = Convert.ToInt32(move.Substring(1));

                switch (direction)
                {
                    case 'R':
                        for (int i = 1; i < length; i++)
                            SetField(board, position[0] + i, position[1], c);
                        position[0] = position[0] + length;
                        break;
                    case 'L':
                        for (int i = 1; i < length; i++)
                            SetField(board, position[0] - length + i, position[1], c);
                        position[0] = position[0] - length;
                        break;
                    case 'U':
                        for (int i = 1; i < length; i++)
                            SetField(board, position[0], position[1] + i, c);
                        position[1] = position[1] + length;
                        break;
                    case 'D':
                        for (int i = 1; i < length; i++)
                            SetField(board, position[0], position[1] - length + i, c);
                        position[1] = position[1] - length;
                        break;
                    default: throw new InvalidOperationException(move);
                }
                board[position[0], position[1]] = c;                
            }
            if (print) Print(board);
            if (print) Console.ReadKey();
        }

        private static void SetField(char[,] board, int x, int y, char c)
        {
            if (board[x, y] == c || board[x, y] == _start) return;
            if (board[x, y] != new char()) c = _cross;
            board[x, y] = c;
        }

        static int FindFirstIntersection(char[,] board, int startX, int startY, bool print = false)
        {
            int result = Int32.MaxValue;
            int max = Math.Max(board.GetLength(0), board.GetLength(1));
            while (result == Int32.MaxValue)
            {
                for (int i = 0; i < max; i++)
                {
                    for (int q = -i; q <= i; q++)
                    {
                        CheckResult(ref result, board, startX, startY, startX + q, startY - i);
                        CheckResult(ref result, board, startX, startY, startX + q, startY + i);
                        CheckResult(ref result, board, startX, startY, startX - i, startY + q);
                        CheckResult(ref result, board, startX, startY, startX + i, startY + q);
                    }
                    if (result < i) break;
                }
            }
            return result;
        }

        private static void CheckResult(ref int result, char[,] board, int startX, int startY, int x, int y)
        {
            if (x < 0 || y < 0 || x >= board.GetLength(0) || y >= board.GetLength(1)) return;
            if (board[x, y] == _cross)
            {
                if (result > Math.Abs(x - startX) + Math.Abs(y - startY))
                {
                    result = Math.Abs(x - startX) + Math.Abs(y - startY);
                }
                Console.WriteLine($"x = {x}, y = {y}, diff = {Math.Abs(x - startX) + Math.Abs(y - startY)}");
            }
            board[x, y] = '/';
        }

        #endregion

        #region Part2

        static int result2 = Int32.MaxValue;

        private static int Process2(string[] wire1, string[] wire2, int startX, int startY, bool print = false)
        {
            var board = new int[_sizeX, _sizeY];

            result2 = Int32.MaxValue;

            TraceWire2(board, wire1, startX, startY, 1, print);
            TraceWire2(board, wire2, startX, startY, -1, print);
            //Save(board);
            //return FindWireLength(board, startX, startY, print);
            return result2;
        }

        private static void TraceWire2(int[,] board, string[] wire, int startX, int startY, int sign, bool print = false)
        {
            var position = new int[2];

            position[0] = startX;
            position[1] = startY;
            board[position[0], position[1]] = 1;
            var l = 1;

            foreach (string move in wire)
            {
                if (print) Console.WriteLine(move);

                var direction = move[0];
                var length = Convert.ToInt32(move.Substring(1));

                switch (direction)
                {
                    case 'R':
                        for (int i = 1; i < length; i++)
                            SetField2(board, position[0] + i, position[1], ++l * sign);
                        position[0] = position[0] + length;
                        break;
                    case 'L':
                        for (int i = length - 1; i > 0; i--)
                            SetField2(board, position[0] - length + i, position[1], ++l * sign);
                        position[0] = position[0] - length;
                        break;
                    case 'U':
                        for (int i = 1; i < length; i++)
                            SetField2(board, position[0], position[1] + i, ++l * sign);
                        position[1] = position[1] + length;
                        break;
                    case 'D':
                        for (int i = length - 1; i > 0; i--)
                            SetField2(board, position[0], position[1] - length + i, ++l * sign);
                        position[1] = position[1] - length;
                        break;
                    default: throw new InvalidOperationException(move);
                }
                board[position[0], position[1]] = ++l * sign;
            }
            // if (print) Print(board);
            // if (print) Console.ReadKey();
        }

        private static void SetField2(int[,] board, int x, int y, int l)
        {
            if (Math.Sign(board[x, y]) == Math.Sign(l)) return;
            if (board[x, y] > 0)
            {
                l = ((board[x, y] - 1) + -(l + 1));                
                if (result2 > l)
                {
                    result2 = l;
                    Console.WriteLine($"{l}");
                }
                l = l * _offset;
            }
            board[x, y] = l;
            //Print2(board);
            //Console.ReadKey();
        }

        //static int FindWireLength(int[,] board, int startX, int startY, bool print = false)
        //{
        //    int result = Int32.MaxValue;
        //    int max = Math.Max(board.GetLength(0), board.GetLength(1));
        //    while (result == Int32.MaxValue)
        //    {
        //        for (int i = 0; i < max; i++)
        //        {
        //            for (int q = -i; q <= i; q++)
        //            {
        //                CheckResult2(ref result, board, startX, startY, startX + q, startY - i);
        //                CheckResult2(ref result, board, startX, startY, startX + q, startY + i);
        //                CheckResult2(ref result, board, startX, startY, startX - i, startY + q);
        //                CheckResult2(ref result, board, startX, startY, startX + i, startY + q);
        //            }
        //            //if (result < i) break;
        //        }
        //    }
        //    return result;
        //}

        //private static void CheckResult2(ref int result, int[,] board, int startX, int startY, int x, int y)
        //{
        //    if (x < 0 || y < 0 || x >= board.GetLength(0) || y >= board.GetLength(1)) return;
        //    if (board[x, y] > _offset)
        //    {
        //        if (result > board[x, y] / _offset)
        //        {
        //            result = board[x, y] / _offset;
        //        }
        //        Console.WriteLine($"x = {x}, y = {y}, len = {board[x, y] / _offset}");
        //    }
        //    board[x, y] = '/';
        //}

        #endregion

        #region Utils

        static void Print(char[,] board)
        {
            Console.Clear();
            var sb = new StringBuilder();
            for (int x = 0; x < board.GetLength(0); x++)
                sb.Append(x.ToString().Substring(x.ToString().Length - 1));

            Console.WriteLine(sb.ToString());

            for (int y = board.GetLength(1) - 1; y >= 0; y--)
            {
                sb.Clear();
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    sb.Append(board[x, y]);
                }
                Console.WriteLine(sb);
            }
        }

        static void Print2(int[,] board)
        {
            Console.Clear();
            var sb = new StringBuilder();
            for (int x = 0; x < board.GetLength(0); x++)
                sb.Append(x.ToString().Substring(x.ToString().Length - 1));

            Console.WriteLine(sb.ToString());

            for (int y = board.GetLength(1) - 1; y >= 0; y--)
            {
                sb.Clear();
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    sb.Append(board[x, y].ToString("000"));
                }
                Console.WriteLine(sb);
            }
        }

        static void Save(char[,] board, string filename = "output.txt")
        {            
            var sb = new StringBuilder();
            //for (int x = 0; x < board.GetLength(0); x++)
            //    sb.Append(x.ToString().Substring(x.ToString().Length - 1));

            var maxX = board.GetLength(0);
            var maxY = board.GetLength(1);
            char empty = new char();

            for (int y = maxY - 1; y >= 0; y--)
            {
                for (int x = 0; x < maxX; x++)
                {
                    sb.Append(board[x, y] == empty ? ' ' : board[x, y]);
                }
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(filename, sb.ToString());
        }

        #endregion
    }
}
