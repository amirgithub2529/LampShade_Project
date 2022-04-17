using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> productCategories;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            this._productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
           productCategories =  _productCategoryApplication.Search(searchModel);
        }


        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication.GetDetails(id);
            return Partial("Edit" , productCategory);
        }

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
