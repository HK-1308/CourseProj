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
using Microsoft.AspNetCore.Hosting;

namespace CourseProj.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly ICollections collections;
        private readonly IItems items;
        private readonly IWebHostEnvironment hostEnvironment;

        public CollectionsController(ICollections collections, IItems items, IWebHostEnvironment hostEnvironment)
        {
            this.items = items;
            this.collections = collections;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult CreateNewCollection(int userID)
        {
            var model = new CreateCollectionViewModel ();
            model.Collection = new Collection { userID = userID };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCollection(CreateCollectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(model.File.FileName);
                string extension = Path.GetExtension(model.File.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/", fileName);
                using (var fileStream = new FileStream(path,FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                }
                var image = new Image { ImageName = fileName };
                model.Collection.Image = image;
                collections.CreateCollection(model.Collection);
                collections.AddImage(image);
                collections.SaveDB();
                return RedirectToAction("CreateNewItem","Items", new { collectionID = model.Collection.ID });
            }
            return View(model.Collection);
        }

        public IActionResult SortEdit(int collectionID)
        {
            var tmp = new ItemListInCollectionEditModel(collections.CollectByID(collectionID));
            tmp.GetItems = items.CollectByCollectionID(collectionID);
            tmp.GetItems = tmp.GetItems.OrderBy(u => u.Name).ToList();
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
            return RedirectToAction("UserPage","Users", new { userID = userID });
        }

        public IActionResult SortDetails(int collectionID)
        {
            var tmp = new ItemListInCollectionEditModel(collections.CollectByID(collectionID));
            tmp.GetItems = items.CollectByCollectionID(collectionID);
            tmp.GetItems = tmp.GetItems.OrderBy(u => u.Name).ToList();
            return View("CollectionDetails", tmp);
        }

        public ViewResult CollectionDetails(int collectionID)
        {
            var tmp = new ItemListInCollectionEditModel(collections.CollectByID(collectionID));
            tmp.ImageName = collections.GetImageById(collections.CollectByID(collectionID).ImageId).ImageName;
            tmp.GetItems = items.CollectByCollectionID(collectionID);
            return View(tmp);
        }
    }
}
