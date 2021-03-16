using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;
using API.Models.Ad;

namespace API.Services
{
    public interface IAdService
    {
        Task<bool> CreateAdAsync(string userId, Ad adModel);
        
        Task<IEnumerable<Ad>> GetAdsAsync();
        Task<IEnumerable<Ad>> GetAdsAsync(GetAllAdsFilters filters);
        Task<Ad> GetAdByIdAsync(Guid adId);
        Task<IEnumerable<Ad>> GetAdsByCategory(Guid categoryId, GetAllAdsFilters filters);
        Task<bool> UpdateAdAsync(Guid adId, AdUpdateModel adModel);
        Task<bool> DeleteAdAsync(Guid adId);
        Task<bool> UserOwnsPostAsync(Guid adId, string getUserId); 
        Task<IEnumerable<Ad>> GetUserAdsAsync(string username);

    }
}