using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel 
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug

            }).AsNoTracking().ToList();
        }



        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice }).AsNoTracking().ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate , x.EndDate }).AsNoTracking().ToList();

            var category = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    Products = MapProducts(x.Products)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            foreach (var product in category.Products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        var discountRate = discount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                        product.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }

            }

            return category;
        }





        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice }).AsNoTracking().ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToList();

            var categories = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).AsNoTracking().ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;
                        product.Price = price.ToMoney();

                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount != null)
                        {
                            var discountRate = discount.DiscountRate;
                            product.DiscountRate = discountRate;
                            product.HasDiscount = discountRate > 0;
                            var discountAmount = Math.Round((price * discountRate) / 100);
                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }

                }
            }

            return categories;
        }





        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Category = product.Category.Name,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
            }).ToList();
        }








        //کدهای زیر را خودم نوشتم و دقیقا همان دو متد بالا است و هیچ فرقی ندارد

        //public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        //{
        //    var inventory = _inventoryContext.Inventory
        //        .Select(x => new { x.ProductId , x.UnitPrice })
        //        .ToList();

        //    var discounts = _discountContext.CustomerDiscounts
        //        .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
        //        .Select(x => new { x.ProductId , x.DiscountRate })
        //        .ToList();

        //    var categories = _context.ProductCategories
        //        .Include(x => x.Products)
        //        .ThenInclude(x => x.Category)
        //        .Select(x => new ProductCategoryQueryModel
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Products = MapProducts(x.Products)
        //        }).ToList();


        //    foreach (var category in categories)
        //    {
        //        foreach(var product in category.Products)
        //        {
        //            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
        //            if(productInventory != null)
        //            {
        //                var price = productInventory.UnitPrice;
        //                product.Price = price.ToMoney();

        //                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
        //                if(discount != null)
        //                {
        //                    var discountRate = discount.DiscountRate;
        //                    product.HasDiscount = discountRate > 0;
        //                    product.DiscountRate = discountRate;
        //                    var discountAmount = Math.Round((price * discountRate) / 100);
        //                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
        //                }

        //            }
        //        }
        //    }

        //    return categories;
        //}


        //private static List<ProductQueryModel> MapProducts(List<Product> products)
        //{
        //    return products.Select(x => new ProductQueryModel 
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Picture = x.Picture,
        //        PictureAlt = x.PictureAlt,
        //        PictureTitle = x.PictureTitle,
        //        Slug = x.Slug,
        //        Category = x.Category.Name,

        //    }).ToList();
        //}
    }
}
