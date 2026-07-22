using System;

class InputOutput
{
    static void Main(String[] args)
    {
        Console.Write("Enter your Name: ");
        string? Name = Console.ReadLine();
        Console.WriteLine(Name);

        // Console.Write("Enter your age: ");
        // int age = int.Parse(Console.ReadLine());
        // Console.WriteLine(age);

        Console.Write("Enter your age: ");
        string? input = Console.ReadLine();
        bool success = int.TryParse(input,out int age);
        if (success)
        {
            Console.WriteLine("Your age is "+age);
        }
        else
        {
            Console.WriteLine("Invalid input");
        }


    }
}