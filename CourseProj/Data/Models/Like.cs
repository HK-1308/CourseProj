using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.Models
{
    public class Like
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int ItemID { get; set; }
        public virtual Item Item { get; set; }
    }
}
