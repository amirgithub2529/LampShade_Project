using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class PrintProductsModel : PageModel
    {
        public List<ProductViewModel> products;
        private readonly IProductApplication _productApplication;

        public PrintProductsModel(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            products = _productApplication.Search(searchModel);
        }
    }
}
