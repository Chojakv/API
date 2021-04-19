using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Application.Interfaces;
using Application.Models.Ad;
using Application.Models.Queries;
using AutoMapper;
using Contracts.Contracts.V1;
using Domain.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Ads
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdsController : ControllerBase
    {

        private readonly IAdService _adService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUriService _uriService;
        
        public AdsController(IAdService adService, IMapper mapper, IWebHostEnvironment webHostEnvironment, IUriService uriService)
        {
            
            _adService = adService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _uriService = uriService;
        }
        
        [HttpPost(ApiRoutes.Ads.Create)]
        public async Task<IActionResult> Create([FromForm] AdCreationModel model)
        {
            var create = await _adService.CreateAdAsync(HttpContext.GetUserId(), model);
            
            var locationUri = _uriService.GetAdUri(create.Payload.Id.ToString());

            return Created(locationUri, _mapper.Map<AdDetailsModel>(create.Payload));
        }

        [HttpGet(ApiRoutes.Ads.Get)]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] Guid adId)
        {
            var ad = await _adService.GetAdByIdAsync(adId);
            
            if (ad == null)
            { 
                return NotFound("Such ad does not exists.");
            }
            return Ok(_mapper.Map<AdDetailsModel>(ad));
        }

        [HttpGet(ApiRoutes.Ads.GetAll)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAdsQueries queries, [FromQuery] PaginationQuery pagingQuery, [FromQuery] string sort)
        {
            var filters = _mapper.Map<GetAllAdsFilters>(queries);

            var paging = _mapper.Map<PaginationFilters>(pagingQuery);
            
            var ads = await _adService.GetAdsAsync(filters, paging, sort);
            
            return Ok((_mapper.Map<IEnumerable<AdDetailsModel>>(ads)));
           
        }
        
        // [HttpGet(ApiRoutes.Ads.GetByCategory)]
        // [AllowAnonymous]
        // public async Task<IActionResult> GetByCategory([FromRoute]Guid categoryId, [FromQuery] GetAllAdsQueries queries, [FromQuery]PaginationQuery pagingQuery)
        // {
        //     var filers = _mapper.Map<GetAllAdsFilters>(queries);
        //     var paging = _mapper.Map<PaginationFilters>(pagingQuery);
        //     
        //     var ads = await _adService.GetAdsByCategory(categoryId, filers, paging);
        //     
        //     if (ads.Any())  
        //     {
        //         return Ok((_mapper.Map<IEnumerable<AdDetailsModel>>(ads)));
        //     }
        //     
        //     return NotFound("Ads with that category does not exists.");
        // }

        [HttpPatch(ApiRoutes.Ads.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid adId,[FromForm] AdUpdateModel model)
        {
            var userOwnsPost = await _adService.UserOwnsPostAsync(adId, HttpContext.GetUserId());
            if (!userOwnsPost.Success)
            {
                return BadRequest(new { error = "You do not own this ad." });
            }
            var update = await _adService.UpdateAdAsync(adId, model);
            
            if (update.Success)
            {
                return Ok(_mapper.Map<AdDetailsModel>(update.Payload));
            }
            
            return NotFound("Such ad does not exists.");
        }
        
        [HttpDelete(ApiRoutes.Ads.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid adId)
        {
            var userOwnsPost = await _adService.UserOwnsPostAsync(adId, HttpContext.GetUserId());
            
            if (!userOwnsPost.Success)
            {
                return BadRequest(new { error = "You do not own this ad" });
            }
            
            var deleted = await _adService.DeleteAdAsync(adId);

            if (deleted.Success)
            {
                return NoContent();
            }
            
            return NotFound("Such ad does not exists.");
        }
    }
}