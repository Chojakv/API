using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Category;
using AutoMapper;
using Contracts.Contracts.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Category
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        
        public CategoryController(ICategoryService categoryService, IMapper mapper, IUriService uriService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _uriService = uriService;
        }
        
        /// <summary>
        ///  Creates category in the database
        /// </summary>
        [HttpPost(ApiRoutes.Categories.Create)]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm]CategoryCreationModel model)
        {
            var create = await _categoryService.CreateCategoryAsync(model);

            var locationUri = _uriService.GetCategoryUri(create.Payload.Id.ToString());

            return Created(locationUri, _mapper.Map<CategoryDetailsModel>(create.Payload));
        }

        /// <summary>
        ///  Returns all categories from the database
        /// </summary>
        [HttpGet(ApiRoutes.Categories.GetAll)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            
            return Ok( (_mapper.Map<IEnumerable<CategoryDetailsModel>>(categories)));
            
        }

        /// <summary>
        ///  Returns single category from the database
        /// </summary>
        [HttpGet(ApiRoutes.Categories.Get)]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute]Guid categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound("Such category does not exists.");
            }
            return Ok(_mapper.Map<CategoryDetailsModel>(category));
        }
        
        
        /// <summary>
        ///  Deletes category from the database
        /// </summary>
        [HttpDelete(ApiRoutes.Categories.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid categoryId)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(categoryId);
            
            if (deleted.Success)
            {
                return NoContent();
            }
            return NotFound("Such category does not exists.");
        }
    }
}