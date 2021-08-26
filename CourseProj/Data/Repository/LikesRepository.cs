using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.Repository
{
    public class LikesRepository: ILikes
    {
        private readonly DBContent dBContent;

        public LikesRepository(DBContent dBContent)
        {
            this.dBContent = dBContent;
        }

        public bool IsLiked(int userID, int itemID)
        {
            return dBContent.Like.FirstOrDefault(u => u.UserID == userID && u.ItemID==itemID) != null;
        }

        public void CreateLike(Like like) => dBContent.Like.Add(like);

        public IEnumerable<Like> GetByItemID(int itemID)=> dBContent.Like.Where(u => u.ItemID == itemID);
        public void DeleteLike(int userID, int itemID)
        {
            dBContent.Remove(dBContent.Like.FirstOrDefault(u => u.UserID == userID && u.ItemID == itemID));
            dBContent.SaveChanges();
        }

        public void Delete(int likeID) => dBContent.Like.Remove(dBContent.Like.FirstOrDefault(u => u.ID == likeID));
        public void SaveDB() => dBContent.SaveChanges();

    }
}
