﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Category;
using Domain.Domain;

namespace Application.Interfaces
{
    public interface ICategoryService
    { 
        Task<PayloadResult<Category>> CreateCategoryAsync(CategoryCreationModel categoryModel);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        Task<BaseRequestResult> DeleteCategoryAsync(Guid categoryId);
    }
}