using System;
using System.IO;

public class WriteCSV
{
    public static void Run()
    {
        string filePath = "employees.csv";

        string[] employees =
        {
            "ID,Name,Department,Salary",
            "1,Kashish,IT,50000",
            "2,Rahul,HR,45000",
            "3,Aman,IT,60000",
            "4,Neha,Finance,55000",
            "5,Anita,IT,70000"
        };

        File.WriteAllLines(filePath, employees);

        Console.WriteLine("Employee CSV created successfully.");
    }
}