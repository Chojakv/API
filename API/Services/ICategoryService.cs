using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;
using API.Models.Category;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Services
{
    public interface ICategoryService
    { 
        Task<bool> CreateCategoryAsync(Category categoryModel);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
        

    }
}