Random randomGenerator = new Random();
string playAgain;

do
{
    int number = randomGenerator.Next(1, 101);
    Console.WriteLine("Guess a number between 1 and 100:");
    int guess = int.Parse(Console.ReadLine());
    int guessCount = 1;

    while (guess != number)
    {
        if (guess > number)
        {
            Console.WriteLine("Lower");
        }
        else if (guess < number)
        {
            Console.WriteLine("Higher");
        }
        Console.WriteLine("Guess again!");
        guess = int.Parse(Console.ReadLine());
        guessCount++;
    }

    if (guessCount <= 5)
    {
        Console.WriteLine("Incredible job! You guessed it in " + guessCount + " attempts.");
        Console.WriteLine("Would you like to play again? (yes/no)");
        playAgain = Console.ReadLine();
    }
    else if (guessCount <= 10)
    {
        Console.WriteLine("You guessed it! It took you " + guessCount + " attempts.");
        Console.WriteLine("Would you like to play again? (yes/no)");
        playAgain = Console.ReadLine();
    }
    else if (guessCount <= 25)
    {
        Console.WriteLine("Wow, that took forever. Even I was starting to get board after your" + guessCount + "attempts.");
        Console.WriteLine("Would you like to try for a better score? (yes/no)");
        playAgain = Console.ReadLine();
    }
    else
    {
        Console.WriteLine("That took way too long! Please stop wasting my time.");
        Console.WriteLine("I'm going to sleep");
        playAgain = Console.ReadLine();
    }

} while (playAgain.ToLower() == "yes" || playAgain.ToLower() == "can i play again, pretty please?");