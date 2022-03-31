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
    public class CollectionsController : Controller
    {
        private readonly ICollections collections;
        private readonly IItems items;

        public CollectionsController(ICollections collections, IItems items)
        {
            this.items = items;
            this.collections = collections;
        }

        public IActionResult CreateNewCollection(int userID)
        {
            var tmp = new Collection { userID = userID };
            return View(tmp);
        }

        [HttpPost]
        public IActionResult CreateNewCollection(Collection collection)
        {
            if (ModelState.IsValid)
            {
                collections.CreateCollection(collection);
                collections.SaveDB();
                return RedirectToAction("CreateNewItem","ItemsController", new { collectionID = collection.ID });
            }
            return View(collection);
        }

        public IActionResult SortEdit(int collectionID)
        {
            var tmp = new ItemListInCollectionEditModel(collections.CollectByID(collectionID));
            tmp.GetItems = items.CollectByCollectionID(collectionID);
            tmp.GetItems = tmp.GetItems.OrderBy(u => u.Name);
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
            return RedirectToAction("EditCollection", new { collectionID = collection.ID });
        }

        public IActionResult DeleteCollection(int collectionID)
        {
            var userID = collections.CollectByID(collectionID).userID;
            foreach (var item in items.CollectByCollectionID(collectionID)) items.Delete(item.ID);
            collections.DeleteCollection(collectionID);
            collections.SaveDB();
            return RedirectToAction("UserPage", new { userID = userID });
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
    }
}
