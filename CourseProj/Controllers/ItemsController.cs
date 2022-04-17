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
        private readonly ITags tags;

        public ItemsController(IUsers users, ICollections collections, IItems items, ILikes likes, IComments comments, ITags tags)
        {
            this.items = items;
            this.collections = collections;
            this.users = users;
            this.likes = likes;
            this.comments = comments;
            this.tags = tags;
        }

        public IActionResult CreateNewItem(int collectionID)
        {
            ItemViewModel model = new ItemViewModel();
            model.Item = new Item { Collection = collections.CollectByID(collectionID) };
            model.Item.CollectionID = model.Item.Collection.ID;
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateNewItem(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                items.CreateItem(model.Item);
                items.SaveDB();
                var itemTags = model.Tags;
                //работа с тэгами тут
                tags.AddTagsForItem(itemTags, model.Item.ID);
                return RedirectToAction("CreateNewItem", new { collectionID = model.Item.CollectionID });
            }
            return View(model);
        }

        public IActionResult EditItem(int itemID)
        {
            var model = new ItemViewModel();
            model.Item = items.CollectByID(itemID);
            model.Item.Collection = collections.CollectByID(model.Item.CollectionID);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditItem(ItemViewModel model)
        {
            //TODO обновление итемов
            items.UpdateInfo(model.Item);
            collections.SaveDB();
            tags.AddTagsForItem(model.Tags, model.Item.ID);
            return RedirectToAction("EditCollection", "Collections", new { collectionID = model.Item.CollectionID });
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
            bool userIsBlocked = !users.GetUser(users.GetUserIdByName(User.Identity.Name)).Unblocked;
            if (userIsBlocked) return View("BlockedView");
            if (!string.IsNullOrEmpty(model.CommentText))
            {
                var comment = new Comment { ItemID = model.Item.ID, UserID = users.GetUserIdByName(User.Identity.Name), UserName = User.Identity.Name, CommentText = model.CommentText };
                comments.CreateComment(comment);
                comments.SaveDB();
            }
            return RedirectToAction("ItemDetails", new { itemID = model.Item.ID });
        }

        [Authorize(Roles = "admin,user")]
        public IActionResult LikeCreation(int itemID)
        {
            bool userIsBlocked = !users.GetUser(users.GetUserIdByName(User.Identity.Name)).Unblocked;
            bool itemIsLikedByUser = likes.IsLiked(users.GetUserIdByName(User.Identity.Name), itemID);
            if (userIsBlocked) return View("BlockedView");
            if (!itemIsLikedByUser)
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
