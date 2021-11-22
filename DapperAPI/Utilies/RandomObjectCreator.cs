using System;
using System.IO;
using System.Threading.Tasks;
using DapperAPI.Models;

namespace DapperAPI.Utilities
{
    public class RandomObjectCreator : IRandomObjectCreator
    {
        private string[] firstNames = new[] {"Simpson", "Kubrick", "Musk", "Luschet", "von Ball", "Mindcontrol", "Asrock", "Käfer"};
        private string[] lastNames = new[] {"Karl", "Maggie", "Jeff", "Olaf", "Matti", "Arne", "Ela", "Nico"};
        private string[] course = new[] {"WI 2021 A", "WI 2021 B", "WI 2021 C"};
        private decimal[] grades = new[] {1.0m, 1.3m, 1.7m, 2.0m, 2.3m, 2.7m, 3.0m, 3.3m, 3.7m, 4.0m, 5.0m};

        
        public Student CreateRandomStudent()
        {
            var student = new Student()
            {
                FirstName = FirstName(),
                LastName = LastName(),
                CourseId = course[2],
                MatNr = Rn(11111111, 99999999)
            };
            return student;
        }

        private string FirstName()
        {
            string[] lines = File.ReadAllLines("FirstNames.txt");
            Random rand = new Random();
            return lines[rand.Next(lines.Length)];
        }
        
        private string LastName()
        {
            string[] lines = File.ReadAllLines("LastNames.txt");
            Random rand = new Random();
            return lines[rand.Next(lines.Length)];
        }
        
        public string Subject(int line)
        {
            string[] lines = File.ReadAllLines("ProjectSubjects.txt");
            return lines[line];
        }

        public decimal GetRandomGrade()
        {
            return grades[Rn(0, 11)];
        }

        private int Rn(int min, int max)  
        {  
            return new Random().Next(min, max);  
        } 
    }
}