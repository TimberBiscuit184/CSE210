using System;

class Program
{
    public static void Main(string[] args)
{
    bool running = true;
    while (running)
    {
        DisplayMenu();
        string choice = Console.ReadLine();
        Activity activity;

        switch (choice)
        {
            case "1": activity = new Breathing(); break;
            case "2": activity = new Reflection(); break;
            case "3": activity = new Listing(); break;
            case "4":
                running = false;
                Console.WriteLine("Exiting the program. Goodbye!");
                continue;
            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                continue;
        }

        activity.DisplayStartMessage();
        activity.RunActivity();
        activity.DisplayEndMessage();

        Console.WriteLine();
        Console.WriteLine("Press Enter to return to the menu.");
        Console.ReadLine();
    }
}
    private static void DisplayMenu()
    {
        Console.WriteLine("Menu Options:");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");
        Console.WriteLine("4. Exit");
        Console.Write("Please select an option (1-4): ");
    }
}

abstract class Activity
{
    private string _name;
    private string _description;
    protected int _duration;
    
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }
    public void DisplayStartMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name} Activity!");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();

        _duration = AskForDuration();

        Console.Clear();
        Console.WriteLine("Get Ready...");
        Spinner(3);
        Console.WriteLine();
    }
    public void DisplayEndMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        Spinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed the {_name} Activity, lasting {_duration} seconds.");
        Spinner(3);
    }
    protected void Spinner(int seconds)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        for (int i = 0; i < 10 * seconds; i++)
        {
            Console.Write(spinner[i % spinner.Length]);
            System.Threading.Thread.Sleep(100);
            Console.Write("\b");
        }
    }
    protected void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            System.Threading.Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }
    protected string GetRandom(List<string> list)
    {
        Random random = new Random();
        int index = random.Next(list.Count);
        return list[index];
    }
    private int AskForDuration()
{
    Console.Write("Enter the requested duration of your activity in seconds: ");
    string? input = Console.ReadLine();
    if (int.TryParse(input, out int duration))
    {
        return duration;
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid number.");
        return AskForDuration();
    }
}
    public abstract void RunActivity();
}

class Reflection: Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you overcame a challenge.",
        "Reflect on a moment of personal growth.",
        "Consider a time when you helped someone in need."
    };
    private List<string> _questions = new List<string>
    {
        "What did you learn from this experience?",
        "How did this experience shape your perspective?",
        "What would you do differently if faced with a similar situation?"
    };
    
    public Reflection() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }
    
    public override void RunActivity()
    {
        Console.WriteLine("Consider the following: ");
        Console.WriteLine(GetRandom(_prompts));
        Console.WriteLine("When you are ready, press Enter to continue.");
        Console.ReadLine();
        
        Console.WriteLine("Now ponder on each of the following questions as they related to this experience.");
        Console.Write("You may begin in: ");
        Countdown(5);
        Console.Clear();
 
        DateTime end = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < end)
        {
            Console.Write($"> {GetRandom(_questions)} ");
            Spinner(10);
            Console.WriteLine();
        }
    }
}
class Breathing: Activity
{
    public Breathing()
        : base("Breathing",
               "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void RunActivity()
    {
        DateTime end = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < end)
        {
            AnimateBreath("Breathe in... ", 4, true);
            AnimateBreath("Breathe out...", 6, false);
        }
        Console.WriteLine();
    }

    private void AnimateBreath(string label, int seconds, bool growing)
    {
        const int barWidth = 20;
        const int stepsPerSecond = 10;
        int totalSteps = seconds * stepsPerSecond;

        for (int step = 0; step <= totalSteps; step++)
        {
            double progress = (double)step / totalSteps;
            int filled = (int)Math.Round(barWidth * (growing ? progress : 1 - progress));
            int secondsLeft = (int)Math.Ceiling((totalSteps - step) / (double)stepsPerSecond);

            string bar = new string('#', filled) + new string(' ', barWidth - filled);
            Console.Write($"\r{label} [{bar}] {secondsLeft,2}");
            Thread.Sleep(1000 / stepsPerSecond);
        }
        Console.WriteLine();
    }
}
class Listing: Activity
{
    private List<string> _prompts;

    public Listing()
        : base("Listing Activity",
               "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
    }

    public override void RunActivity()
    {
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {GetRandom(_prompts)} ---");
        Console.WriteLine();
        Console.Write("You may begin in: ");
        Countdown(5);
        Console.WriteLine();

        int count = 0;
        DateTime end = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < end)
        {
            Console.Write("> ");
            string? entry = Console.ReadLine();

            if (entry == null)
            {
                break; // input stream closed
            }
            if (entry.Trim() != "")
            {
                count++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {count} items!");
    }
}