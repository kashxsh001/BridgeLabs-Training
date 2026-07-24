using System;
    Console.Write("Enter a number: ");
    string? input = Console.ReadLine();
    if(int.TryParse(input,out int number)){
            int fact = 1;
            for(int i = number ; i>=1;i--){
                fact*=i;
            }
            Console.WriteLine($"Factoial of {number} is = {fact}");
     } 
    else
    {
        Console.WriteLine("Invalid Input");
    }

