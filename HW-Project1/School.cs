using System;
using System.Collections.Generic;

namespace Homework_8
{
    class School
    {
        public School(string name, string address)
        {
            Name = name;
            Address = address;
            ListOfStudents = new List<Student>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Student> ListOfStudents { get; set; }

        public void AddStudent(Student student)
        {
            ListOfStudents.Add(student);
        }

        public void RemoveStudent(int id)
        {
            if ((id >= 1) && (id < ListOfStudents.Count))
            {
                ListOfStudents.RemoveAt(id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid Student id.");
            }
        }

        public List<Student> GetExcellentStudents(List<Student> listOfStudents)
        {
            List<Student> excellentStudents = new List<Student>();

            foreach (var student in listOfStudents)
            {
                if (student.IsExcellent)
                {
                    excellentStudents.Add(student);
                }
            }
            return excellentStudents;
        }
    }
}
