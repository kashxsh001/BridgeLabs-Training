using System;
using System.Collections.Generic;
using System.Text;

namespace Arrays_Assesment {
    internal class Level2Assessment

    {
        static void BonusCalculation()
        {
            double[,] employeeData = new double[10, 2];
            

            double[,] salaryDetails = new double[10, 2];
            
            double totalBonus = 0;
            double totalOldSalary = 0;
            double totalNewSalary = 0;


        
            for (int i = 0; i < 10; i++)
            {
                

                
                double salary = Convert.ToDouble(Console.ReadLine());

                
                double years = Convert.ToDouble(Console.ReadLine());


               
                if (salary <= 0 || years < 0)
                {
                    Console.WriteLine("Invalid input. Enter values again.");
                    i--;  
                    continue;
                }

                employeeData[i, 0] = salary;
                employeeData[i, 1] = years;
            }


            
            for (int i = 0; i < 10; i++)
            {
                double salary = employeeData[i, 0];
                double years = employeeData[i, 1];

                double bonus;

                if (years > 5)
                {
                    bonus = salary * 0.05;
                }
                else
                {
                    bonus = salary * 0.02;
                }

                double newSalary = salary + bonus;


                salaryDetails[i, 0] = bonus;
                salaryDetails[i, 1] = newSalary;


                totalBonus += bonus;
                totalOldSalary += salary;
                totalNewSalary += newSalary;
            }


            
            Console.WriteLine("Employee\tOld Salary\tBonus\tNew Salary");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine((i + 1) + "\t\t" +
                    employeeData[i, 0] + "\t\t" +
                    salaryDetails[i, 0] + "\t" +
                    salaryDetails[i, 1]);
            }


            
            Console.WriteLine("\nTotal Bonus Payout: " + totalBonus);
            Console.WriteLine("Total Old Salary: " + totalOldSalary);
            Console.WriteLine("Total New Salary: " + totalNewSalary);
        }
        static void YoungestAndTallest(string[] friends, int[] ages, int[] heights)
        {
            int smallest = 0;
            int tallest = 0;
            for (int i = 1; i < 3; i++)
            {
                if (ages[i] < ages[smallest])
                {
                    smallest = i;
                }
                if (heights[i] > heights[tallest])
                {
                    tallest = i;
                }

            }
            Console.WriteLine("\nThe youngest friend is " + friends[smallest] +
                             " with age " + ages[smallest]);

            Console.WriteLine("The tallest friend is " + friends[tallest] +
                              " with height " + heights[tallest] + " cm");
        }

        static void LargestAndSecondLargest()
        {
            int number = Convert.ToInt32(Console.ReadLine()!);
            int maxDigit = 10;
            int index = 0;
            int[] numbers = new int[maxDigit];
            while (number > 0)
            {
                if (index == maxDigit)
                {
                    maxDigit = maxDigit + 10;
                    int[] temp = new int[maxDigit];
                    for(int i = 0; i < maxDigit; i++)
                    {
                        temp[i] = numbers[i];

                    }
                    numbers = temp;

                }
                numbers[index] = number % 10;
                index++;

                number = number / 10;
            }
            int largest = 0;
            int second_largest = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > largest)
                {
                    second_largest = largest;
                    largest = numbers[i];
                }
                else if (numbers[i] > second_largest)
                {
                    second_largest = numbers[i];
                }
            }
            Console.WriteLine($"The largest number is {largest} and  the second largest number is {second_largest}");
        }

        static void ReverseArray()
        {
            int number = Convert.ToInt32(Console.ReadLine()!);
            int digits = 0;
            int duplicate = number;
            while (duplicate > 0)
            {
                duplicate= duplicate / 10;
                digits++;
            }
            int[] arr = new int[digits];
            int index = 0;
            while (number > 0)
            {
                arr[index] = number % 10;
                index++;
                number = number / 10;
            }
            foreach (int num in arr)
            {
                Console.Write(num+" ");
            }
            Console.WriteLine();
            int i = 0;
            int j = arr.Length - 1;

            while (i < j)
            {
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                i++;j--;
            }

            foreach(int num in arr)
            {
                Console.Write(num+" ");
            }
        }

        static void BMI()
        {
            Console.Write("Enter number of persons: ");
            int number = Convert.ToInt32(Console.ReadLine());

            
            double[][] personData = new double[number][];

            for (int i = 0; i < number; i++)
            {
                personData[i] = new double[3];
            }

        
            string[] weightStatus = new string[number];


           
            for (int i = 0; i < number; i++)
             { 

                do
                {
                    
                    personData[i][0] = Convert.ToDouble(Console.ReadLine());

                    if (personData[i][0] <= 0)
                    {
                        Console.WriteLine("Height should be positive. Enter again.");
                    }

                } while (personData[i][0] <= 0);


                
                do
                {
                    
                    personData[i][1] = Convert.ToDouble(Console.ReadLine());

                    if (personData[i][1] <= 0)
                    {
                        Console.WriteLine("Weight should be positive. Enter again.");
                    }

                } while (personData[i][1] <= 0);

                personData[i][2] = personData[i][1] /
                                   (personData[i][0] * personData[i][0]);

                if (personData[i][2] < 18.5)
                {
                    weightStatus[i] = "Underweight";
                }
                else if (personData[i][2] < 25)
                {
                    weightStatus[i] = "Normal Weight";
                }
                else if (personData[i][2] < 30)
                {
                    weightStatus[i] = "Overweight";
                }
                else
                {
                    weightStatus[i] = "Obese";
                }
            }


            
            Console.WriteLine("\nHeight\tWeight\tBMI\tStatus");

            for (int i = 0; i < number; i++)
            {
                Console.WriteLine(
                    personData[i][0] + "\t" +
                    personData[i][1] + "\t" +
                    personData[i][2].ToString("0.00") + "\t" +
                    weightStatus[i]
                );
            }
        }

        static void Frequency()
        {
            long number = Convert.ToInt64(Console.ReadLine()!);
            int digits = 0;
            long duplicate = number;
            while (duplicate > 0)
            {
                duplicate = duplicate / 10;
                digits++;
            }
            int[] arr = new int[digits];
            int index = 0;
            while (number > 0)
            {
                arr[index] = (int)number % 10;
                index++;
                number = number / 10;
            }
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

            int[] freq = new int[10];
            foreach(int num in arr)
            {
                freq[num]++;
            }
            for(int i = 0; i < freq.Length; i++)
            {
                Console.Write(freq[i] + " ");
            }
        }
 
            static void Main(String[] args)
        {
            BonusCalculation();
            LargestAndSecondLargest();
            String[] friends = { "Amar", "Akbar", "Anthony" };
            int[] ages = new int[3];
            int[] heights = new int[3];
            for(int i = 0; i < 3; i++)
            {
                ages[i] = int.Parse(Console.ReadLine()!);
                heights[i] = int.Parse(Console.ReadLine()!);
            }
            YoungestAndTallest(friends, ages, heights);
            ReverseArray();
            BMI();
            Frequency();
        }
    }
}
