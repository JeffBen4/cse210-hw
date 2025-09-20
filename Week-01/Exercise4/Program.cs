using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        var numbers = new List<int>();

        while (true)
        {
            Console.Write("Enter number: ");
            string s = Console.ReadLine();
            if (!int.TryParse(s, out int n)) { continue; }
            if (n == 0) break;
            numbers.Add(n);
        }

        if (numbers.Count == 0)
        {
            Console.WriteLine("The sum is: 0");
            Console.WriteLine("The average is: 0");
            Console.WriteLine("The largest number is: 0");
            return;
        }

        long sum = 0;
        int max = numbers[0];
        foreach (int x in numbers)
        {
            sum += x;
            if (x > max) max = x;
        }

        double avg = sum / (double)numbers.Count;

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {avg}");
        Console.WriteLine($"The largest number is: {max}");

        int smallestPositive = int.MaxValue;
        foreach (int x in numbers)
        {
            if (x > 0 && x < smallestPositive) smallestPositive = x;
        }
        if (smallestPositive != int.MaxValue)
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");

        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int x in numbers) Console.WriteLine(x);
    }
}
