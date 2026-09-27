using System;
using System.Collections.Generic;

List<int> numbers = new List<int>();
int userNumber = -1;

Console.WriteLine("Enter a list of numbers, one per line. Enter 0 to finish the list and compute.");

while (userNumber != 0)
{
    userNumber = int.Parse(Console.ReadLine());
    if (userNumber != 0)
    {
        numbers.Add(userNumber);
    }
}
numbers.Sort();
Console.WriteLine("The sum of the numbers is:" + numbers.Sum());
Console.WriteLine("The average of the numbers is:" + (numbers.Sum() / numbers.Count));
Console.WriteLine("The largest number is:" + (numbers.Count > 0 ? numbers.Max() : 0));
Console.WriteLine("The smallest number is:" + (numbers.Count > 0 ? numbers.Min() : 0));
Console.WriteLine("The smallest positive number is:" + (numbers.Count > 0 ? numbers.Where(n => n > 0).Min() : 0));
Console.WriteLine("The list, sorted:");
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
