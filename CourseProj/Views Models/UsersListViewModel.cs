using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Views_Models
{
    public class UsersListViewModel
    {
        public IEnumerable<User> GetUsers { get; set; }
    }
}
