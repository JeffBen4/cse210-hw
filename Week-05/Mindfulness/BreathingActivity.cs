using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void Execute()
    {
        var end = DateTime.UtcNow.AddSeconds(DurationSeconds);
        bool inhale = true;
        while (DateTime.UtcNow < end)
        {
            if (inhale)
            {
                Console.Write("Breathe in... ");
                Countdown(4);
                Console.WriteLine();
            }
            else
            {
                Console.Write("Breathe out... ");
                Countdown(6);
                Console.WriteLine();
            }
            inhale = !inhale;
        }
    }
}
