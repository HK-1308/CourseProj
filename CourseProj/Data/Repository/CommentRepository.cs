using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.Repository
{
    public class CommentRepository : IComments
    {
        private readonly DBContent dBContent;

        public CommentRepository(DBContent dBContent)
        {
            this.dBContent = dBContent;
        }
        public void CreateComment(Comment comment) => dBContent.Comment.Add(comment);

        public IEnumerable<Comment> GetByItemID(int itemID) => dBContent.Comment.Where(u => u.ItemID == itemID);

        public void Delete(int commentID) => dBContent.Comment.Remove(dBContent.Comment.FirstOrDefault(u => u.ID == commentID));
        public void SaveDB() => dBContent.SaveChanges();
    }
}
