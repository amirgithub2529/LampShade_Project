﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampshadeQuery.Contracts.ProductCategory
{
    public class ProductCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
       // public string Description { get; set; }  برای سئو میتونیم این رو هم توی ایندکس داشته باشیم ولی هایدش کنیم
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }     
        public string Slug { get; set; }
    }
}
