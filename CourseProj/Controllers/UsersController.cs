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
    public class UsersController : Controller
    {
        private readonly IUsers users;
        private readonly ICollections collections;
        private readonly IItems items;

        public UsersController(IUsers users, ICollections collections, IItems items)
        {
            this.items = items;
            this.collections = collections;
            this.users = users;
        }

        //Main Page
        public ViewResult MainPage()
        {
            var tmp = new MainPageViewModel
            {GetItems = items.ItemsForMainPage(), GetCollections = collections.CollectionsForMainPage(), GetTags = items.AllTags(), UserID = 0};
            if (User.Identity.IsAuthenticated) tmp.UserID = users.GetUserIdByName(User.Identity.Name);
            foreach (var item in tmp.GetItems) item.Collection = collections.CollectByID(item.CollectionID);
            return View(tmp);
        }

        public IActionResult UserPage(int userID)
        {
            var tmp = new UserPageViewModel
            {UserID = userID,GetCollections = collections.CollectionsByUserID(userID), Email = users.GetUser(userID).Email};
            if (users.GetUserIdByName(User.Identity.Name) == userID && !users.GetUser(userID).Unblocked) return View("BlockedView");
            if (User.Identity.IsAuthenticated) tmp.CurrentUserID = users.GetUserIdByName(User.Identity.Name);
            else tmp.CurrentUserID = 0;
            return View(tmp);
        }

        public IActionResult MyPage()
        {
            return RedirectToAction("UserPage",new { userID = users.GetUserIdByName(User.Identity.Name)});
        }

        public IActionResult SortTags()
        {
            var tmp = new MainPageViewModel
            { GetItems = items.ItemsForMainPage(), GetCollections = collections.CollectionsForMainPage(), GetTags = items.AllTags(), Tmp = items.AllTags().OrderBy(u => u).ToList() };
            if (User.Identity.IsAuthenticated) tmp.UserID = users.GetUserIdByName(User.Identity.Name);
            else tmp.UserID = 0;
            return View("MainPage", tmp);
        }
    }
}
