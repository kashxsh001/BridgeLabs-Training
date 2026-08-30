using System;
using System.Reflection;

class Person
{
    private int age = 20;

    public void Display()
    {
        Console.WriteLine("Age: " + age);
    }
}

//class Program
//{
//    static void Main(String[] agrs)
//    {
//        Person person = new Person();

//        Type type = typeof(Person);

//        FieldInfo? field = type.GetField(
//            "age",
//            BindingFlags.NonPublic | BindingFlags.Instance);

//        // Get private field value
//        int oldAge = (int)field?.GetValue(person)!;

//        Console.WriteLine("Old Age: " + oldAge);

//        // Modify private field
//        field.SetValue(person, 30);

//        // Get modified value
//        int newAge = (int)field?.GetValue(person)!;

//        Console.WriteLine("New Age: " + newAge);
//    }
//}