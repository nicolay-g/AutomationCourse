using NUnit.Framework;
using Homework_11;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

namespace SchoolTestProject
{
    public class Tests
    {
        private const string StudentsJsonFile = "Students.json";
        private JArray Array { get; set; }
        private School School { get; set; }


        [SetUp]
        public void InitializeSchool()
        {
            School school = new School("Sofia High School", "str. Sofia 1, Sofia");

            Array = JsonDataFileReader.GetJArray(StudentsJsonFile);

            Random random = new Random();

            foreach (var jToken in Array)
            {
                string studentName = jToken["Name"].ToObject<string>();
                int studentAge = jToken["Age"].ToObject<int>();
                Dictionary<string, int> subjectsMarks = jToken["Marks"].ToObject<Dictionary<string, int>>();

                int randomNumber = random.Next();
                Student student = new Student(randomNumber,
                                              studentName,
                                              studentAge,
                                              subjectsMarks);
                school.AddStudent(student);
            }

            School = school;
        }

        [Test]
        public void ValidateStudentsCount()
        {
            Assert.That(School.ListOfStudents, Has.Count.EqualTo(Array.Count));
        }

        [Test]
        [Description("Validate students marks range is [2..6]")]
        public void ValidateMarksRange()
        {
            foreach (Student student in School.ListOfStudents)
            {
                foreach (int value in student.SubjectsMarks.Values)
                {
                    Assert.That(value, Is.GreaterThanOrEqualTo(2).And.LessThanOrEqualTo(6));
                }
            }
        }

        [Test]
        public void ValidateNoIdInJsonData()
        {
            JArray jArray = JsonDataFileReader.GetJArray(StudentsJsonFile);

            foreach (var jToken in jArray)
            {
                Assert.IsNull(jToken["Id"]);
            }
        }

        [Test]
        public void ValidateAllStudentsIdsUniqueness()
        {
            HashSet<int> ids = new HashSet<int>();

            foreach (Student student in School.ListOfStudents)
            {
                ids.Add(student.Id);
            }

            Assert.That(ids.Count, Is.EqualTo(School.ListOfStudents.Count));
        }

        [Test]
        public void ValidateAllStudentsHaveSameSubjects()
        {
            JArray jArray = JsonDataFileReader.GetJArray(StudentsJsonFile);

            List<Dictionary<string, int>> listOfStudentMarks = new List<Dictionary<string, int>>();
            List<Dictionary<string, int>> listOfJsonMarks = new List<Dictionary<string, int>>();

            foreach (Student student in School.ListOfStudents)
            {
                listOfStudentMarks.Add(student.SubjectsMarks);
            }

            foreach (var jToken in jArray)
            {
                listOfJsonMarks.Add(jToken["Marks"].ToObject<Dictionary<string, int>>());
            }

            CollectionAssert.AreEqual(listOfStudentMarks, listOfJsonMarks);
        }

        [Test]
        public void ValidateJsonParsing()
        {
            JArray jArray = JsonDataFileReader.GetJArray(StudentsJsonFile);

            int i = 0;

            Assert.AreEqual(jArray.Count, School.ListOfStudents.Count);

            foreach (var jToken in jArray)
            {
                Student student = School.ListOfStudents[i];

                string name = jToken["Name"].ToObject<string>();
                int age = jToken["Age"].ToObject<int>();
                Dictionary<string, int> marks = jToken["Marks"].ToObject<Dictionary<string, int>>();

                Assert.AreEqual(name, student.Name);
                Assert.AreEqual(age, student.Age);

                foreach (var pair in marks)
                {
                    Assert.AreEqual(student.SubjectsMarks[pair.Key], pair.Value);
                }
                i++;
            }
        }

        [Test]
        public void ValidateNonGraduatingStudentsCount()
        {
            int nonGraduatingStudentsCount = 0;

            foreach (Student student in School.ListOfStudents)
            {
                if (student.Age != 18)
                {
                    nonGraduatingStudentsCount++;
                }
            }

            School.RemoveGraduatingStudents();

            Assert.AreEqual(nonGraduatingStudentsCount, School.ListOfStudents.Count);
        }
    }
}