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
            this.Item = item;
            this.Collection = item.Collection;
        }

        public Item Item { get; set; }
        public virtual Collection Collection { get; set; }

        public IEnumerable<Like> GetItemLikes { get; set; }

        public IEnumerable<Comment> GetItemComments { get; set; }

        public string CommentText { get; set; }


    }
}
