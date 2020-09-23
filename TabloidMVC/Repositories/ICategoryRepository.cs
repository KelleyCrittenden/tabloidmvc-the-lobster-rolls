using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetCategoryById(int id);
        void UpdateCategory(Category category);
        void Add(Category category);
        void DeleteCategory(Category category);



    }
}