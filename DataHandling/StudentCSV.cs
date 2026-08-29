using System;
using System.Collections.Generic;
using System.IO;

public class Student
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public double Marks { get; set; }

    public Student(int id, string name, int age, double marks)
    {
        ID = id;
        Name = name;
        Age = age;
        Marks = marks;
    }

    public void Display()
    {
        Console.WriteLine(
            $"{ID}\t{Name}\t{Age}\t{Marks}");
    }
}

public class StudentCSV
{
    public static void Run()
    {
        string[] lines =
            File.ReadAllLines("students.csv");

        List<Student> students =
            new List<Student>();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(',');

            Student student = new Student(
                Convert.ToInt32(data[0]),
                data[1],
                Convert.ToInt32(data[2]),
                Convert.ToDouble(data[3])
            );

            students.Add(student);
        }

        Console.WriteLine("ID\tName\tAge\tMarks");

        foreach (Student student in students)
        {
            student.Display();
        }
    }
}