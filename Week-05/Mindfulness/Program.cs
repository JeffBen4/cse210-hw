using System;

/*
Exceeds requirements:
- Non-repeating prompt bank per session for Reflection and Listing.
- Session summary counters for each activity type displayed on exit.
*/

class Program
{
    static int _breathCount = 0;
    static int _reflectCount = 0;
    static int _listCount = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option (1-4): ");
            var choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                new BreathingActivity().Run();
                _breathCount++;
            }
            else if (choice == "2")
            {
                new ReflectionActivity().Run();
                _reflectCount++;
            }
            else if (choice == "3")
            {
                new ListingActivity().Run();
                _listCount++;
            }
            else if (choice == "4")
            {
                Console.WriteLine("Session summary:");
                Console.WriteLine($"Breathing sessions:  {_breathCount}");
                Console.WriteLine($"Reflection sessions: {_reflectCount}");
                Console.WriteLine($"Listing sessions:    {_listCount}");
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option.\n");
            }
        }
    }
}
