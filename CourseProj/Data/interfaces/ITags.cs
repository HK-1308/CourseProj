using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.interfaces
{
    public interface ITags
    {
        public IEnumerable<Tag> AllTags { get; }

        public List<Tag> GetTagsForMainPageByWeight();

        public List<Tag> GetTagsForMainPageАlphabetical();

        public void AddTagsForItem(string tags, int itemId);

        public void UpdateTagsForItem(string tags, int itemId);
    }
}
