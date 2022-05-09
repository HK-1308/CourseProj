using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CourseProj.Data.Repository
{
    public class UserRepository : IUsers
    {
        private readonly DBContent dBContent;


        public UserRepository(DBContent dBContent)
        {
            this.dBContent = dBContent;
        }
        public IEnumerable<User> AllUsers => dBContent.User;


        public User ConfirmUser(string name, string pas) => dBContent.User.FirstOrDefault(u => u.Email == name && u.Password == pas);

        public void CreateUser(User user) => dBContent.User.Add(user);

        public int GetUserIdByName(string name) => dBContent.User.FirstOrDefault(u => u.Email == name).ID;

        public List<Item> GetItemsForUserFavoritePage(string name)
        {
            var id = GetUserIdByName(name);
            return dBContent.Item.Include(i => i.tags).Include(i=>i.likes).Include(i => i.Collection).Where(i => i.likes.Any(l => l.UserID == id)).ToList();
        }
        public void DeleteUser(User user) => dBContent.User.Remove(user);

        public User GetUser(int id) => dBContent.User.FirstOrDefault(u => u.ID == id);

        public void SaveDB() => dBContent.SaveChanges();

        public User WithSameName(string name) => dBContent.User.FirstOrDefault(u => u.Email == name);
    }
}
