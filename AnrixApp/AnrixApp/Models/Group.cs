using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AnrixApp.Models
{
    class Group : IEnumerable<Student>
    {
        public string NumberOfGroup { get; set; }
        List<Student> Students = new List<Student>();

        public Group(string numberOfGroup)
        {
            NumberOfGroup = numberOfGroup;
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return ((IEnumerable<Student>)Students).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Student>)Students).GetEnumerator();
        }

        public void Remove(Student a)
        {
            throw new NotSupportedException();
            //Students.Remove(a);
        }
        public void Add(Student a) => Students.Add(a);
    }
}
