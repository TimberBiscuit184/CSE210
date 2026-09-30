static void Main(string[] args)
    {
        displayWelcome();

        string userName = promptUserName();
        int userNumber = promptUserNumber();
        int squaredNumber = squareNumber(userNumber);
        int birthYear;

        promptUserBirthYear(out birthYear);
        DisplayResult(userName, squaredNumber, birthYear);
    }

static void displayWelcome()
{
    Console.WriteLine("Welcome to the program!");
}

static string promptUserName()
{
    Console.Write("Enter your name: ");
    return Console.ReadLine();
}

static int promptUserNumber()
{
    Console.Write("Enter your favorite number: ");
    return int.Parse(Console.ReadLine());
}

static void promptUserBirthYear(out int birthYear)
{
    Console.Write("Enter your birth year: ");
    birthYear = int.Parse(Console.ReadLine());
}

static int squareNumber(int number)
{
    return number * number;
}

static void DisplayResult(string name, int square, int birthYear)
{
    Console.WriteLine($"{name}, the square of your number is {square}.");
    Console.WriteLine($"{name}, you will turn {2026 - birthYear} years old this year.");
}

    Main(new string[] {});