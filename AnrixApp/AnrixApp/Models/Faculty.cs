using System.Collections;
using System.Collections.Generic;

namespace AnrixApp.Models
{
    public class Faculty : IEnumerable<Group>
    {
        public string Name{ get; set; }
        List<Group> Groups = new List<Group>();

        public IEnumerator<Group> GetEnumerator()
        {
            return ((IEnumerable<Group>)Groups).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Group>)Groups).GetEnumerator();
        }

        public void Add(Group a) => Groups.Add(a);
        public void RemoveStudent(Student student)
        {
            foreach (var group in Groups)
            {
                if (student.NumberOfGroup.Equals(group.NumberOfGroup))
                {
                    group.Remove(student);
                }
                    
            }           
        }

        public List<Group> getGroups() => Groups;
        public Group getMegaGroup()
        {
            Group megaGroup = new Group("000", 0);
            foreach (var temp in getGroups())
            {
                foreach (var tempS in temp)
                    megaGroup.Add(tempS);
            }
            return megaGroup;
        }
        public Faculty() { }

        public Faculty(List<Student> megaGroup)
        {
            int index = -1;
            string tempNum = null;
            
            foreach(var student in megaGroup)
            {
                if (!student.NumberOfGroup.Equals(tempNum))
                {
                    tempNum = student.NumberOfGroup;
                    Groups.Add(new Group(student.NumberOfGroup, student.NumberOfGroup[0] - '0' - 5));
                    index++;
                }
                Groups[index].Add(student);
            }
        }
    }
}
