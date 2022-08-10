using System;

namespace Scores
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter First Name");
            string date = DateTime.Today.ToShortDateString();
            string userName = Console.ReadLine();
            string message = $"\nWelcome Back {userName}. Today is {date}";
            Console.WriteLine(message);

            //Convert text file to a string Array
            string path = @"C:\Users\Razer\source\repos\Scores\Scores\studentScores.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            double totalSCore = 0.0;
            Console.WriteLine("\n Student scores: \n");
            foreach (string line in lines) {
                Console.WriteLine($"{line}");
                double score = Convert.ToDouble(line);
                totalSCore += score;
            }

            double averageScore = totalSCore / lines.Length;
            Console.WriteLine($"\n Total of {lines.Length} student scores. \t average score: {averageScore}");

            //var studentScores = 

            Console.WriteLine("\n\nPress any Key");
            Console.ReadLine();
        }
    }
}

//C: \Users\Razer\source\repos\Scores\Scores