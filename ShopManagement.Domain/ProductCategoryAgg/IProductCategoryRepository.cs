using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long , ProductCategory>
    {
        //----These are Unused because of IRepository<long , ProductCategory> ----
        //void Create(ProductCategory entity);     
        //ProductCategory Get(long id);
        //bool Exists(Expression<Func<ProductCategory,bool>> expression);
        //void SaveChanges();
        //////////List<ProductCategory> GetAll();


        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
