using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Data.Repository
{
    public class TagRepository:ITags
    {
        private readonly DBContent dBContent;

        public TagRepository(DBContent dBContent)
        {
            this.dBContent = dBContent;
        }

        public IEnumerable<Tag> AllTags => dBContent.Tag;

        public List<Tag> GetTagsForMainPageByWeight()
        {
            var tags = dBContent.Tag.Include(t => t.items).OrderByDescending(t=>t.items.Count()).Take(15).ToList();
            return tags; 
        }

        public List<Tag> GetTagsForMainPageАlphabetical()
        {
            var tags = dBContent.Tag.Include(t => t.items).OrderBy(t => t.tag).Take(15).ToList();
            return tags;
        }

        public Tag FindTag(string tag) => dBContent.Tag.FirstOrDefault(t => t.tag == tag);

        public void AddTagsForItem(string tags, int itemId)
        {
            var itemObject = dBContent.Item.Include(i=>i.tags).FirstOrDefault(i => i.ID == itemId);
            var tagsList = TagsParsing(tags);
            itemObject.tags = tagsList;
            dBContent.SaveChanges();
        }

        public void UpdateTagsForItem(string tags, int itemId)
        {
            var itemObject = dBContent.Item.Include(i=>i.tags).FirstOrDefault(i => i.ID == itemId);
            itemObject.tags.Clear();
            AddTagsForItem(tags, itemId);
            dBContent.Item.Update(itemObject);
            dBContent.SaveChanges();
        }

        private List<Tag> TagsParsing(string tags)
        {
            var tagObjectList = new List<Tag>();
            var tagsList = tags.Trim().Split('#').ToList();
            tagsList.Remove("");
            foreach(var tag in tagsList)
            {
                bool tagExists = dBContent.Tag.Any(t => t.tag == tag);
                if (!tagExists)
                {
                    tagObjectList.Add(new Tag() { tag = tag });
                }
                else
                {
                    tagObjectList.Add(dBContent.Tag.FirstOrDefault(t => t.tag == tag));
                }
            }
            return tagObjectList;
        }
    }
}
