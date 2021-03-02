using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts.V1;
using API.Domain;
using API.Models.Ad;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.V1
{
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
            
            var create = _mapper.Map<Ad>(model);

            await _adService.CreateAdAsync(create);

            var result = _mapper.Map<AdDetailsModel>(create);

            return CreatedAtAction("Get", new {adId = create.Id}, result);
        }

        [HttpGet(ApiRoutes.Ads.Get)]
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
        public async Task<IActionResult> GetAll()
        {
            var ads = await _adService.GetAdsAsync();

            return Ok((_mapper.Map<IEnumerable<AdDetailsModel>>(ads)));
        }

        [HttpGet(ApiRoutes.Ads.GetByCategory)]
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

            var result = await _adService.UpdateAdAsync(model);
            
            if (result)
            {
                return NoContent();
            }
            
            return NotFound("Such ad does not exists.");

        }

        [HttpDelete(ApiRoutes.Ads.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid adId)
        {
            var deleted = await _adService.DeleteAdAsync(adId);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound("Such ad does not exists.");
        }
        
    }
}