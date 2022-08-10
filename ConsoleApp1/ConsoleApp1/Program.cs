//in c# import replaces using. These are namespaces
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int i = 257;
            byte c = (byte)i;
            Console.WriteLine(c);

            string nullVar = null;
            string empty = "";
                List<int> notDeclared = new List<int>();

            int five = 5;

            if (five>4)
            {
                notDeclared.Add(five);
            }
            int reference = notDeclared.Count;

            Console.WriteLine($"nullVar is {nullVar}" +
                $"\n empty is {empty}" +
                $"\n notDeclared is {notDeclared}");
            Console.ReadLine();

        }
    }
}
