using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Views_Models
{
    public class ItemDetailsViewModel
    {
        public ItemDetailsViewModel()
        {

        }
        public ItemDetailsViewModel(Item item)
        {
            this.Name = item.Name;
            this.NumericField1 = item.NumericField1;
            this.NumericField2 = item.NumericField2;
            this.NumericField3 = item.NumericField3;
            this.StringField1 = item.StringField1;
            this.StringField2 = item.StringField2;
            this.StringField3 = item.StringField3;
            this.Tags = item.Tags;
            this.TextField1 = item.TextField1;
            this.TextField2 = item.TextField2;
            this.TextField3 = item.TextField3;
            this.Collection = item.Collection;
            this.CollectionID = item.CollectionID;
            this.BooleanField1 = item.BooleanField1;
            this.BooleanField2 = item.BooleanField2;
            this.BooleanField3 = item.BooleanField3;
            this.ID = item.ID;
            this.DateField1 = item.DateField1;
            this.DateField2 = item.DateField2;
            this.DateField3 = item.DateField3;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public virtual Collection Collection { get; set; }
        public int CollectionID { get; set; }
        public int NumericField1 { get; set; }
        public int NumericField2 { get; set; }
        public int NumericField3 { get; set; }
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public string StringField3 { get; set; }
        public string TextField1 { get; set; }
        public string TextField2 { get; set; }
        public string TextField3 { get; set; }
        public string DateField1 { get; set; }
        public string DateField2 { get; set; }
        public string DateField3 { get; set; }
        public bool BooleanField1 { get; set; }
        public bool BooleanField2 { get; set; }
        public bool BooleanField3 { get; set; }

        public IEnumerable<Like> GetItemLikes { get; set; }

        public IEnumerable<Comment> GetItemComments { get; set; }

        public string CommentText { get; set; }


    }
}
