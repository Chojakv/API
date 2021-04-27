using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Ad;
using Domain.Domain;
using Domain.Filters;

namespace Application.Interfaces
{
    public interface IAdService
    {
        Task<PayloadResult<Ad>> CreateAdAsync(string userId, AdCreationModel adModel);
        Task<IEnumerable<Ad>> GetAdsAsync(GetAllAdsFilters filters, PaginationFilters paging, string sort);
        Task<Ad> GetAdByIdAsync(Guid adId);
        Task<PayloadResult<Ad>> UpdateAdAsync(Guid adId, AdUpdateModel adModel);
        Task<BaseRequestResult> DeleteAdAsync(Guid adId);
        Task<BaseRequestResult> UserOwnsPostAsync(Guid adId, string getUserId); 
        Task<IEnumerable<Ad>> GetUserAdsAsync(string username);
    }
}