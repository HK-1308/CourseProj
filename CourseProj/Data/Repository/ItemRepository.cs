using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<string> AllTags()
        {
            string tmpstring=null;
            foreach(Item item in dBContent.Item) tmpstring += item.Tags;
            if (tmpstring == null) return new List<string>();
            return SortByWeight(tmpstring.Trim().Split('#').OrderBy(u=>u).ToList());
        }

        private List<int> GetWeights(IEnumerable<string> tags)
        {
            int k = 0; List<int> weights = new List<int>();
            for (int i = 1; i < tags.ToList().Count - 1; i++)
            {
                for (int j = i + 1; j < tags.ToList().Count; j++)
                    if (tags.ToList()[j] == tags.ToList()[i]) k++; else { i = j - 1; break; }
                weights.Add(k); k = 0;
            }
            return weights;
        }
        private List<string> SortByWeight(IEnumerable<string> tags)
        {
            List<int> weights = GetWeights(tags); 
            List<string> result = tags.Distinct().ToList();
            result.Remove("");
            for (int i = 0; i < weights.Count - 1; i++)
                for (int j = i+1; j < weights.Count; j++)
                {
                    if(weights[j]>weights[i])
                    {
                        int tmp = weights[j];weights[j] = weights[i]; weights[i] = tmp;
                        string restmp = result[j]; result[j] = result[i];result[i] = restmp;
                    }
                } 
            return result;
        }


        public IEnumerable<Item> ItemsForMainPage()
        {
            List<Item> items = new List<Item>();
            var tmp = dBContent.Item.ToArray();
            for (int i = tmp.Length-1; i >= 0; i--)
            {
                items.Add(tmp[i]);
                if (items.Count > 9) break;
            }
            return items;
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

        public Item CollectByID(int ID) => dBContent.Item.FirstOrDefault(i => i.ID == ID);
        public IEnumerable<Item> CollectByCollectionID(int ID) => dBContent.Item.Where(u => u.CollectionID == ID);

        public Item TakeLastElement() => dBContent.Item.Last();
        public void UpdateInfo(Item item)
        {
            var tmp = CollectByID(item.ID);
            tmp.Name = item.Name;
            tmp.Tags = item.Tags;
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
