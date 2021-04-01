using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;
using API.Filters;
using API.Models.Ad;


namespace API.Services
{
    public interface IAdService
    {
        Task<PayloadResult<Ad>> CreateAdAsync(string userId, AdCreationModel adModel);
        Task<IEnumerable<Ad>> GetAdsAsync(GetAllAdsFilters filters, PaginationFilters paging, string sort);
        Task<Ad> GetAdByIdAsync(Guid adId);
        Task<IEnumerable<Ad>> GetAdsByCategory(Guid categoryId, GetAllAdsFilters filters, PaginationFilters paging);
        Task<PayloadResult<Ad>> UpdateAdAsync(Guid adId, AdUpdateModel adModel);
        Task<BaseRequestResult> DeleteAdAsync(Guid adId);
        Task<BaseRequestResult> UserOwnsPostAsync(Guid adId, string getUserId); 
        Task<IEnumerable<Ad>> GetUserAdsAsync(string username);

    }
}