using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework_Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                var student = new Student()
                {
                    Name = "Bilbo"
                };
                context.Students.Add(student);
                context.SaveChanges();
            }

            Console.WriteLine("Program Over. Hit enter to Exit.");
            Console.ReadLine();

        }
    }
}
