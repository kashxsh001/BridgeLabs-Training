using System;
using System.Collections.Generic;
using System.Text;

namespace DataHandling
{
    public class ReadAndPrint
    {
        public static void Run()
        {
            string filePath = "students.csv";

            string[] lines = File.ReadAllLines(filePath);

            Console.WriteLine("ID\tName\tAge\tMarks");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');

                Console.WriteLine(
                    $"{data[0]}\t{data[1]}\t{data[2]}\t{data[3]}"
                );
            }
        }
    }
}
