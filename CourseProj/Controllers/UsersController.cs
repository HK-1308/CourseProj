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
        private readonly ITags tags;

        public UsersController(IUsers users, ICollections collections, IItems items, ITags tags)
        {
            this.items = items;
            this.collections = collections;
            this.users = users;
            this.tags = tags;
        }

        //Main Page
        public ViewResult MainPage()
        {
            var viewModel = new MainPageViewModel()                                                                        
            {GetItems = items.ItemsForMainPage(), GetCollections = collections.CollectionsForMainPage(), GetTagsByWeight = tags.GetTagsForMainPageByWeight(), GetTagsAlphabetical = tags.GetTagsForMainPageАlphabetical(), UserID = 0, tagCloudIsSortedAlphabetical = false};
            foreach(var collection in viewModel.GetCollections)
            {
                collection.Image = collections.GetImageById(collection.ImageId);
            }
            if (User.Identity.IsAuthenticated) viewModel.UserID = users.GetUserIdByName(User.Identity.Name);
            return View(viewModel);
        }

        public IActionResult UserPage(int userID)
        {
            var tmp = new UserPageViewModel
            {UserID = userID,GetCollections = collections.CollectionsByUserID(userID), Email = users.GetUser(userID).Email};
            if (User.Identity.IsAuthenticated)
            {
                tmp.CurrentUserID = users.GetUserIdByName(User.Identity.Name);
                if (users.GetUserIdByName(User.Identity.Name) == userID && !users.GetUser(userID).Unblocked) return View("BlockedView");
            }
            else tmp.CurrentUserID = 0;
            foreach (var collection in tmp.GetCollections)
            {
                collection.Image = collections.GetImageById(collection.ImageId);
            }
            return View(tmp);
        }

        public IActionResult MyPage()
        {
            return RedirectToAction("UserPage",new { userID = users.GetUserIdByName(User.Identity.Name)});
        }

        public IActionResult SortTags()
        {
            var model = new MainPageViewModel
            { GetItems = items.ItemsForMainPage(), GetCollections = collections.CollectionsForMainPage(), GetTagsByWeight = tags.GetTagsForMainPageByWeight(), GetTagsAlphabetical = tags.GetTagsForMainPageАlphabetical(), UserID = 0, tagCloudIsSortedAlphabetical = true };
            if (User.Identity.IsAuthenticated) model.UserID = users.GetUserIdByName(User.Identity.Name);
            else model.UserID = 0;
            foreach (var collection in model.GetCollections)
            {
                collection.Image = collections.GetImageById(collection.ImageId);
            }
            return View("MainPage", model);
        }
    }
}
