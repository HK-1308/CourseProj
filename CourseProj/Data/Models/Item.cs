using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.Models
{
    [Serializable]
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual Collection Collection { get; set; }
        public int CollectionID { get; set; }
        public IEnumerable<Like> likes { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public List<Tag> tags { get; set; } = new List<Tag>();

        public virtual Image Image { get; set; }
        public int ImageId { get; set; }

        public int NumericField1 { get; set; }
        public int NumericField2 { get; set; }
        public int NumericField3 { get; set; }

        [DataType(DataType.Text)]
        public string StringField1 { get; set; }
        [DataType(DataType.Text)]
        public string StringField2 { get; set; }
        [DataType(DataType.Text)]
        public string StringField3 { get; set; }

        [DataType(DataType.MultilineText)]
        public string TextField1 { get; set; }

        [DataType(DataType.MultilineText)]
        public string TextField2 { get; set; }

        [DataType(DataType.MultilineText)]
        public string TextField3 { get; set; }

        [DataType(DataType.Date)]
        public string DateField1 { get; set; }

        [DataType(DataType.Date)]
        public string DateField2 { get; set; }

        [DataType(DataType.Date)]
        public string DateField3 { get; set; }

        public bool BooleanField1 { get; set; }
        public bool BooleanField2 { get; set; }
        public bool BooleanField3 { get; set; }
    }
}
