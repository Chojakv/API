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
        Task<PayloadResult<Category>> CreateCategoryAsync(CategoryCreationModel categoryModel);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        Task<BaseRequestResult> DeleteCategoryAsync(Guid categoryId);
        

    }
}