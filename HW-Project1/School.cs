using System;
using System.Collections.Generic;

namespace Homework_11
{
    public class School
    {
        public School(string name, string address)
        {
            Name = name;
            Address = address;
            ListOfStudents = new List<Student>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Student> ListOfStudents { get; private set; }

        public void AddStudent(Student student)
        {
            ListOfStudents.Add(student);
        }
        /*
                public void RemoveStudent(int id)
                {
                    if ((id >= 1) && (id < ListOfStudents.Count))
                    {
                        ListOfStudents.RemoveAt(id);
                        ListOfStudents.Remove()
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Invalid Student id.");
                    }
                }
        */
        public void RemoveStudent(Student student)
        {
            ListOfStudents.Remove(student);
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

        public void RemoveGraduatingStudents()
        {
            List<Student> nonGraduatingStudents = new List<Student>();

            foreach (var student in ListOfStudents)
            {
                nonGraduatingStudents.Add(student);
            }

            foreach (var student in ListOfStudents)
            {
                if (student.Age == 18)
                {
                    nonGraduatingStudents.Remove(student);
                }
            }

            ListOfStudents = nonGraduatingStudents;
        }
    }
}
