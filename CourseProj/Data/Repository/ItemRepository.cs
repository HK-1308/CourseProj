using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Data.Repository
{
    public class ItemRepository:IItems
    {
        private readonly DBContent dBContent;

        public ItemRepository(DBContent dBContent)
        {
            this.dBContent = dBContent;
        }

        public IEnumerable<Item> AllItems => dBContent.Item;

        public List<Item> ItemsForMainPage()
        {
            var result = dBContent.Item.Include(i => i.tags).Include(i=>i.likes).OrderByDescending(i => i.ID).Take(15).ToList();
            return result;
        }

        public void CreateItem(Item item) => dBContent.Item.Add(item);

        public void SaveDB() => dBContent.SaveChanges();

        public void Delete(int itemID)
        {
            foreach (var like in dBContent.Like.Where(u => u.ItemID == itemID)) dBContent.Like.Remove(like); 
            foreach (var comment in dBContent.Comment.Where(u => u.ItemID == itemID)) dBContent.Comment.Remove(comment);
            dBContent.Item.Remove(dBContent.Item.FirstOrDefault(u => u.ID == itemID));
        }
        public void DeleteAllItems(List<Item> items)
        {
            foreach (Item i in items)
                dBContent.Item.Remove(i);
        }

        public Item CollectByID(int ID) => dBContent.Item.Include(i=>i.tags).FirstOrDefault(i => i.ID == ID);
        public List<Item> CollectByCollectionID(int collectionId) => dBContent.Item.Include(i=>i.tags).Where(u => u.CollectionID == collectionId).ToList();

        public Item TakeLastElement() => dBContent.Item.Last();
        public void UpdateInfo(Item item)
        {
            var tmp = CollectByID(item.ID);
            tmp.Name = item.Name;
            tmp.BooleanField1 = item.BooleanField1;
            tmp.BooleanField2 = item.BooleanField2;
            tmp.BooleanField3 = item.BooleanField3;
            tmp.DateField1 = item.DateField1;
            tmp.DateField2 = item.DateField2;
            tmp.DateField3 = item.DateField3;
            tmp.NumericField1 = item.NumericField1;
            tmp.NumericField2 = item.NumericField2;
            tmp.NumericField3 = item.NumericField3;
            tmp.StringField1= item.StringField1;
            tmp.StringField2 = item.StringField2;
            tmp.StringField3 = item.StringField3;
            tmp.TextField1 = item.TextField1;
            tmp.TextField2 = item.TextField2;
            tmp.TextField3 = item.TextField3;
            dBContent.SaveChanges();          
        }
    }
}
