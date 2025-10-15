using System;

public class ReflectionActivity : Activity
{
    private readonly PromptBank _bank = new();
    private static readonly string[] _questions =
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };
    private readonly Random _rng = new();

    public ReflectionActivity()
        : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    protected override void Execute()
    {
        Console.WriteLine();
        var prompt = _bank.NextReflectionPrompt();
        Console.WriteLine($"Consider this prompt:\n> {prompt}\n");
        Console.WriteLine("When you are ready, reflect on the following questions...");
        Spinner(3);
        Console.WriteLine();

        var end = DateTime.UtcNow.AddSeconds(DurationSeconds);
        while (DateTime.UtcNow < end)
        {
            var q = _questions[_rng.Next(_questions.Length)];
            Console.WriteLine(q);
            Spinner(6);
            Console.WriteLine();
        }
    }
}
