Console.WriteLine("What is your grade? (0-100%)");
int grade = int.Parse(Console.ReadLine());

string letter = "";

if (grade >= 90 && grade <= 100)
{
    letter = "A";
}
else if (grade >= 80)
{
    letter = "B";
}
else if (grade >= 70)
{
    letter = "C";
}
else if (grade >= 60)
{
    letter = "D";
}
else if (grade < 60 && grade >= 0)
{
    letter = "F";
}
else
{
    letter = "Invalid grade. Please enter a grade between 0 and 100%.";
}

Console.WriteLine($"Your letter grade is: {letter}");

if (grade >= 70 && grade <= 100)
{
    Console.WriteLine("Congrats! You passed!");
}
else
{
    Console.WriteLine("lol, you failed.");
}

