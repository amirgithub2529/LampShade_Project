using System.Collections.Generic;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public ProductSearchModel SearchModel;
        public List<ProductViewModel> products;
        public SelectList ProductCategories;

        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            this._productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        [NeedsPermission(ShopPermissions.ListProducts)]
        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories() , "Id", "Name");
            products =  _productApplication.Search(searchModel);
        }



        [NeedsPermission(ShopPermissions.CreateProduct)]//----------> I add this.
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command);
        }


        [NeedsPermission(ShopPermissions.CreateProduct)]
        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }


        [NeedsPermission(ShopPermissions.EditProduct)] //----------> I add this.
        public IActionResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            product.Categories = _productCategoryApplication.GetProductCategories();
            return Partial("Edit" , product);
        }

        [NeedsPermission(ShopPermissions.EditProduct)]
        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }


        //public IActionResult OnGetNotInStock(long id)
        //{
        //    var result = _productApplication.NotInStock(id);
        //    if (result.IsSuccedded)
        //        return RedirectToPage("./Index");

        //    Message = result.Message;
        //    return RedirectToPage("./Index");

        //}

        //public IActionResult OnGetIsInStock(long id)
        //{
        //    var result = _productApplication.InStock(id);
        //    if (result.IsSuccedded)
        //        return RedirectToPage("./Index");

        //    Message = result.Message;
        //    return RedirectToPage("./Index");
        //}


        



    }
}
