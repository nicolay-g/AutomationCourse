using System;
using System.Collections.Generic;

namespace Homework_8
{
    public class Student
    {
        private const int AdultAge = 18;

        private int _age;
        
        public Student(int id, string name, int age, Dictionary<string, int> subjectsMarks)
        {
            this.Name = name;
            this.Id = id;
            this.Age = age;
            this.SubjectsMarks = subjectsMarks;
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value >= 7)
                {
                    this._age = value;
                }
                else
                {
                    throw new ArgumentException("Student is less than 7 years old!");
                }
            }
        }

        public Dictionary<string, int> SubjectsMarks { get; set; }

        public bool IsExcellent 
        {
            get
            {
                return IsExcellentStudent();
            } 
        }

        private bool IsExcellentStudent()
        {
            bool isExcellent = true;

            foreach (var subject in SubjectsMarks)
            {
                if (subject.Value != 6)
                {
                    isExcellent = false;
                    break;
                }
            }

            return isExcellent;
        }

        public void Speak()
        {
            if (this.Age == AdultAge)
            {
                Console.WriteLine("Hello I'm {0} and I'll graduate this year", Name);
            }
            else
            {
                Console.WriteLine("Hello I'm {0} and I've got {1} years to graduate.", Name, AdultAge - Age);
            }
        }
    }
}
