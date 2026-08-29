using System;
using System.IO;
using System.Text.RegularExpressions;

public class ValidateCSV
{
    public static void Run()
    {
        string[] lines = File.ReadAllLines("employees.csv");

        string emailPattern =
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        string phonePattern =
            @"^\d{10}$";

        for (int i = 1; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(',');

            string email = data[2];
            string phone = data[3];

            bool validEmail =
                Regex.IsMatch(email, emailPattern);

            bool validPhone =
                Regex.IsMatch(phone, phonePattern);

            if (!validEmail)
            {
                Console.WriteLine(
                    $"Row {i}: Invalid Email - {email}");
            }

            if (!validPhone)
            {
                Console.WriteLine(
                    $"Row {i}: Invalid Phone - {phone}");
            }
        }
    }
}