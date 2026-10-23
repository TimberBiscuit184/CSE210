using System;
using System.Collections.Generic;
using System.IO;

namespace eternalQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Eternal Quest!");
            Console.WriteLine("Your journey begins now...");
            GoalManager manager = new GoalManager();
            manager.Start();
        }
    }

    class GoalManager
    {
        private List<Goal> _goals = new List<Goal>();
        private int _score = 0;

        private static readonly string[] _titles =
            { "Apprentice", "Adventurer", "Pathfinder", "Champion", "Legend" };

        public void Start()
        {
            string choice = "";
            while (choice != "6")
            {
                Console.WriteLine("Menu Options:");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Save Goals");
                Console.WriteLine("4. Load Goals");
                Console.WriteLine("5. Record Event");
                Console.WriteLine("6. Exit");
                Console.Write("Select a choice from the menu: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal();
                        break;
                    case "2":
                        ListGoalDetails();
                        break;
                    case "3":
                        SaveGoals();
                        break;
                    case "4":
                        LoadGoals();
                        break;
                    case "5":
                        RecordEvent();
                        break;
                    case "6":
                        Console.WriteLine("Exiting the program. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        private void DisplayPlayerInfo()
        {
            int level = Math.Max(0, _score) / 1000;
            string title = _titles[Math.Min(level, _titles.Length - 1)];
            Console.WriteLine($"\nYou have {_score} points. Level {level + 1}: {title}");
        }

        private void ListGoalDetails()
        {
            Console.WriteLine("Goal Details:");
            foreach (Goal goal in _goals)
            {
                Console.WriteLine($"- {goal.GetDetailsString()}");
            }
        }

        private void CreateGoal()
        {
            Console.WriteLine("Select Goal Type:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.WriteLine("4. Negative Goal (Get rid of a bad habit)");
            Console.Write("Which type of goal would you like to create? ");
            string type = Console.ReadLine();

            if (type != "1" && type != "2" && type != "3" && type != "4")
            {
                Console.WriteLine("Invalid type.");
                return;
            }

            string name = Prompt("What is the name of your goal? ");
            string desc = Prompt("What is a short description of it? ");
            int points = PromptInt("What is the amount of points associated with this goal? ");

            switch (type)
            {
                case "1":
                    _goals.Add(new SimpleGoal(name, desc, points));
                    break;
                case "2":
                    _goals.Add(new EternalGoal(name, desc, points));
                    break;
                case "3":
                    int target = PromptInt("How many times does this goal need to be accomplished for a bonus? ");
                    int bonus = PromptInt("What is the bonus for accomplishing it that many times? ");
                    _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                    break;
                case "4":
                    _goals.Add(new NegativeGoal(name, desc, points));
                    break;
            }
        }

        private void RecordEvent()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals to record yet.");
                return;
            }

            Console.WriteLine("\nThe goals are:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].ShortName}");
            }

            int index = PromptInt("Which goal did you accomplish? ") - 1;
            if (index < 0 || index >= _goals.Count)
            {
                Console.WriteLine("Invalid goal number.");
                return;
            }

            int earned = _goals[index].RecordEvent();
            _score += earned;

            if (earned >= 0)
            {
                Console.WriteLine($"Congratulations! You have earned {earned} points!");
            }
            else
            {
                Console.WriteLine($"Ouch. You lost {-earned} points.");
            }
            Console.WriteLine($"You now have {_score} points.");
        }

        private void SaveGoals()
        {
            string file = Prompt("What is the filename for the goal file? ");
            using (StreamWriter output = new StreamWriter(file))
            {
                output.WriteLine(_score);
                foreach (Goal g in _goals)
                {
                    output.WriteLine(g.GetStringRepresentation());
                }
            }
            Console.WriteLine("Goals saved.");
        }

        private void LoadGoals()
        {
            string file = Prompt("What is the filename for the goal file? ");
            if (!File.Exists(file))
            {
                Console.WriteLine("File not found.");
                return;
            }

            string[] lines = File.ReadAllLines(file);
            _score = int.Parse(lines[0]);
            _goals.Clear();
            for (int i = 1; i < lines.Length; i++)
            {
                _goals.Add(ParseGoal(lines[i]));
            }
            Console.WriteLine("Goals loaded.");
        }

        private Goal ParseGoal(string line)
        {
            string[] parts = line.Split(':', 2);
            string[] d = parts[1].Split('|');

            switch (parts[0])
            {
                case "SimpleGoal":
                    return new SimpleGoal(d[0], d[1], int.Parse(d[2]), bool.Parse(d[3]));
                case "EternalGoal":
                    return new EternalGoal(d[0], d[1], int.Parse(d[2]));
                case "ChecklistGoal":
                    return new ChecklistGoal(d[0], d[1], int.Parse(d[2]),
                        int.Parse(d[4]), int.Parse(d[3]), int.Parse(d[5]));
                case "NegativeGoal":
                    return new NegativeGoal(d[0], d[1], int.Parse(d[2]));
                default:
                    throw new InvalidDataException($"Unknown goal type: {parts[0]}");
            }
        }

        private string Prompt(string message)
        {
            Console.Write(message);
            return (Console.ReadLine() ?? "").Replace("|", "/");
        }

        private int PromptInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                {
                    return value;
                }
                Console.WriteLine("Please enter a whole number (0 or more).");
            }
        }
    }

    public abstract class Goal
    {
        private string _shortName;
        private string _description;
        private int _points;

        protected Goal(string name, string description, int points)
        {
            _shortName = name;
            _description = description;
            _points = points;
        }

        public string ShortName => _shortName;
        protected string Description => _description;
        protected int Points => _points;

        public abstract int RecordEvent();
        public abstract bool IsComplete();
        public abstract string GetStringRepresentation();
        public virtual string GetDetailsString()
        {
            string box = IsComplete() ? "[X]" : "[ ]";
            return $"{box} {_shortName} ({_description})";
        }

        protected string BaseFields() => $"{_shortName}|{_description}|{_points}";
    }

    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points, bool isComplete = false)
            : base(name, description, points)
        {
            _isComplete = isComplete;
        }

        public override int RecordEvent()
        {
            if (_isComplete) return 0;
            _isComplete = true;
            return Points;
        }

        public override bool IsComplete() => _isComplete;

        public override string GetStringRepresentation() =>
            $"SimpleGoal:{BaseFields()}|{_isComplete}";
    }

    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override int RecordEvent() => Points;

        public override bool IsComplete() => false; 

        public override string GetStringRepresentation() =>
            $"EternalGoal:{BaseFields()}";
    }

    public class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private int _target;
        private int _bonus;

        public ChecklistGoal(string name, string description, int points,
                             int target, int bonus, int amountCompleted = 0)
            : base(name, description, points)
        {
            _target = target;
            _bonus = bonus;
            _amountCompleted = amountCompleted;
        }

        public override int RecordEvent()
        {
            if (IsComplete()) return 0;
            _amountCompleted++;
            return IsComplete() ? Points + _bonus : Points;
        }

        public override bool IsComplete() => _amountCompleted >= _target;


        public override string GetDetailsString() =>
            $"{base.GetDetailsString()} -- Currently completed: {_amountCompleted}/{_target}";

        public override string GetStringRepresentation() =>
            $"ChecklistGoal:{BaseFields()}|{_bonus}|{_target}|{_amountCompleted}";
    }

  
    public class NegativeGoal : Goal
    {
        public NegativeGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override int RecordEvent() => -Points;

        public override bool IsComplete() => false;

        public override string GetDetailsString() =>
            $"[!] {ShortName} ({Description}) -- costs {Points} pts";

        public override string GetStringRepresentation() =>
            $"NegativeGoal:{BaseFields()}";
    }
}