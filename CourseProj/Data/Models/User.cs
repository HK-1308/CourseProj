using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.Models
{
    public class User 
    {
        public int ID { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool Unblocked { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }
        public IEnumerable <Collection> Collections { get; set; }

        public IEnumerable<Like> likes { get; set; }

        public IEnumerable<Comment> comments { get; set; }
    }

    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IEnumerable<User> Users { get; set; }

    }
}
