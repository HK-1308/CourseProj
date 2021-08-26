using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.interfaces
{
    public interface IComments
    {
        public void SaveDB();

        public void Delete(int CommentID);

        public IEnumerable<Comment> GetByItemID(int itemID);
        public void CreateComment(Comment comment);
    }
}
