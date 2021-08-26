using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public virtual Collection Collection { get; set; }
        public int CollectionID { get; set; }
        public IEnumerable<Like> likes { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public int NumericField1 { get; set; }
        public int NumericField2 { get; set; }
        public int NumericField3 { get; set; }
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public string StringField3 { get; set; }
        public string TextField1 { get; set; }
        public string TextField2 { get; set; }
        public string TextField3 { get; set; }
        public string DateField1 { get; set; }
        public string DateField2 { get; set; }
        public string DateField3 { get; set; }
        public bool BooleanField1 { get; set; }
        public bool BooleanField2 { get; set; }
        public bool BooleanField3 { get; set; }
    }
}
