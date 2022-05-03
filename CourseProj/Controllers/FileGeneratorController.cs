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
using System.Xml.Serialization;
using System.Xml;
using Microsoft.AspNetCore.Hosting;

namespace CourseProj.Controllers
{
    public class FileGeneratorController:Controller
    {
        private readonly IUsers users;
        private readonly ICollections collections;
        private readonly IItems items;
        private readonly ILikes likes;
        private readonly IComments comments;
        private readonly ITags tags;
        private readonly IWebHostEnvironment hostEnvironment;
        public FileGeneratorController(IUsers users, ICollections collections, IItems items, ILikes likes, IComments comments, ITags tags, IWebHostEnvironment hostEnvironment)
        {
            this.items = items;
            this.collections = collections;
            this.users = users;
            this.likes = likes;
            this.comments = comments;
            this.tags = tags;
            this.hostEnvironment = hostEnvironment;
        }

        public FileResult GenerateXML()
        {
            List<Item> items = users.GetItemsForUserFavoritePage(User.Identity.Name);
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = "Favorite";
            string extension = ".xml";
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/files/", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                XmlTextWriter xmlWriter = new XmlTextWriter(stream, System.Text.Encoding.Default);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Items");
                xmlWriter.WriteStartElement("Item");
                foreach (var item in items)
                {
                    string tags = GetItemTags(item);

                    xmlWriter.WriteElementString("ItemName", item.Name);
                        xmlWriter.WriteStartElement("Collection");
                        xmlWriter.WriteElementString("CollectionName", item.Collection.Name);
                        xmlWriter.WriteElementString("CollectionTheme", item.Collection.Theme);
                        xmlWriter.WriteEndElement();
                    xmlWriter.WriteElementString("Tags", tags); 
                    xmlWriter.WriteElementString("LikesCount", Convert.ToString(item.likes.Count()));

                    if(!string.IsNullOrEmpty(item.Collection.NumericField1_name))
                        xmlWriter.WriteElementString($"{item.Collection.NumericField1_name}", Convert.ToString(item.NumericField1));
                    if (!string.IsNullOrEmpty(item.Collection.NumericField2_name))
                        xmlWriter.WriteElementString($"{item.Collection.NumericField2_name}", Convert.ToString(item.NumericField2));
                    if (!string.IsNullOrEmpty(item.Collection.NumericField3_name))
                        xmlWriter.WriteElementString($"{item.Collection.NumericField3_name}", Convert.ToString(item.NumericField3));

                    if (!string.IsNullOrEmpty(item.Collection.StringField1_name))
                        xmlWriter.WriteElementString($"{item.Collection.StringField1_name}", Convert.ToString(item.StringField1));
                    if (!string.IsNullOrEmpty(item.Collection.StringField2_name))
                        xmlWriter.WriteElementString($"{item.Collection.StringField2_name}", Convert.ToString(item.StringField2));
                    if (!string.IsNullOrEmpty(item.Collection.StringField3_name))
                        xmlWriter.WriteElementString($"{item.Collection.StringField3_name}", Convert.ToString(item.StringField3));

                    if (!string.IsNullOrEmpty(item.Collection.TextField1_name))
                        xmlWriter.WriteElementString($"{item.Collection.TextField1_name}", Convert.ToString(item.TextField1));
                    if (!string.IsNullOrEmpty(item.Collection.TextField2_name))
                        xmlWriter.WriteElementString($"{item.Collection.TextField2_name}", Convert.ToString(item.TextField2));
                    if (!string.IsNullOrEmpty(item.Collection.TextField3_name))
                        xmlWriter.WriteElementString($"{item.Collection.TextField3_name}", Convert.ToString(item.TextField3));

                    if (!string.IsNullOrEmpty(item.Collection.DateField1_name))
                        xmlWriter.WriteElementString($"{item.Collection.DateField1_name}", Convert.ToString(item.DateField1));
                    if (!string.IsNullOrEmpty(item.Collection.DateField2_name))
                        xmlWriter.WriteElementString($"{item.Collection.DateField2_name}", Convert.ToString(item.DateField2));
                    if (!string.IsNullOrEmpty(item.Collection.DateField3_name))
                        xmlWriter.WriteElementString($"{item.Collection.DateField3_name}", Convert.ToString(item.DateField3));

                    if (!string.IsNullOrEmpty(item.Collection.BooleanField1_name))
                        xmlWriter.WriteElementString($"{item.Collection.BooleanField1_name}", Convert.ToString(item.BooleanField1));
                    if (!string.IsNullOrEmpty(item.Collection.BooleanField2_name))
                        xmlWriter.WriteElementString($"{item.Collection.BooleanField2_name}", Convert.ToString(item.BooleanField2));
                    if (!string.IsNullOrEmpty(item.Collection.BooleanField3_name))
                        xmlWriter.WriteElementString($"{item.Collection.BooleanField3_name}", Convert.ToString(item.BooleanField3));

                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();

                return PhysicalFile(path, "text/xml", "FavoriteItems.xml");
            }
        }
        private string GetItemTags(Item item)
        {
            List<string> tagsList = new List<string>();
            var tagsObjectList = item.tags;
            foreach (var tagObject in tagsObjectList)
            {
                tagsList.Add(tagObject.tag);
            }
            return $"#{string.Join("#", tagsList)}";
        }
    }        
}
