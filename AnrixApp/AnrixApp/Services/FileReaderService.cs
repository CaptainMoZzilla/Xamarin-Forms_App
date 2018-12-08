using AnrixApp.Models;
using System;
using System.IO;
using Xamarin.Forms;

namespace AnrixApp.Services
{
    class FileReaderService
    {
        public static Faculty ReadFromFile(string filename)
        {
            Faculty faculty = new Faculty();
            var fs = new FileStream(filename, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            var groupCount = Convert.ToInt32(sr.ReadLine());
            for (var i = 0; i < groupCount; i++)
            {
                var groupNumber = sr.ReadLine();
                var tempGroup = new Group(groupNumber, groupNumber[0] - '0' - 5);
                var studentCount = Convert.ToInt32(sr.ReadLine());

                for (var j = 0; j < studentCount; j++)
                {
                    var studentName = sr.ReadLine();
                    var studentMiddleName = sr.ReadLine();
                    var studentSurname = sr.ReadLine();
                    var studentGPA = Convert.ToDouble(sr.ReadLine());

                    var url = sr.ReadLine();
                    var studentPhotoUrl = url.Length > 0 ? url : "big_student_face.png";

                    tempGroup.Add(new Student(studentName, studentSurname, studentMiddleName, studentGPA, groupNumber, studentPhotoUrl));
                    
                }
                faculty.Add(tempGroup);
            }

            sr.Close();
            fs.Close();

            return faculty;
        }
    }
}
