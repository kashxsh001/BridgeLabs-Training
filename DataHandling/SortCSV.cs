using System;
using System.IO;
using System.Linq;

public class SortCSV
{
    public static void Run()
    {
        string[] lines = File.ReadAllLines("employees.csv");

        var employees = lines
            .Skip(1)
            .Select(line =>
            {
                string[] data = line.Split(',');

                return new
                {
                    ID = data[0],
                    Name = data[1],
                    Department = data[2],
                    Salary = Convert.ToDouble(data[3])
                };
            })
            .OrderByDescending(e => e.Salary)
            .Take(5);

        Console.WriteLine("Top 5 Highest Paid Employees");

        Console.WriteLine("ID\tName\tDepartment\tSalary");

        foreach (var employee in employees)
        {
            Console.WriteLine(
                $"{employee.ID}\t{employee.Name}\t" +
                $"{employee.Department}\t{employee.Salary}"
            );
        }
    }
}