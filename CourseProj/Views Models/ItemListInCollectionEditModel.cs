using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Views_Models
{
    public class ItemListInCollectionEditModel
    {
        public ItemListInCollectionEditModel()
        { }

        public ItemListInCollectionEditModel(Collection collection)
        {
            this.ID = collection.ID;
            this.Name = collection.Name;
            this.Theme = collection.Theme;
            this.img = collection.img;
            this.Description = collection.Description;
            this.UserID = collection.userID;
        }
        public int ID { get; set; }
        public string Name { get; set; }

        public string Theme { get; set; }

        public int UserID { get; set; }
        public string img { get; set; }

        public string Description { get; set; }
        public List<Item> GetItems { get; set; }
    }
}
