using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture 
            {
                Id = x.Id,
                ProductId = x.ProductId,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle

            }).FirstOrDefault(x => x.Id == id);
        }

        public ProductPicture GetWithProductAndCategory(long id)
        {
            return _context.ProductPictures
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == id); //به نظرم اگه یه سلکت هم میزدیم که فقط اون 3 و 4 تا فیلد مورد نظر رو بهمون بده از لحاظ عملکرد بهتره
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Include(x => x.Product).Select(x => new ProductPictureViewModel 
            {
                Id = x.Id,
                CreationDate = x.CreationDate.ToString(),
                Picture = x.Picture,
                Product = x.Product.Name,
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved
                
            });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
