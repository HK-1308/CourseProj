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
    public class ItemsController : Controller
    {
        private readonly IUsers users;
        private readonly ICollections collections;
        private readonly IItems items;
        private readonly ILikes likes;
        private readonly IComments comments;

        public ItemsController(IUsers users, ICollections collections, IItems items, ILikes likes, IComments comments)
        {
            this.items = items;
            this.collections = collections;
            this.users = users;
            this.likes = likes;
            this.comments = comments;
        }

        public IActionResult CreateNewItem(int collectionID)
        {
            var CurrentItem = new Item { Collection = collections.CollectByID(collectionID) };
            CurrentItem.CollectionID = CurrentItem.Collection.ID;
            return View(CurrentItem);
        }

        [HttpPost]
        public IActionResult CreateNewItem(Item item)
        {
            if (ModelState.IsValid)
            {
                items.CreateItem(item);
                items.SaveDB();
                return RedirectToAction("CreateNewItem", new { collectionID = item.CollectionID });
            }
            return View(item);
        }

        public IActionResult EditItem(int itemID)
        {
            var CurrentItem = items.CollectByID(itemID);
            CurrentItem.Collection = collections.CollectByID(CurrentItem.CollectionID);
            return View(CurrentItem);
        }

        [HttpPost]
        public IActionResult EditItem(Item item)
        {
            items.UpdateInfo(item);
            collections.SaveDB();
            return RedirectToAction("EditCollection", "Collections", new { collectionID = item.Collection.ID });
        }

        public IActionResult DeleteItem(int itemID)
        {
            int collectionID = items.CollectByID(itemID).CollectionID;
            items.Delete(itemID);
            items.SaveDB();
            return RedirectToAction("EditCollection", "Collections", new { collectionID = collectionID });
        }

        public IActionResult ItemDetails(int itemID)
        {
            var CurrentItem = items.CollectByID(itemID);
            CurrentItem.Collection = collections.CollectByID(CurrentItem.CollectionID);
            ItemDetailsViewModel tmp = new ItemDetailsViewModel(CurrentItem) { GetItemLikes = likes.GetByItemID(itemID), GetItemComments = comments.GetByItemID(itemID) };
            return View(tmp);
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost]
        public IActionResult ItemDetails(ItemDetailsViewModel model)
        {
            if (!users.GetUser(users.GetUserIdByName(User.Identity.Name)).Unblocked) return View("BlockedView");
            var comment = new Comment { ItemID = model.ID, UserID = users.GetUserIdByName(User.Identity.Name), UserName = User.Identity.Name, CommentText = model.CommentText };
            comments.CreateComment(comment);
            comments.SaveDB();
            return RedirectToAction("ItemDetails", new { itemID = model.ID });
        }

        [Authorize(Roles = "admin,user")]
        public IActionResult LikeCreation(int itemID)
        {
            if (!users.GetUser(users.GetUserIdByName(User.Identity.Name)).Unblocked) return View("BlockedView");
            if (!likes.IsLiked(users.GetUserIdByName(User.Identity.Name), itemID))
            {
                var like = new Like { ItemID = itemID, UserID = users.GetUserIdByName(User.Identity.Name) };
                likes.CreateLike(like);
                likes.SaveDB();
            }
            else likes.DeleteLike(users.GetUserIdByName(User.Identity.Name), itemID);
            return RedirectToAction("ItemDetails", new { itemID = itemID });
        }
    }
}
