using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int grade))
        {
            Console.WriteLine("Invalid input. Please enter a whole number.");
            return;
        }

        string letter;
        if (grade >= 90)      letter = "A";
        else if (grade >= 80) letter = "B";
        else if (grade >= 70) letter = "C";
        else if (grade >= 60) letter = "D";
        else                  letter = "F";

        Console.WriteLine($"Letter grade: {letter}");

        if (grade >= 70)
            Console.WriteLine("Congratulations! You passed the course.");
        else
            Console.WriteLine("Keep trying! You'll do better next time.");
    }
}
