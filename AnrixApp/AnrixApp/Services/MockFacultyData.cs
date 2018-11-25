using AnrixApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnrixApp.Services
{
    class MockFacultyData
    {
        public static Faculty getMockicngFaculty()
        {
            Faculty faculty = new Faculty { Name = "KSIS" };

            for (int a = 753500; a < 753506; a++)
            {
                Group group = new Group(a.ToString());
                for (int b = 0; b < 30; b++)
                {
                    group.Add(new Student("Даниил", "Бережнов", "Эйнштенович", 10, a.ToString()));

                }
            }

            return faculty;
        }
    }
}
