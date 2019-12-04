using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main()
        {
            Utils.WriteStart(4);
            Utils.WritePart(1);

            Console.WriteLine(CheckPassword("111111"));
            Console.WriteLine(CheckPassword("223450"));
            Console.WriteLine(CheckPassword("223450"));

            int result = 0;
            for (int i = 136818; i <= 685979; i++)
            {
                if (CheckPassword(i.ToString())) result++;
            }
            Utils.WriteResult(result);

            Utils.WritePart(2);

            Console.WriteLine(CheckPassword2("112233"));
            Console.WriteLine(CheckPassword2("123444"));
            Console.WriteLine(CheckPassword2("111122"));

            result = 0;
            for (int i = 136818; i <= 685979; i++)
            {
                if (CheckPassword2(i.ToString())) result++;
            }
            Utils.WriteResult(result);

            Utils.Wait();
        }        

        private static bool CheckPassword(string v)
        {
            if (!NeverDec(v)) return false;
            if (!AtLeastTwoSame(v)) return false;
            return true;
        }

        private static bool CheckPassword2(string v)
        {
            if (!NeverDec(v)) return false;
            //if (!AtLeastTwoSame(v)) return false;
            if (!ExactlyTwoSame(v)) return false;
            return true;
        }

        private static bool NeverDec(string v)
        {
            for (int i = 1; i < v.Length; i++)
                if (v[i] < v[i - 1]) return false;
            return true;
        }

        private static bool AtLeastTwoSame(string v)
        {
            for (int i = 1; i < v.Length; i++)
                if (v[i] == v[i - 1]) return true;
            return false;
        }

        private static bool ExactlyTwoSame(string v)
        {
            for (int i = 0; i < v.Length - 1; i++)
            {
                var l = 1;
                while (i < v.Length - 1 && v[i + 1] == v[i]) { l++; i++; }
                if (l == 2) return true;
            }
            return false;
        }
    }
}
