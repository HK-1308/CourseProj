using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Collection> CollectionsForMainPage()
        {
            IEnumerable<Collection> collections = dBContent.Collection;
            foreach (Collection collection in collections)
                collection.Items = dBContent.Item.Where(i => i.CollectionID == collection.ID);
            
            var tmp = collections.OrderBy(s => s.Items.Count()).ToArray();
            List<Collection> result = new List<Collection>();
            for(int i= tmp.Length-1; i>=0;i--)
            {
                result.Add(tmp[i]);
                if (result.Count() > 9) break;
            }
            return result;
        }
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
    }
}
