using System;

class Basics
{
    static void Main()
    {
        int number = 10;

        Change(number);
        Console.WriteLine(number);

        Modify(ref number);
        Console.WriteLine(number);

        int x;
        Declare(out x);
        Console.WriteLine(x);

        string input = "123";

        int num;

        bool success = int.TryParse(input, out num);
        Console.WriteLine(success);
        Console.WriteLine(num);
        
    }
    // Pass by value
    static void Change(int res)
    {
        res = 20;
    }

    //Pass by reference 
    static void Modify(ref int x)
    {
        x = 20;
    }
    static void Declare(out int result)
    {
        result = 100;
    }
}