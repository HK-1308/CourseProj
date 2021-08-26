using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using CourseProj.Views_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsers users;
        private readonly ICollections collections;
        private readonly IItems items;
        private readonly ILikes likes;
        private readonly IComments comments;

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
            {GetItems = items.ItemsForMainPage(), GetCollections = collections.CollectionsForMainPage(), GetTags = items.AllTags()};
            if (User.Identity.IsAuthenticated) tmp.UserID = users.GetUserIdByName(User.Identity.Name);
            else tmp.UserID = 0;
            return View(tmp);
        }

        public IActionResult UserPage(int userID)
        {
            if (User.IsInRole("blocked") && users.GetUserIdByName(User.Identity.Name) == userID) return View("BlockedView");
            var tmp = new UserPageViewModel
            {UserID = userID,GetCollections = collections.CollectionsByUserID(userID)};
            if (User.Identity.IsAuthenticated) tmp.CurrentUserID = users.GetUserIdByName(User.Identity.Name);
            else tmp.CurrentUserID = 0;
            return View(tmp);
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
            else foreach (User user in users.AllUsers)user.RoleId = 3;
            users.SaveDB();
            if (isMe) return RedirectToAction("Logout", "AccountController");
            else return RedirectToAction("AdminTable");
        }
        public IActionResult TableUnblockButton(int[] selectedUsers)
        {
            if (selectedUsers[0] != -1)  foreach (int v in selectedUsers) users.GetUser(v).Unblocked = true;
            else foreach (User user in users.AllUsers) user.RoleId = 2;
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
        public IActionResult TableDeleteButton(int[] selectedUsers, bool isMe)
        {
            if (selectedUsers[0] != -1)
                for (int i = 0; i < selectedUsers.Length; i++)
                {
                    foreach (var collection in collections.CollectionsByUserID(selectedUsers[i]))
                    {
                        foreach (var item in items.CollectByCollectionID(collection.ID)) items.Delete(item.ID);
                        collections.DeleteCollection(collection.ID);
                    }
                    users.DeleteUser(users.GetUser(selectedUsers[i]));
                }

            else foreach (var user in users.AllUsers)
                {
                    foreach (var collection in collections.CollectionsByUserID(user.ID))
                    {
                        foreach (var item in items.CollectByCollectionID(collection.ID)) items.Delete(item.ID);
                        collections.DeleteCollection(collection.ID);
                    }
                    users.DeleteUser(users.GetUser(user.ID));
                }
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
                foreach (int userID in selectedUsers) if (userID == users.GetUserIdByName(User.Identity.Name)) IsMe = true;
                if (button == "Block") return RedirectToAction("TableBlockButton", new { selectedUsers = selectedUsers, isMe = IsMe });
                if (button == "Unblock") return RedirectToAction("TableUnblockButton", new { selectedUsers = selectedUsers });
                if (button == "Admin") return RedirectToAction("TableAdminButton", new { selectedUsers = selectedUsers });
                if (button == "Delete") return RedirectToAction("TableDeleteButton", new { selectedUsers = selectedUsers, isMe = IsMe });
            }
            return RedirectToAction("AdminTable");
        }

        public IActionResult CreateNewCollection(int userID)
        {
            var tmp = new Collection{userID = userID};
            return View(tmp);
        }

        [HttpPost]
        public IActionResult CreateNewCollection(Collection collection)
        {
            if (ModelState.IsValid)
            {
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
