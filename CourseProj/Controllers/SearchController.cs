using CourseProj.Data;
using CourseProj.Data.Models;
using CourseProj.Views_Models;
using Korzh.EasyQuery.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Controllers
{
    public class SearchController : Controller
    {

        private readonly DBContent dBContent;
        private List<int> CollectionIDs = new List<int>();
        private List<int> ItemsIDs = new List<int>();

        public SearchController(DBContent dBContent)
        {
            this.dBContent = dBContent;
        }

        public IActionResult Search()
        {
            SearchViewModel model = new SearchViewModel();
            model.items = dBContent.Item;
            foreach (Item item in model.items.ToList()) item.Collection = dBContent.Collection.FirstOrDefault(u => u.ID == item.CollectionID);
            return View(model);
        }

        private List<Item> SearchEverywhere(string text)
        {
            foreach (var col in dBContent.Collection.FullTextSearchQuery(text)) CollectionIDs.Add(col.ID);
            foreach (var com in dBContent.Comment.FullTextSearchQuery(text)) ItemsIDs.Add(com.ItemID);
            List<Item> result = dBContent.Item.FullTextSearchQuery(text).ToList();
            foreach (var id in CollectionIDs) result.AddRange(dBContent.Item.Where(u => u.CollectionID == id));
            foreach (var id in ItemsIDs) result.Add(dBContent.Item.FirstOrDefault(u => u.ID == id));
            return result;
        }

        public IActionResult TagSearch(string tag)
        {
            SearchViewModel model = new SearchViewModel();
            if (!string.IsNullOrEmpty(tag))
            {
                model.items = SearchEverywhere(tag).AsQueryable(); 
                foreach (var item in model.items) item.Collection = dBContent.Collection.FirstOrDefault(u => u.ID == item.CollectionID);    
            }
            else model.items = dBContent.Item;
            model.items = model.items.Distinct();
            return View("Search", model);
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel model)
        {

            if (!string.IsNullOrEmpty(model.Text))
            {
                model.items = SearchEverywhere(model.Text).AsQueryable();
                foreach (var item in model.items) item.Collection = dBContent.Collection.FirstOrDefault(u => u.ID == item.CollectionID);
            }
            else model.items = dBContent.Item;
            model.items = model.items.Distinct();
            return View(model);
        }
    }
}
