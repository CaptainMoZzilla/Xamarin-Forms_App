using AnrixApp.Models;
using System;
using System.Linq;

namespace AnrixApp.Services
{
    class SortHelper
    {
        public static Group SortByName(Group group)
        {
            group.Students = group.Students.OrderBy(u => u.Name).ToList();
            return group;
        }

        public static Group SortBySurname(Group group)
        {
            group.Students = group.Students.OrderBy(u => u.Surname).ToList();
            return group;
        }

        public static Group SortByGroup(Group group)
        {
            group.Students = group.Students.OrderBy(u => u.NumberOfGroup).ToList();
            return group;
        }

        public static Group Search(Group group, string phrase)
        {
            Group temp = new Group("000", 0); 
            temp.Students = group.Students.Where(s => s.Name.ToLower().Contains(phrase.ToLower()) ||
                                           s.Surname.ToLower().Contains(phrase.ToLower()) ||
                                           s.NumberOfGroup.Contains(phrase))
                                          .ToList();
            return temp;
        }
    }
}
