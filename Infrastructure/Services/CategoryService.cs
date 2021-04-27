using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Category;
using AutoMapper;
using Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CategoryService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<PayloadResult<Category>> CreateCategoryAsync(CategoryCreationModel categoryModel)
        {
            var category = _mapper.Map<CategoryCreationModel, Category>(categoryModel);
            
            await _dataContext.AddAsync(category);
             
            var create = await _dataContext.SaveChangesAsync() > 0;

            if (!create)
                return new PayloadResult<Category>
                {
                    Errors = new[] {"Could not save changes."},
                    Success = false
                };
            
            return new PayloadResult<Category>
            {
                Payload = category
            };
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _dataContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _dataContext.Categories.FirstOrDefaultAsync(x=>x.Id == categoryId);
        }
        
        public async Task<BaseRequestResult> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);

            if (category == null)
                return new RegisterResult
                {
                    Errors = new[] {$"Category with id '{categoryId}' does not exists."}
                };

            _dataContext.Categories.Remove(category);
            
            var delete = await _dataContext.SaveChangesAsync() > 0;

            if (!delete)
                return new BaseRequestResult
                {
                    Errors = new[] {"Could not save changes."},
                    Success = false
                };

            return new BaseRequestResult
            {
                Success = true
            };
        }
        
    }
}