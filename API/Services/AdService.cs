using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Data;
using API.Domain;
using API.Filters;
using API.Models.Ad;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Services
{
    public class AdService : IAdService
    {
        private readonly DataContext _dataContext;

        public AdService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateAdAsync(string userId, Ad adModel)
        {
            adModel.UserId = userId;
            await _dataContext.AddAsync(adModel);
            
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Ad>> GetAdsAsync()
        {
            return await _dataContext.Ads.ToListAsync();
        }

        public async Task<IEnumerable<Ad>> GetAdsAsync(GetAllAdsFilters filters, PaginationFilters paging)
        {
            
            var collection = _dataContext.Ads as IQueryable<Ad>;

            collection = GetFilers(filters, paging, collection);

            return await collection.ToListAsync();
        }
        
        public async Task<Ad> GetAdByIdAsync(Guid adId)
        {
            return await _dataContext.Ads.Include(x=>x.User).FirstOrDefaultAsync(x =>x.Id == adId);
        }
        
        public async Task<IEnumerable<Ad>> GetAdsByCategory(Guid categoryId, GetAllAdsFilters filters, PaginationFilters paging)
        {
            var collection = _dataContext.Ads as IQueryable<Ad>;

            collection = GetFilers(filters, paging, collection);
            
            return await collection.Where(x=>x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<bool> UpdateAdAsync(Guid adId, AdUpdateModel adModel)
        {
            var ad = await _dataContext.Ads.FirstOrDefaultAsync(x => x.Id == adId);
            
            if (ad == null)
                return false;

            ad.Title = adModel.Title ?? ad.Title;
            
            ad.Author = adModel.Author ?? ad.Author;
            
            ad.BookName = adModel.BookName ?? ad.BookName;

            ad.Price = adModel.Price;
            ad.LastEditedDate = DateTime.UtcNow;
            
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAdAsync(Guid adId)
        {
            var ad = await GetAdByIdAsync(adId);

            if (ad == null)
                return false;
            _dataContext.Ads.Remove(ad);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserOwnsPostAsync(Guid adId, string getUserId)
        {
            var ad = await _dataContext.Ads.AsNoTracking().SingleOrDefaultAsync(x => x.Id == adId);
            
            if (ad == null)
            {
                return false;
            }

            if (ad.UserId != getUserId)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Ad>> GetUserAdsAsync(string username)
        {
            return await _dataContext.Ads.Where(x => x.User.UserName == username).ToListAsync();
        }
        
        
        private  static IQueryable<Ad> GetFilers(GetAllAdsFilters filters, PaginationFilters paging, IQueryable<Ad> collection)
        {
            collection = filters.Condition switch
            {
                Cond.New => collection.Where(x => x.Condition == Condition.New),
                Cond.Used => collection.Where(x => x.Condition == Condition.Used),
                _ => collection
            };

            if (!string.IsNullOrEmpty(filters.CategoryId))
            {
                collection = collection.Where(x => x.CategoryId.ToString() == filters.CategoryId);
            }
            
            if (filters.MaxPrice != 0 && filters.MinPrice != 0)
            {
                collection = collection.Where(x => x.Price <= filters.MaxPrice && x.Price >= filters.MinPrice);
            }

            if (filters.MinPrice != 0)
            {
                collection = collection.Where(x => x.Price >= filters.MinPrice);
            }
            
            if (filters.MaxPrice != 0)
            {
                collection = collection.Where(x => x.Price <= filters.MaxPrice);
            }

            if (!string.IsNullOrWhiteSpace(filters.Bookname))
            {
                filters.Bookname = filters.Bookname.Trim();
                collection = collection.Where(x => x.BookName.Contains(filters.Bookname));
            }
            
            if (!string.IsNullOrWhiteSpace(filters.Author))
            {
                filters.Author = filters.Author.Trim();
                collection = collection.Where(x => x.Title.Contains(filters.Author));
            }
            
            if (!string.IsNullOrWhiteSpace(filters.Title)) 
            {
                filters.Title = filters.Title.Trim();
                collection = collection.Where(x => x.Author.Contains(filters.Title));
            }
            
            return collection.Include(x=>x.User).Skip(paging.PageSize * (paging.PageNumber - 1)).Take(paging.PageSize);
        }

        public enum Cond
        {
            All, New, Used
        }
        
    }
}