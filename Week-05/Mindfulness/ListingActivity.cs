using System;

public class ListingActivity : Activity
{
    private readonly PromptBank _bank = new();

    public ListingActivity()
        : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void Execute()
    {
        Console.WriteLine();
        var prompt = _bank.NextListingPrompt();
        Console.WriteLine($"List responses for the following prompt:\n> {prompt}\n");
        Console.Write("You will begin in: ");
        Countdown(5);
        Console.WriteLine("\nStart listing. Press Enter after each item.");

        int count = 0;
        var end = DateTime.UtcNow.AddSeconds(DurationSeconds);
        while (DateTime.UtcNow < end)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(line)) count++;
            if (DateTime.UtcNow >= end) break;
        }

        Console.WriteLine($"\nYou listed {count} item(s).");
    }
}
