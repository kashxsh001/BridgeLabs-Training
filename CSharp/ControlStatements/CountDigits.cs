using System;

string? input = Console.ReadLine();
if(int.TryParse(input,out int number))
{
    int count = 0;
    while (number > 0)
    {
        number/=10;
        count++;
        
    }
    Console.WriteLine(count);
}
else
{
    Console.WriteLine("Invalid input!");
}