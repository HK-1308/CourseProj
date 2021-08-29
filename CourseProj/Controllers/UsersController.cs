﻿using CloudinaryDotNet;
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
        private readonly ILikes likes;
        private readonly IComments comments;
        private const string CLOUD_NAME = "duymqmdpp";
        private const string API_KEY = "689235495947759";
        private const string API_SECRET = "34eqfnzFmGuixL1JQrBJwXy_zbE";


        public UsersController(IUsers users, ICollections collections, IItems items, ILikes likes, IComments comments)
        {
            this.items = items;
            this.collections = collections;
            this.users = users;
            this.likes = likes;
            this.comments = comments;
        }

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
            {UserID = userID,GetCollections = collections.CollectionsByUserID(userID)};
            if (users.GetUserIdByName(User.Identity.Name) == userID && !users.GetUser(userID).Unblocked) return View("BlockedView");
            if (User.Identity.IsAuthenticated) tmp.CurrentUserID = users.GetUserIdByName(User.Identity.Name);
            else tmp.CurrentUserID = 0;
            return View(tmp);
        }

        public IActionResult MyPage()
        {
            return RedirectToAction("UserPage",new { userID = users.GetUserIdByName(User.Identity.Name)});
        }

        [Authorize(Roles = "admin")]
        public ViewResult AdminTable()
        {
            var mod = new UsersListViewModel {GetUsers = users.AllUsers };
            return View(mod);
        }

        public IActionResult TableBlockButton(int[] selectedUsers,bool isMe)
        {
            if(selectedUsers[0]!=-1) foreach (int v in selectedUsers) if (users.GetUser(v).RoleId != 1) users.GetUser(v).Unblocked = false;
            else foreach (User user in users.AllUsers)user.Unblocked = false;
            users.SaveDB();
            if (isMe) return RedirectToAction("Logout", "AccountController");
            else return RedirectToAction("AdminTable");
        }
        public IActionResult TableUnblockButton(int[] selectedUsers)
        {
            if (selectedUsers[0] != -1)  foreach (int v in selectedUsers) users.GetUser(v).Unblocked = true;
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

        private void TableDeleteAll ()
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
            if (isMe) return RedirectToAction("Logout", "AccountController");
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

        public IActionResult CreateNewCollection(int userID)
        {
            var tmp = new Collection { userID = userID};
            return View(tmp);
        }

        [HttpPost]
        public IActionResult CreateNewCollection(Collection collection)
        {
            if (ModelState.IsValid)
            {
                //Cloudinary cloudinary = new Cloudinary(new Account(CLOUD_NAME, API_KEY, API_SECRET));
                //var uploadParams = new ImageUploadParams
                //{
                //    File = new FileDescription(collection.img.FileName, new FileStream("path",FileMode.Create,FileAccess.Write)),
                //    //transformation code here
                //    Transformation = new Transformation().Width(200).Height(200).Crop("thumb").Gravity("face")
                //};
                //var uploadResult = cloudinary.Upload(uploadParams);
                //Collection tmp = new Collection { Name = collection.Name, img = uploadResult.Url.ToString() };
                collections.CreateCollection(collection);
                collections.SaveDB();
                return RedirectToAction("CreateNewItem", new {collectionID = collection.ID });
            }
            return View(collection);
        }

        public IActionResult CreateNewItem(int collectionID)
        {
            var CurrentItem = new Item{ Collection = collections.CollectByID(collectionID)};
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
                return RedirectToAction("CreateNewItem",new { collectionID = item.CollectionID});
            }    
            return View(item);
        }

        public IActionResult SortEdit(int collectionID)
        {
            var tmp = new ItemListInCollectionEditModel(collections.CollectByID(collectionID));
            tmp.GetItems = items.CollectByCollectionID(collectionID);
            tmp.GetItems=tmp.GetItems.OrderBy(u => u.Name);
            return View("EditCollection", tmp);
        }

        public IActionResult EditCollection(int collectionID)
        {
            var tmp = new ItemListInCollectionEditModel(collections.CollectByID(collectionID));
            tmp.GetItems = items.CollectByCollectionID(collectionID);
            return View(tmp);
        }
        
        [HttpPost]
        public IActionResult EditCollection(Collection collection)
        {
            collections.UpdateInfo(collection);
            return RedirectToAction("EditCollection", new {collectionID = collection.ID});
        }

        public IActionResult DeleteCollection(int collectionID)
        {
            var userID = collections.CollectByID(collectionID).userID;
            foreach (var item in items.CollectByCollectionID(collectionID)) items.Delete(item.ID);
            collections.DeleteCollection(collectionID);
            collections.SaveDB();
            return RedirectToAction("UserPage", new {userID = userID});
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
            return RedirectToAction("EditCollection",new { collectionID = item.Collection.ID});
        }

        public IActionResult DeleteItem(int itemID)
        {
            int collectionID = items.CollectByID(itemID).CollectionID;
            items.Delete(itemID);
            items.SaveDB();
            return RedirectToAction("EditCollection",new { collectionID = collectionID});
        }
        public IActionResult SortDetails(int collectionID)
        {
            var tmp = new ItemListInCollectionEditModel(collections.CollectByID(collectionID));
            tmp.GetItems = items.CollectByCollectionID(collectionID);
            tmp.GetItems = tmp.GetItems.OrderBy(u => u.Name);
            return View("CollectionDetails", tmp);
        }
        public ViewResult CollectionDetails(int collectionID) 
        {
            var tmp = new ItemListInCollectionEditModel(collections.CollectByID(collectionID));
            tmp.GetItems = items.CollectByCollectionID(collectionID);
            return View(tmp);
        }

        public IActionResult ItemDetails(int itemID)
        {
            var CurrentItem  = items.CollectByID(itemID);
            CurrentItem.Collection = collections.CollectByID(CurrentItem.CollectionID);
            ItemDetailsViewModel tmp = new ItemDetailsViewModel(CurrentItem){GetItemLikes = likes.GetByItemID(itemID), GetItemComments = comments.GetByItemID(itemID)};
            return View(tmp);
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost]
        public IActionResult ItemDetails(ItemDetailsViewModel model)
        {
            if(!users.GetUser(users.GetUserIdByName(User.Identity.Name)).Unblocked) return View("BlockedView");
            var comment = new Comment {ItemID = model.ID,UserID = users.GetUserIdByName(User.Identity.Name),UserName = User.Identity.Name,CommentText = model.CommentText};
            comments.CreateComment(comment);
            comments.SaveDB();
            return RedirectToAction("ItemDetails", new { itemID = model.ID });
        }

        public IActionResult SortTags()
        {
            var tmp = new MainPageViewModel
            { GetItems = items.ItemsForMainPage(), GetCollections = collections.CollectionsForMainPage(), GetTags = items.AllTags(), Tmp = items.AllTags().OrderBy(u => u).ToList() };
            if (User.Identity.IsAuthenticated) tmp.UserID = users.GetUserIdByName(User.Identity.Name);
            else tmp.UserID = 0;
            return View("MainPage", tmp);
        }

        [Authorize (Roles="admin,user")]
        public IActionResult LikeCreation(int itemID)
        {
            if (!users.GetUser(users.GetUserIdByName(User.Identity.Name)).Unblocked) return View("BlockedView");
            if (!likes.IsLiked(users.GetUserIdByName(User.Identity.Name), itemID))
            {
                var like = new Like  { ItemID = itemID,UserID = users.GetUserIdByName(User.Identity.Name)};
                likes.CreateLike(like);
                likes.SaveDB();
            }
            else likes.DeleteLike(users.GetUserIdByName(User.Identity.Name), itemID);
            return RedirectToAction("ItemDetails", new { itemID = itemID });
        }
    }
}
