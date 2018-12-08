using System.Collections.Generic;
using System.Diagnostics;

namespace AnrixApp.Models
{
    public class Student
    {
        public Student(string name, string surname, string patronymic, double averageMark, string numberOfGroup, string photoUrl)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            AverageMark = averageMark;
            NumberOfGroup = numberOfGroup;
            Title = Name + " " + Surname;
            PhotoUrl = photoUrl;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public double AverageMark { get; set; }
        public string NumberOfGroup { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }

        public override bool Equals(object obj)
        {
            var student = obj as Student;
            return student != null &&
                   Name == student.Name &&
                   Surname == student.Surname &&
                   Patronymic == student.Patronymic &&
                   AverageMark == student.AverageMark &&
                   NumberOfGroup == student.NumberOfGroup;
        }

        public override int GetHashCode()
        {
            var hashCode = 853717672;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Patronymic);
            hashCode = hashCode * -1521134295 + AverageMark.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NumberOfGroup);
            return hashCode;
        }
    }
}
