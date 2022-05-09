using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.Models
{
    public class Collection
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual Image Image { get; set; }
        public int ImageId { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public int userID { get; set; }
        public virtual User user  { get; set; }
        public IEnumerable <Item> Items { get; set; }
      
        public bool NumericField1_visible { get; set; }
        public string NumericField1_name { get; set; }

        public bool NumericField2_visible { get; set; }
        public string NumericField2_name { get; set; }

        public bool NumericField3_visible { get; set; }
        public string NumericField3_name { get; set; }

        public bool StringField1_visible { get; set; }
        public string StringField1_name { get; set; }

        public bool StringField2_visible { get; set; }
        public string StringField2_name { get; set; }

        public bool StringField3_visible { get; set; }
        public string StringField3_name { get; set; }

        public bool TextField1_visible { get; set; }
        public string TextField1_name { get; set; }

        public bool TextField2_visible { get; set; }
        public string TextField2_name { get; set; }

        public bool TextField3_visible { get; set; }
        public string TextField3_name { get; set; }

        public bool DateField1_visible { get; set; }
        public string DateField1_name { get; set; }

        public bool DateField2_visible { get; set; }
        public string DateField2_name { get; set; }

        public bool DateField3_visible { get; set; }
        public string DateField3_name { get; set; }

        public bool BooleanField1_visible { get; set; }
        public string BooleanField1_name { get; set; }

        public bool BooleanField2_visible { get; set; }
        public string BooleanField2_name { get; set; }

        public bool BooleanField3_visible { get; set; }
        public string BooleanField3_name { get; set; }

    }
}
