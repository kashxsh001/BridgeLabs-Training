using System;
using System.Reflection;

class MathOperations
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }
}

//class Program
//{
//    static void Main()
//    {
//        MathOperations obj = new MathOperations();

//        Console.Write("Enter method (Add/Subtract/Multiply): ");
//        string methodName = Console.ReadLine();

//        Console.Write("Enter first number: ");
//        int a = Convert.ToInt32(Console.ReadLine());

//        Console.Write("Enter second number: ");
//        int b = Convert.ToInt32(Console.ReadLine());

//        Type type = typeof(MathOperations);

//        MethodInfo method = type.GetMethod(methodName);

//        if (method == null)
//        {
//            Console.WriteLine("Method not found.");
//            return;
//        }

//        object result = method.Invoke(obj, new object[] { a, b });

//        Console.WriteLine("Result: " + result);
//    }
//}