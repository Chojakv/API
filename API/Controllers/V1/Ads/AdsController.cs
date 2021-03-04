using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts.V1;
using API.Domain;
using API.Extensions;
using API.Models.Ad;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Ads
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdsController : ControllerBase
    {

        private readonly IAdService _adService;
        private readonly IMapper _mapper;

        public AdsController(IAdService adService, IMapper mapper)
        {
            _adService = adService;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Ads.Create)]
        public async Task<IActionResult> Create([FromForm] AdCreationModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var create = _mapper.Map<Ad>(model);
        
            await _adService.CreateAdAsync(HttpContext.GetUserId(), create);
        
            var result = _mapper.Map<AdDetailsModel>(create);
        
            return CreatedAtAction("Get", new {adId = create.Id, userId = create.UserId}, result);
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
        public async Task<IActionResult> GetAll()
        {
            var ads = await _adService.GetAdsAsync();
            
            if (ads.Any())
            {
                return Ok((_mapper.Map<IEnumerable<AdDetailsModel>>(ads)));
            }

            return NotFound("Ads does not exists.");
        }

        [HttpGet(ApiRoutes.Ads.GetByCategory)]
        [AllowAnonymous]
        public async Task<IActionResult> GetByCategory([FromRoute]Guid categoryId)
        {
            var ads = await _adService.GetAdsByCategory(categoryId);
            
            if (ads.Any())  
            {
                return Ok((_mapper.Map<IEnumerable<AdDetailsModel>>(ads)));
            }
            
            return NotFound("Ads with that category does not exists.");
        }

        [HttpPut(ApiRoutes.Ads.Update)]
        public async Task<IActionResult> Update([FromForm] AdUpdateModel model)
        {
            
            var userOwnsPost = await _adService.UserOwnsPostAsync(model.Id, HttpContext.GetUserId());
            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }

            var ad = await _adService.GetAdByIdAsync(model.Id);
            
            var result = await _adService.UpdateAdAsync(_mapper.Map<AdUpdateModel>(ad));
            
            if (result)
            {
                return NoContent();
            }
            
            return NotFound("Such ad does not exists.");

        }

        [HttpDelete(ApiRoutes.Ads.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid adId)
        {
            var userOwnsPost = await _adService.UserOwnsPostAsync(adId, HttpContext.GetUserId());
            
            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }
            
            var deleted = await _adService.DeleteAdAsync(adId);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound("Such ad does not exists.");
        }
        
        
        
    }
}