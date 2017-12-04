using System.Collections.Generic;

namespace MaxTest.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Child> Children { get; set; }
    }
}