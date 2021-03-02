using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;
using API.Models.Ad;

namespace API.Services
{
    public interface IAdService
    {
        Task<bool> CreateAdAsync(Ad adModel);
        Task<IEnumerable<Ad>> GetAdsAsync();
        Task<Ad> GetAdByIdAsync(Guid adId);
        Task<IEnumerable<Ad>> GetAdsByCategory(Guid categoryId);
        Task<bool> UpdateAdAsync(AdUpdateModel adModel);
        Task<bool> DeleteAdAsync(Guid adId);
    }
}