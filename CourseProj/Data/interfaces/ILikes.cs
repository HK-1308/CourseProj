using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.interfaces
{
    public interface ILikes
    {
        public void SaveDB();

        public void Delete(int LikeID);
        public bool IsLiked(int userID, int itemID);

        public IEnumerable<Like> GetByItemID(int itemID);
        public void DeleteLike(int userID, int itemID);
        public void CreateLike(Like like);
    }
}
