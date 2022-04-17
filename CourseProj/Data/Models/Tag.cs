using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string tag { get; set; }
        public List<Item> items { get; set; } = new List<Item>();
    }
}
