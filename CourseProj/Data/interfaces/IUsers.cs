using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.interfaces
{
    public interface IUsers
    {
        IEnumerable<User> AllUsers { get; }

        void CreateUser(User user);

        User ConfirmUser(string name, string pas);

        User WithSameName(string name);

        User GetUser(int id);

        public int GetUserIdByName(string name);

        public List<Item> GetItemsForUserFavoritePage(string name);

        public void AddItemToFavorite(Item item, int userId);

        public void DeleteItemFromFavorite(Item item, int userId);
        void DeleteUser(User user);

        public void SaveDB();
    }
}
