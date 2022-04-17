using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture,path);

            var product = new Product(command.Name, command.Code, 
                command.ShortDescription, command.Description,
                picturePath, command.PictureAlt, command.PictureTitle,
                slug, command.Keywords, command.MetaDescription, command.CategoryId);

            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var path = $"{product.Category.Slug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            product.Edit(command.Name, command.Code,
                command.ShortDescription, command.Description,
                picturePath, command.PictureAlt, command.PictureTitle,
                slug, command.Keywords, command.MetaDescription, command.CategoryId);

            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }


        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }


        public List<ProductViewModel> GetProducts_with_no_inventory()
        {
            return _productRepository.GetProducts_with_no_inventory();
        }




        public OperationResult InventoryCreated(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            product.InventoryCreated();
            _productRepository.SaveChanges();
            return operation.Succedded();
        }




        public OperationResult InventoryDeleted(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            product.InventoryDelete();
            _productRepository.SaveChanges();
            return operation.Succedded();
        }




        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }


        //public OperationResult InStock(long id)
        //{
        //    var operation = new OperationResult();
        //    var product = _productRepository.Get(id);
        //    if (product == null)
        //        return operation.Failed(ApplicationMessages.RecordNotFound);

        //    product.InStock();
        //    _productRepository.SaveChanges();
        //    return operation.Succedded();
        //}



        //public OperationResult NotInStock(long id)
        //{
        //    var operation = new OperationResult();
        //    var product = _productRepository.Get(id);
        //    if (product == null)
        //        return operation.Failed(ApplicationMessages.RecordNotFound);

        //    product.NotInStock();
        //    _productRepository.SaveChanges();
        //    return operation.Succedded();
        //}




    }
}
