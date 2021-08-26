using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Views_Models
{
    public class MainPageViewModel
    {
        public int UserID { get; set; } = 0;
        public IEnumerable<Item> GetItems { get; set; }

        public IEnumerable<Collection> GetCollections { get; set; }

        public List<string> GetTags { get; set; }

        public List<string> Tmp { get; set; }
    }
}
