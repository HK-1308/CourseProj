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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

namespace CourseProj.Controllers
{
    public class FileGeneratorController : Controller
    {
        private readonly IUsers users;
        private readonly IWebHostEnvironment hostEnvironment;
        public FileGeneratorController(IUsers users, IWebHostEnvironment hostEnvironment)
        {
            this.users = users;
            this.hostEnvironment = hostEnvironment;
        }
        private string GetLetterOfColumn(int numberOfColumn)
        {
            switch (numberOfColumn)
            {
                case 1: return "A";
                case 2: return "B";
                case 3: return "C";
                case 4: return "D";
                case 5: return "E";
                default: return "F";
            }
        }

        private void CreateXlxFileUsingTemplate(string path)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileTemplateName = "FavoritesTemplate.xlsx";
            string templatePath = Path.Combine(wwwRootPath + "/files/", fileTemplateName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                using (var fileTemplateStream = new FileStream(templatePath, FileMode.Open))
                {
                    fileTemplateStream.CopyTo(fileStream);
                }
            }
        }
        private string GeneratePathForXLSX()
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = "Favorites";
            string extension = ".xlsx";
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            return Path.Combine(wwwRootPath + "/files/", fileName);
        }
        public FileResult GenerateXLSX()
        {
            string path = GeneratePathForXLSX();
            CreateXlxFileUsingTemplate(path);

            // Open the copied template workbook. 
            using (SpreadsheetDocument myWorkbook = SpreadsheetDocument.Open(path,true))
            {
                // Access the main Workbook part, which contains all references.
                WorkbookPart workbookPart = myWorkbook.WorkbookPart;

                // Get the first worksheet. 
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.ElementAt(0);

                // The SheetData object will contain all the data.
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Begining Row pointer                       
                int index = 2;

                // Database results
                List<Data.Models.Item> items = users.GetItemsForUserFavoritePage(User.Identity.Name);

                // For each item in the database, add a Row to SheetData.
                foreach (var item in items)
                {
                    // Cell related variable
                    string name = item.Name;
                    string tags = GetItemTags(item);
                    string collectionName = item.Collection.Name;
                    string collectionTheme = item.Collection.Theme;
                    string likesCount = Convert.ToString(item.likes.Count());

                    // New Row
                    Row row = new Row();
                    row.RowIndex = (UInt32)index;

                    for (int columnNumber = 1; columnNumber < 6; columnNumber++)
                    {
                        string letter = GetLetterOfColumn(columnNumber);
                        // New Cell
                        Cell cell = new Cell();
                        cell.DataType = CellValues.InlineString;


                        // Column A1, 2, 3 ... and so on
                        cell.CellReference = letter + index;

                        // Create Text object
                        Text text = new Text();
                        switch(columnNumber)
                        {
                            case 1: text.Text = name; break;
                            case 2: text.Text = tags; break;
                            case 3: text.Text = collectionName; break;
                            case 4: text.Text = collectionTheme; break;
                            case 5: text.Text = likesCount; break;
                            default: text.Text = "ERROR"; break;
                        }

                        // Append Text to InlineString object
                        InlineString inlineString = new InlineString();
                        inlineString.AppendChild(text);

                        // Append InlineString to Cell
                        cell.AppendChild(inlineString);

                        // Append Cell to Row
                        row.AppendChild(cell);
                    }
                    // Append Row to SheetData
                    sheetData.AppendChild(row);

                    // increase row pointer
                    index++;

                }
                // save
                worksheetPart.Worksheet.Save();
                return PhysicalFile(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FavoriteItems.xlsx");
            }
        }

        public FileResult GenerateXML()
        {
            List<Data.Models.Item> items = users.GetItemsForUserFavoritePage(User.Identity.Name);
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
        private string GetItemTags(Data.Models.Item item)
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
