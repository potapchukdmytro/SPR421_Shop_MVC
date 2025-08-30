using SPR421_Shop.Models;

namespace SPR421_Shop.Repositories.Categories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        void Create(Category model);
        void Update(Category model);
        void Delete(Category model);
        Category? GetById(int id);
        Category? GetByName(string name);
        bool IsExists(string name);
    }
}
