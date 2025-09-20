using System;

class Program
{
    static void Main(string[] args)
    {
        var rng = new Random();
        string playAgain;

        do
        {
            int magic = rng.Next(1, 101);
            int guesses = 0;
            int guess = int.MinValue;

            while (guess != magic)
            {
                Console.Write("What is your guess? ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out guess))
                {
                    Console.WriteLine("Please enter a whole number.");
                    continue;
                }

                guesses++;
                if (guess < magic) Console.WriteLine("Higher");
                else if (guess > magic) Console.WriteLine("Lower");
                else Console.WriteLine("You guessed it!");
            }

            Console.WriteLine($"Guesses: {guesses}");
            Console.Write("Do you want to play again? ");
            playAgain = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
        } while (playAgain == "yes");
    }
}

