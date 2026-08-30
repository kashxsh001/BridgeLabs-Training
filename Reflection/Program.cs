using System;
using System.Reflection;

// 1. Define the Abstract Class
public abstract class Vehicle { }

// 2. Define an intermediate concrete class
public class Car : Vehicle { }

// 3. Define the target class
public class ElectricCar : Car { }

//class Program
//{
//    static void Main()
//    {
//        // Get the Type object for the class you want to inspect
//        Type targetType = typeof(ElectricCar);

//        // Find its abstract base class
//        Type abstractBase = ReflectionExt.GetAbstractBaseClass(targetType);
        
//        if (abstractBase != null)
//        {
//            Console.WriteLine($"The abstract class implemented by {targetType.Name} is: {abstractBase.Name}");
//        }
//        else
//        {
//            Console.WriteLine($"{targetType.Name} does not inherit from an abstract class.");
//        }
//    }
//}
