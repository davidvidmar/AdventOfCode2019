using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main()
        {
            Utils.WriteStart(1);            
            
            Utils.WritePart(1);

            // For a mass of 12, divide by 3 and round down to get 4, then subtract 2 to get 2.
            Console.WriteLine($"12 -> {CalculateFuel(12)} = 2");
            // For a mass of 14, dividing by 3 and rounding down still yields 4, so the fuel required is also 2.
            Console.WriteLine($"14 -> {CalculateFuel(14)} = 2");
            // For a mass of 1969, the fuel required is 654.
            Console.WriteLine($"1969 -> {CalculateFuel(1969)} = 654");
            // For a mass of 100756, the fuel required is 33583
            Console.WriteLine($"100756 -> {CalculateFuel(100756)} = 33583");

            var input = Utils.ReadInputAsIntLines();

            var sum = 0;
            foreach (var i in input)
            {
                sum += CalculateFuel(i);
            }

            Utils.WriteResult(sum);            

            // part 2
            Utils.WritePart(2);

            Console.WriteLine($"14 -> {CalculateFuelRecursive(14)} = 2");
            Console.WriteLine($"1969 -> {CalculateFuelRecursive(1969)} = 966");
            Console.WriteLine($"100756 -> {CalculateFuelRecursive(100756)} = 50346");

            var sum2 = 0;
            foreach (var i in input)
            {
                sum2 += CalculateFuelRecursive(i);
            }

            Utils.WriteResult(sum2);

            Utils.Wait();            
        }

        // Fuel required to launch a given module is based on its mass. 
        // Specifically, to find the fuel required for a module, 
        // take its mass, divide by three, round down, and subtract 2.
        private static int CalculateFuel(int mass)
        {
            return (int)Math.Floor(mass / 3.0) - 2;
        }

        // For each module mass, calculate its fuel and add it to the total. 
        // Then, treat the fuel amount you just calculated as the input mass and 
        // repeat the process, continuing until a fuel requirement is zero or negative.
        private static int CalculateFuelRecursive(int mass)
        {            
            var fuelMass = CalculateFuel(mass);
            Console.WriteLine($" + {fuelMass}");
            if (fuelMass > 0)
                fuelMass += CalculateFuelRecursive(fuelMass);
                
            return fuelMass > 0 ? fuelMass : 0;
        }
    }
}