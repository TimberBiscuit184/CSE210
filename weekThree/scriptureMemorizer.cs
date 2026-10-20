using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
class Program
{
    static void Main(string[] args)
    {
        DisplayMenu();
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            while (choice != 3)
            {
                switch (choice)
                {
                    case 1:
                        ScriptureLibrary scriptureLibrary = new ScriptureLibrary();
                        Scripture scripture = scriptureLibrary.GetRandomScripture();
                        RunMemorizer(scripture);
                        break;
                    case 2:
                        scripture = GetScriptureFromUser();
                        RunMemorizer(scripture);
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
    private static void DisplayMenu()
    {
        Console.WriteLine("Menu Options:");
        Console.WriteLine("1. Display Random Scripture");
        Console.WriteLine("2. Use an inputted scripture");
        Console.WriteLine("3. Exit");
        Console.Write("Please select an option (1-3): ");

    }
    private static Scripture GetScriptureFromUser()
    {
        Console.Write("Enter the book: ");
        string book = Console.ReadLine();
        Console.Write("Enter the chapter: ");
        int chapter = int.Parse(Console.ReadLine());
        Console.Write("Enter the starting verse: ");
        int startVerse = int.Parse(Console.ReadLine());
        Console.Write("Enter the ending verse (or press Enter for a single verse): ");
        string endVerseInput = Console.ReadLine();
        int endVerse = string.IsNullOrEmpty(endVerseInput) ? startVerse : int.Parse(endVerseInput);
        Console.Write("Enter the scripture text: ");
        string text = Console.ReadLine();
        Reference reference = new Reference(book, chapter, startVerse, endVerse);
        return new Scripture(reference, text);
    }
    private static void RunMemorizer(Scripture scripture)
    {
       Console.Clear();
       Console.WriteLine(scripture.GetDisplayText());
        while (!scripture.IsCompletelyHidden())
        {
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }
            scripture.HideRandomWords(3);
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
        }
        if (scripture.IsCompletelyHidden())
        {
            Console.WriteLine("Congratulations! You've hidden all the words.");
        }
    }

}

class ScriptureLibrary
{
    private List<Scripture> _scriptures;
    private Random _random;
    Scripture scripture1 = new Scripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");
    Scripture scripture2 = new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.");
    Scripture scripture3 = new Scripture(new Reference("Philippians", 4, 13), "I can do all this through him who gives me strength.");
    Scripture scripture4 = new Scripture(new Reference("Alma", 7, 11, 13), "And he shall go forth, suffering pains and afflictions and temptations of every kind; and this that the word might be fulfilled which saith he will take upon him the pains and the sicknesses of his people. And he will take upon him death, that he may loose the bands of death which bind his people; and he will take upon him their infirmities, that his bowels may be filled with mercy, according to the flesh, that he may know according to the flesh how to succor his people according to their infirmities. Now the Spirit knoweth all things; nevertheless the Son of God suffereth according to the flesh that he might take upon him the sins of his people, that he might blot out their transgressions according to the power of his deliverance; and now behold, this is the testimony which is in me.");
    Scripture scripture5 = new Scripture(new Reference("Moses", 1, 39), "For behold, this is my work and my glory —to bring to pass the immortality and eternal life of man.");
    Scripture scripture6 = new Scripture(new Reference("Psalm", 23, 1), "The Lord is my shepherd, I lack nothing.");
    public Scripture GetRandomScripture()
    {
        _scriptures = new List<Scripture> { scripture1, scripture2, scripture3, scripture4, scripture5, scripture6 };
        _random = new Random();
        int index = _random.Next(_scriptures.Count);
        return _scriptures[index];
    }
}
class Scripture
{
    private Reference _reference;
    private Random _random;
    private List<Word> _words;
    private string _text
        {
            get
            {
                List<string> wordTexts = new List<string>();
                foreach (Word word in _words)
                {
                    wordTexts.Add(word.GetDisplayText());
                }
                return string.Join(" ", wordTexts);
            }
        }

    public Scripture(Reference refer, string text)
    {
        _reference = refer;
        _random = new Random();
        _words = new List<Word>();
        foreach (string word in text.Split(' '))
        {
            _words.Add(new Word(word));
        }
    }
    
    public void HideRandomWords(int count)
    {
        List<Word> visibleWords = _words.FindAll(word => !word.IsHidden());
        int wordsToHide = Math.Min(count, visibleWords.Count);
        for (int i = 0; i < wordsToHide; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }
    public string GetDisplayText()
    {
        return $"{_reference.GetDisplayText()} {_text}";
    }
    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden());
    }
}

class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        return _isHidden ? "_____" : _text;
    }
}

class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;
    public Reference(string book, int chapter, int verse)
        : this(book, chapter, verse, verse)
    {
    }
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}
}
