﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace AnrixApp.Models
{
    public class Group : IEnumerable<Student>
    {
        public string NumberOfGroup { get; set; }
        public int Course { get; set; }
        public List<Student> Students = new List<Student>();

        public Group(string numberOfGroup, int course)
        {
            NumberOfGroup = numberOfGroup;
            Course = course;
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return ((IEnumerable<Student>)Students).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Student>)Students).GetEnumerator();
        }

        public void Remove(Student a) => Students.Remove(a);
        public void Add(Student a) => Students.Add(a);
        public List<Student> GetStudents()  => new List<Student>(Students);
    }
}
