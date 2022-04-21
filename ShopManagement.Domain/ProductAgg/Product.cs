﻿using _0_Framework.Domain;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        //public double UnitPrice { get; private set; }
        public bool IsDeleted { get; private set; }  //This property added by myself . 
        //public int Stock { get; private set; } //This property added by myself . 
        //public bool IsInStock { get; private set; }
        public bool HasInventory { get; private set; } //This property added by myself .
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public long CategoryId { get; private set; }
        public ProductCategory Category { get; private set; }
        public List<ProductPicture> ProductPictures { get; private set; }
        public List<Comment> Comments { get; private set; }

        public Product(string name, string code, 
            string shortDescription, string description, string picture, 
            string pictureAlt, string pictureTitle, string slug, string keywords,
            string metaDescription, long categoryId)
        {
            Name = name;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
            IsDeleted = false;
            HasInventory = false;
        }


        public void Edit(string name, string code,
            string shortDescription, string description, string picture,
            string pictureAlt, string pictureTitle, string slug, string keywords,
            string metaDescription, long categoryId)
        {
            Name = name;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            
            if(!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
        }



        public void InventoryCreated()
        {
            HasInventory = true;
        }


        public void InventoryDelete()
        {
            HasInventory = false;
        }


        //public void InStock()
        //{
        //    IsInStock = true;
        //}


        //public void NotInStock()
        //{
        //    IsInStock = false;
        //}
    }
}
