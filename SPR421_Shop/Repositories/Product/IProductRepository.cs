using SPR421_Shop.ViewModels;

namespace SPR421_Shop.Repositories.Product
{
    public interface IProductRepository
    {
        IQueryable<Models.Product> Products { get; }
        IQueryable<Models.Product> GetByCategory(string category, PaginationVM pagination);
        IQueryable<Models.Product> GetByPagination(PaginationVM pagination);
        IQueryable<Models.Product> SetPagination(IQueryable<Models.Product> product, PaginationVM pagination);
    }
}
