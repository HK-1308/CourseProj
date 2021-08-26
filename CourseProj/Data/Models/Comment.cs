using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int UserID { get; set; }

        public string UserName { get; set; }
        public virtual User User { get; set; }

        public int ItemID { get; set; }
        public virtual Item Item { get; set; }

        public string CommentText { get; set; }
    }
}
