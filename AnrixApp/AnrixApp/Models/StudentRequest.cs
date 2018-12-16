namespace AnrixApp.Models
{
    public class StudentRequest : Student
    {
        public int MessageId;
        public long ChatId;
        public StudentRequest() { }
        public StudentRequest(string name, string surname, string patronymic, double averageMark, string numberOfGroup, string photoUrl) : base(name, surname, patronymic, averageMark, numberOfGroup, photoUrl) { }
        public StudentRequest(Student student) : base(student.Name, student.Surname, student.Patronymic, student.AverageMark, student.NumberOfGroup, student.PhotoUrl) { }
    }
}
