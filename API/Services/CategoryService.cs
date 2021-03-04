using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Domain;
using API.Models.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<bool> CreateCategoryAsync(Category categoryModel)
        {
            await _dataContext.AddAsync(categoryModel);
             
            return await _dataContext.SaveChangesAsync() > 0;
        }
        

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _dataContext.Categories.Include(x=>x.Ads).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _dataContext.Categories.FirstOrDefaultAsync(x=>x.Id == categoryId);
            //return await _dataContext.Categories.Include(x=>x.Ads).FirstOrDefaultAsync(x=>x.Id == categoryId);
        }
        
        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);

            if (category == null)
                return false;

            _dataContext.Categories.Remove(category);
            return await _dataContext.SaveChangesAsync() > 0;
        }
        
    }
}