using CloudinaryDotNet;
using CourseProj.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Views_Models
{
    public class CreateCollectionViewModel
    {
       public Collection Collection { get; set; }

        public IFormFile File { get; set; }
    }
}