using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopManagement.Presentation.Api
{
    //وقتی اتریبیوت ای پی آی کنترلر در اینجا قرار میگیرد یعنی این کنترلر
    // دیگه از روت سیستم پیروی نمیکنه و
    // روت اختصاصی خودش رو داره که
    // میبینید که این روت اختصاصی با اتریبیوت روت مشخص شده
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }


        [HttpGet]
        public List<ProductQueryModel> GetLatestArrivals()
        {
            return _productQuery.GetLatestArrivals();
        }
    }
}
