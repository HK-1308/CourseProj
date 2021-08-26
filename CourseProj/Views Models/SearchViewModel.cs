using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Views_Models
{
    public class SearchViewModel
    {
        public IQueryable<Item> items { get; set; }

        public string Text { get; set; }
    }
}
