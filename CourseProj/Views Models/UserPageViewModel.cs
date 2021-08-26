using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Views_Models
{
    public class UserPageViewModel
    {
        public int UserID { get; set; }

        public int CurrentUserID { get; set; }

        public IEnumerable<Collection>  GetCollections { get; set; }

    }
}
