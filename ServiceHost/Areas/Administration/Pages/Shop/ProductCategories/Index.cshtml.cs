using System.Collections.Generic;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    //[Authorize(Roles ="1,3")]  --------------------> این روش جالب نیست . به جاش توی استارتاپ باید پالیسی تعریف کنیم
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> productCategories;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            this._productCategoryApplication = productCategoryApplication;
        }


        [NeedsPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(ProductCategorySearchModel searchModel)
        {
           productCategories =  _productCategoryApplication.Search(searchModel);
        }


        [NeedsPermission(ShopPermissions.CreateProductCategory)] //--------> I add this.
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }


        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }


        [NeedsPermission(ShopPermissions.EditProductCategory)] //--------> I add this.
        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication.GetDetails(id);
            return Partial("Edit" , productCategory);
        }


        [NeedsPermission(ShopPermissions.EditProductCategory)]
        public JsonResult OnPostEdit(EditProductCategory command)
        {
            //if (ModelState.IsValid)
            //{
            //    //do some thing ...
            //} ------------------------------>چون ما از اونبترسیو جی کوئری استفاده میکنیم
            //  ------------------------------>دیگر برای ولیدیشن فرم از این کد استفاده نمیکنیم
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}
