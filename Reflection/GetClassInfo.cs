using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Reflection
{
    internal class GetClassInfo
    {
        private int id;
        public string Name;

        public GetClassInfo()
        {
        }

        public GetClassInfo(int id)
        {
            this.id = id;
        }

        public void Display()
        {
            Console.WriteLine("Hello");
        }

        public int Add(int a, int b)
        {
            return a + b;
        }
    
    }
    //class Program
    //{
    //    static void Main()
    //    {
    //        Console.Write("Enter class name: ");
    //        string className = Console.ReadLine();

    //        Type type = Assembly.GetExecutingAssembly().GetType(className);

    //        if (type == null)
    //        {
    //            Console.WriteLine("Class not found.");
    //            return;
    //        }

    //        Console.WriteLine("\nClass: " + type.Name);

    //        Console.WriteLine("\nMethods:");
    //        foreach (MethodInfo method in type.GetMethods(
    //            BindingFlags.Public |
    //            BindingFlags.NonPublic |
    //            BindingFlags.Instance |
    //            BindingFlags.DeclaredOnly))
    //        {
    //            Console.WriteLine(method.Name);
    //        }

    //        Console.WriteLine("\nFields:");
    //        foreach (FieldInfo field in type.GetFields(
    //            BindingFlags.Public |
    //            BindingFlags.NonPublic |
    //            BindingFlags.Instance |
    //            BindingFlags.DeclaredOnly))
    //        {
    //            Console.WriteLine(field.Name);
    //        }

    //        Console.WriteLine("\nConstructors:");
    //        foreach (ConstructorInfo constructor in type.GetConstructors(
    //            BindingFlags.Public |
    //            BindingFlags.NonPublic |
    //            BindingFlags.Instance))
    //        {
    //            Console.WriteLine(constructor);
    //        }
    //    }
    //}

}


