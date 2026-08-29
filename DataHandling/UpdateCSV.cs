using System;
using System.IO;

public class UpdateCSV
{
    public static void Run()
    {
        string inputFile = "employees.csv";
        string outputFile = "updated_employees.csv";

        string[] lines = File.ReadAllLines(inputFile);

        string[] updatedLines = new string[lines.Length];

        updatedLines[0] = lines[0];

        for (int i = 1; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(',');

            string department = data[2];
            double salary = Convert.ToDouble(data[3]);

            if (department == "IT")
            {
                salary = salary + salary * 10 / 100;
            }

            data[3] = salary.ToString();

            updatedLines[i] = string.Join(",", data);
        }

        File.WriteAllLines(outputFile, updatedLines);

        Console.WriteLine("Updated CSV created successfully.");
    }
}