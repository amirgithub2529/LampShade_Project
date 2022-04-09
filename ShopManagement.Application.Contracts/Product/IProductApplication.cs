using _0_Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        EditProduct GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts_with_no_inventory();
        List<ProductViewModel> GetProducts();
        OperationResult InventoryCreated(long id);
        OperationResult InventoryDeleted(long id);
        //OperationResult InStock(long id);
        //OperationResult NotInStock(long id);
    }
}
