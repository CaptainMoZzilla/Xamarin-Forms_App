using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AnrixApp.Models
{
    class Faculty : IEnumerable<Group>
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
    }
}
