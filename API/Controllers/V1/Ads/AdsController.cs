using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts.V1;
using API.Data;
using API.Domain;
using API.Extensions;
using API.Models.Ad;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

            try
            {
                var create = _mapper.Map<Ad>(model);

                await _adService.CreateAdAsync(HttpContext.GetUserId(), create);

                var result = _mapper.Map<AdDetailsModel>(create);

                return CreatedAtAction(nameof(Get), new {adId = create.Id, userId = create.UserId}, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
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
        public async Task<IActionResult> GetAll([FromQuery] string bookname, string title, string author)
        {
            var ads = await _adService.GetAdsAsync(bookname, title, author);
            
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

        [HttpPatch(ApiRoutes.Ads.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid adId,[FromForm] AdUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
        
            var userOwnsPost = await _adService.UserOwnsPostAsync(adId, HttpContext.GetUserId());
            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post." });
            }
        
            var result = await _adService.UpdateAdAsync(adId, model);
            
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