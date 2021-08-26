using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.interfaces
{
    public interface IItems
    {
        public IEnumerable<Item> AllItems { get; }

        public List<string> AllTags();
        IEnumerable<Item> ItemsForMainPage();
        public void CreateItem(Item item) ;

        public void Delete(int itemID);
        public void DeleteAllItems(List<Item> items);

        public Item TakeLastElement();
        public Item CollectByID(int ID);

        public void UpdateInfo(Item item);
        public void SaveDB();
        public IEnumerable<Item> CollectByCollectionID(int iD);
    }
}
