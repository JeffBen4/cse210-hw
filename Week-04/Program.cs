using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Create videos
        Video video1 = new Video("Learning C#", "CodeMaster", 420);
        Video video2 = new Video("10 Visual Studio Tips", "DevTips", 315);
        Video video3 = new Video("OOP Fundamentals", "LearnTech", 600);

        // Add comments to first video
        video1.AddComment(new Comment("Anna", "Great video! It helped me a lot."));
        video1.AddComment(new Comment("Luis", "Very clear explanation."));
        video1.AddComment(new Comment("Peter", "Can you make one about interfaces?"));

        // Add comments to second video
        video2.AddComment(new Comment("Martha", "I didn’t know about that shortcut, awesome!"));
        video2.AddComment(new Comment("Charles", "Please upload more videos like this."));
        video2.AddComment(new Comment("Julia", "Loved it! I’ll share it with my friends."));

        // Add comments to third video
        video3.AddComment(new Comment("Sophia", "The class concept was explained so well."));
        video3.AddComment(new Comment("Michael", "Thanks for using practical examples."));
        video3.AddComment(new Comment("Andrew", "Excellent educational content."));

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video information
        Console.WriteLine("===== YouTube Video Information =====");
        foreach (Video v in videos)
        {
            v.Display();
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
