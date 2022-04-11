using CloudinaryDotNet;
using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using CourseProj.Views_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CloudinaryDotNet.Actions;
using System.IO;

namespace CourseProj.Controllers
{
    public class TablesController : Controller
    {
        private readonly IUsers users;
        private readonly ICollections collections;
        private readonly IItems items;

        public TablesController(IUsers users, ICollections collections, IItems items)
        {
            this.items = items;
            this.collections = collections;
            this.users = users;
        }

        [Authorize(Roles = "admin")]
        public ViewResult AdminTable()
        {
            var mod = new UsersListViewModel { GetUsers = users.AllUsers };
            return View(mod);
        }
        public IActionResult TableBlockButton(int[] selectedUsers, bool isMe)
        {
            if (selectedUsers[0] != -1) foreach (int v in selectedUsers) { if (users.GetUser(v).RoleId != 1) users.GetUser(v).Unblocked = false; }
            else foreach (User user in users.AllUsers) user.Unblocked = false;
            users.SaveDB();
            if (isMe) return RedirectToAction("Logout", "Account");
            else return RedirectToAction("AdminTable");
        }
        public IActionResult TableUnblockButton(int[] selectedUsers)
        {
            if (selectedUsers[0] != -1) foreach (int v in selectedUsers) users.GetUser(v).Unblocked = true;
            else foreach (User user in users.AllUsers) user.Unblocked = true;
            users.SaveDB();
            return RedirectToAction("AdminTable");
        }
        public IActionResult TableAdminButton(int[] selectedUsers)
        {
            if (selectedUsers[0] != -1) foreach (int v in selectedUsers)
                    if (users.GetUser(v).Unblocked) users.GetUser(v).RoleId = 1;
                    else foreach (User user in users.AllUsers) if (user.Unblocked) user.RoleId = 1;
            users.SaveDB();
            return RedirectToAction("AdminTable");
        }
        private void TablePartionDelete(int[] selectedUsers)
        {
            foreach (int v in selectedUsers)
                if (users.GetUser(v).RoleId != 1)
                {
                    foreach (var collection in collections.CollectionsByUserID(v))
                    {
                        foreach (var item in items.CollectByCollectionID(collection.ID)) items.Delete(item.ID);
                        collections.DeleteCollection(collection.ID);
                    }
                    users.DeleteUser(users.GetUser(v));
                }
        }
        private void TableDeleteAll()
        {
            foreach (var user in users.AllUsers)
                if (user.RoleId != 1)
                {
                    foreach (var collection in collections.CollectionsByUserID(user.ID))
                    {
                        foreach (var item in items.CollectByCollectionID(collection.ID)) items.Delete(item.ID);
                        collections.DeleteCollection(collection.ID);
                    }
                    users.DeleteUser(users.GetUser(user.ID));
                }
        }

        public IActionResult TableDeleteButton(int[] selectedUsers, bool isMe)
        {
            if (selectedUsers[0] != -1) TablePartionDelete(selectedUsers);
            else TableDeleteAll();
            users.SaveDB();
            if (isMe) return RedirectToAction("Logout", "Account");
            else return RedirectToAction("AdminTable");
        }

        [HttpPost]
        public IActionResult AdminTable(string button, int[] selectedUsers)
        {
            if (selectedUsers.Length > 0)
            {
                bool IsMe = false;
                foreach (int userID in selectedUsers) if (userID == users.GetUserIdByName(User.Identity.Name) && User.IsInRole("user")) IsMe = true;
                if (button == "Block") return RedirectToAction("TableBlockButton", new { selectedUsers = selectedUsers, isMe = IsMe });
                if (button == "Unblock") return RedirectToAction("TableUnblockButton", new { selectedUsers = selectedUsers });
                if (button == "Admin") return RedirectToAction("TableAdminButton", new { selectedUsers = selectedUsers });
                if (button == "Delete") return RedirectToAction("TableDeleteButton", new { selectedUsers = selectedUsers, isMe = IsMe });
            }
            return RedirectToAction("AdminTable");
        }
    }
}

