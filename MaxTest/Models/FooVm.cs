using System.Collections.Generic;

namespace MaxTest.Models
{
    public class FooVm
    {
        public int? ParentId { get; set; }
        public int? SumChildId { get; set; }
        public int? MaxChildId { get; set; }
        public double? AverageChildId { get; set; }
        public int? ChildrenCount { get; set; }
        public IEnumerable<BarVm> Bars { get; set; }
    }
    public class BarVm
    {
        public int Group { get; set; }
        public int GroupCount { get; set; }
    }
}