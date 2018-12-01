using AnrixApp.Models;
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
    }
}
