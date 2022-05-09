﻿using CourseProj.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Views_Models
{
    public class ItemViewModel
    {
        public Item Item { get; set; }

        public string Tags { get; set; }

        public IFormFile File { get; set; }
    }
}
