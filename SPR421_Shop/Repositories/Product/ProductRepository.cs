using Microsoft.EntityFrameworkCore;
using SPR421_Shop.ViewModels;

namespace SPR421_Shop.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Models.Product> Products => _context.Products;

        public IQueryable<Models.Product> GetByCategory(string category, PaginationVM pagination)
        {
            var products = Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Amount)
                .Where(p => p.Category!.Name.ToLower() == category.ToLower());
            products = SetPagination(products, pagination);
            return products;
        }

        public IQueryable<Models.Product> GetByPagination(PaginationVM pagination)
        {
            return SetPagination(Products.OrderByDescending(p => p.Amount), pagination);
        }

        public IQueryable<Models.Product> SetPagination(IQueryable<Models.Product> products, PaginationVM pagination)
        {
            pagination.TotalCount = products.Count();
            int pageCount = (int)Math.Ceiling((double)pagination.TotalCount / pagination.PageSize);
            pagination.PageCount = pageCount;

            int page = pagination.Page < 1 ? 1 : pagination.Page;
            page = page > pagination.PageCount ? 1 : page;
            pagination.Page = page;

            products = products.Skip(pagination.PageSize * (pagination.Page - 1)).Take(pagination.PageSize);

            return products;
        }
    }
}
