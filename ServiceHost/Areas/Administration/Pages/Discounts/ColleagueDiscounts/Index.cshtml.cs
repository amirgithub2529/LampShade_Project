using System.Collections.Generic;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts
{
    //[Authorize(Roles = Roles.Administrator)]
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public ColleagueDiscountSearchModel SearchModel;
        public List<ColleagueDiscountViewModel> ColleagueDiscounts;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts_with_no_inventory(), "Id", "Name");
            ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
        }


        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = _productApplication.GetProducts_with_no_inventory()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var colleagueDiscount = _colleagueDiscountApplication.GetDetails(id);
            colleagueDiscount.Products = _productApplication.GetProducts_with_no_inventory();
            return Partial("Edit", colleagueDiscount);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }


        public IActionResult OnGetRemove(long id)
        {
            _colleagueDiscountApplication.Remove(id);
            return RedirectToPage("./Index");
        }


        public IActionResult OnGetRestore(long id)
        {
            _colleagueDiscountApplication.Restore(id);
            return RedirectToPage("./Index");
        }

    }
}
