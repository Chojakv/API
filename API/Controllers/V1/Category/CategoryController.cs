using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contracts.V1;
using API.Models.Category;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Category
{
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        
        [HttpPost(ApiRoutes.Categories.Create)]
        public async Task<IActionResult> Create([FromForm]CategoryCreationModel model)
        {
            var create = _mapper.Map<Domain.Category>(model);
            await _categoryService.CreateCategoryAsync(create);
        
            var result = _mapper.Map<CategoryDetailsModel>(create);
        
            return CreatedAtAction("Get", new {categoryId = create.Id},result );
        }

        [HttpGet(ApiRoutes.Categories.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            
            return Ok( (_mapper.Map<IEnumerable<CategoryDetailsModel>>(categories)));
            
        }

        [HttpGet(ApiRoutes.Categories.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound("Such category does not exists.");
            }
            return Ok(_mapper.Map<CategoryDetailsModel>(category));
        }
        
        [HttpDelete(ApiRoutes.Categories.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid categoryId)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(categoryId);
            
            if (deleted)
            {
                return NoContent();
            }
            return NotFound("Such category does not exists.");
        }
    }
}