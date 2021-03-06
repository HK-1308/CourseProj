using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.Repository
{
    public class CollectionRepository: ICollections
    {
        private readonly DBContent dBContent;

        public CollectionRepository(DBContent dBContent)
        {
            this.dBContent = dBContent;
        }

        public List<Collection> CollectionsForMainPage() => dBContent.Collection.OrderByDescending(c=>c.Items.Count()).Take(10).ToList();

        public void DeleteCollection(int collectionID)=> dBContent.Collection.Remove(dBContent.Collection.FirstOrDefault(c => c.ID == collectionID));

        public void SaveDB() => dBContent.SaveChanges();
        public IEnumerable<Collection> AllCollections => dBContent.Collection;

        public void CreateCollection(Collection collection) => dBContent.Collection.Add(collection);

        public IEnumerable<Collection> CollectionsByUserID(int uID) => dBContent.Collection.Where(c => c.userID == uID);

        public Collection CollectByID(int ID)=>dBContent.Collection.FirstOrDefault(c=>c.ID == ID);

        public void UpdateInfo(Collection collection)
        {
            var tmp = CollectByID(collection.ID);
            tmp.Name = collection.Name;
            tmp.Theme = collection.Theme;
            dBContent.SaveChanges();
        }

        public IEnumerable<Image> AllImages => dBContent.Image;

        public void AddImage(Image image) => dBContent.Image.Add(image);

        public Image GetImageById(int id) => dBContent.Image.FirstOrDefault(i => i.ID == id);
    }
}
