using System;
using System.Collections.Generic;
using System.IO;


public class Program
{
    static void Main(string[] args)
    {
        //Entry test = new Entry("10/15/2026", "What was the best part of my day?", "Finished my homework.");
//test.Display();
        Journal myJournal = new Journal();
        Console.WriteLine("Welcome to your Journal Program!");
        Console.Write("What is your name? ");
        myJournal._name = Console.ReadLine();
        DisplayMenu();
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            while (choice != 5)
            {
                switch (choice)
                {
                    case 1:
                        PromptGenerator promptGenerator = new PromptGenerator();
                        string prompt = promptGenerator.GetRandomPrompt();
                        Console.WriteLine(prompt);
                        Console.Write("Enter your response: ");
                        string response = Console.ReadLine();
                        Entry entry = new Entry("", prompt, response);
                        myJournal._entries.Add(entry);
                        break;
                    case 2:
                        myJournal.DisplayJournal();
                        break;
                    case 3:
                        Console.Write("Enter the filename to save the journal: ");
                        string saveFilename = Console.ReadLine();
                        myJournal.SaveJournal(saveFilename);
                        Console.WriteLine($"Journal saved to: {Path.GetFullPath(saveFilename)}");
                        break;
                    case 4:
                        Console.Write("Enter the filename to load the journal: ");
                        string loadFilename = Console.ReadLine();
                        myJournal.LoadJournal(loadFilename);
                        Console.WriteLine($"Journal loaded from: {Path.GetFullPath(loadFilename)}");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                DisplayMenu();
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    choice = 0;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Exiting the program.");
        }

    }

    static void DisplayMenu()
    {
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal");
        Console.WriteLine("4. Load the journal");
        Console.WriteLine("5. Quit");
    }
}

public class Journal
{
    public List<Entry> _entries = new List<Entry>();
    public void DisplayJournal()
    {
        Console.WriteLine($"{_name}'s Journal");
        Console.WriteLine("--------------------");
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }
    public void SaveJournal(string filename)
    {
        filename = Path.Combine(Directory.GetCurrentDirectory(), filename);
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_name);
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry._date);
                writer.WriteLine(entry._prompt);
                writer.WriteLine(entry._response);
            }
        }
    }
    public void LoadJournal(string filename)
    {
        filename = Path.Combine(Directory.GetCurrentDirectory(), filename);
        using (StreamReader reader = new StreamReader(filename))
        {
            _name = reader.ReadLine();
            _entries.Clear();
            while (!reader.EndOfStream)
            {
                Entry entry = new Entry("", "", "");
                entry._date = reader.ReadLine();
                entry._prompt = reader.ReadLine();
                entry._response = reader.ReadLine();
                _entries.Add(entry);
            }
        }
    }
    public string? _name;
}

public class Entry
{
    public string _date;
    public string _prompt;
    public string _response;

    public Entry(string date, string prompt, string response)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
        DateTime currentDate = DateTime.Now;
        _date = currentDate.ToString("MM/dd/yyyy");
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");
    }
}

public class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "What was the best part of your day?",
        "What are you grateful for today?",
        "Who was the best part of your day and why? How can you be more like them tomorrow?",
        "What is one thing you learned today?",
        "Describe a challenge you faced today and how you overcame it.",
        "What made you smile today?",
        "How did you feel as you worshipped Christ today? What can you do to feel that way more often?",
        "What is one thing you can do to improve your relationship with God?",
        "What is one thing you want to improve on tomorrow?",
        "What is a goal you have for the next week?",
        "What is a positive affirmation you can tell yourself today?",
        "What is one thing you can do to make tomorrow better than today?",
        "What is one thing you can do to help someone else today?"
    };

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }
}