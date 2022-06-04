using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Homework_8
{
    class Program
    {
        static void Main(string[] args)
        {
            School school = new School("Sofia High School", "str. Sofia 1, Sofia");

            string filePath = GetFilePath("Students.json");
            string jsonString = File.ReadAllText(filePath);
            JArray jArray = JArray.Parse(jsonString);

            Random random = new Random();

            foreach (var jToken in jArray)
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


            foreach (var student in school.ListOfStudents)
            {
                student.Speak();
            }

            Console.WriteLine("Excellent students: ");
            foreach (var student in school.GetExcellentStudents(school.ListOfStudents))
            {
                Console.WriteLine(student.Name);
            }

            foreach (var student in school.ListOfStudents)
            {
                student.Speak();
            }
        }

        static Dictionary<string, int> GetSubjectMarkPair(string studentData)
        {
            string subjectMarkStringPairs = studentData.Split(":")[1];
            Dictionary<string, int> subjectMarkPairDictionary = new Dictionary<string, int>();

            string[] subjectMarkPairs = subjectMarkStringPairs.Split(",");


            foreach (string singlePair in subjectMarkPairs)
            {
                string[] pair = singlePair.Split("-");
                string subject = pair[0];
                int mark = int.Parse(pair[1]);
                subjectMarkPairDictionary.Add(subject, mark);
            }
            return subjectMarkPairDictionary;
        }

        static string GetStudentName(string studentData)
        {
            string studentPersonalData = studentData.Split(":")[0];
            string studentName = studentPersonalData.Split(",")[0];
            return studentName;
        }

        static int GetStudentAge(string studentData)
        {
            string studentPersonalData = studentData.Split(":")[0];
            string studentAge = studentPersonalData.Split(",")[1];
            return int.Parse(studentAge);
        }

        static string GetFilePath(string fileName)
        {
            string relativePath = $"\\TestData\\{fileName}";
            string baseDirPath = AppDomain.CurrentDomain.BaseDirectory;
            baseDirPath = baseDirPath.Replace("\\bin\\Debug\\net5.0\\", "");
            string absolutePath = baseDirPath + relativePath;
            return absolutePath;
        }

        static List<string> GetTextFileLines(string filePath)
        {
            List<string> lines = new List<string>();
            var reader = new StreamReader(filePath);
            string line;
            line = reader.ReadLine();
            while (line != null)
            {
                lines.Add(line);
                line = reader.ReadLine();
            }
            reader.Dispose();
            return lines;
        }
    }
}
