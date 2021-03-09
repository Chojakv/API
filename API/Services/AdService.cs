using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Data;
using API.Domain;
using API.Models.Ad;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

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
        
        public async Task<IEnumerable<Ad>> GetAdsAsync(string bookname, string title, string author)
        {
            if (string.IsNullOrWhiteSpace(bookname) && string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(author))
            {
                return await _dataContext.Ads.ToListAsync();
            }

            var collection = _dataContext.Ads as IQueryable<Ad>;

            if (!string.IsNullOrWhiteSpace(bookname))
            {
                bookname = bookname.Trim();
                collection = collection.Where(x => x.BookName.Contains(bookname));
            }
        
            if (!string.IsNullOrWhiteSpace(title))
            {
                title = title.Trim();
                collection = collection.Where(x => x.Title.Contains(title));
            }

            if (string.IsNullOrWhiteSpace(author)) return await collection.ToListAsync();
            {
                author = author.Trim();
                collection = collection.Where(x => x.Author.Contains(author));
            }

            return await collection.ToListAsync();
        }

        public async Task<Ad> GetAdByIdAsync(Guid adId)
        {
            return await _dataContext.Ads.Include(x=>x.User).FirstOrDefaultAsync(x =>x.Id == adId);
        }
        public async Task<IEnumerable<Ad>> GetAdsByCategory(Guid categoryId)
        {
            return await _dataContext.Ads.Where(x=>x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<bool> UpdateAdAsync(Guid adId, AdUpdateModel adModel)
        {
            var ad = await _dataContext.Ads.FirstOrDefaultAsync(x => x.Id == adId);
            
            if (ad == null)
                return false;

            if (adModel.Title == null)
            {
                ad.Title = ad.Title;
            }
            else{ ad.Title = adModel.Title;}
            
            if (adModel.Author == null)
            {
                ad.Author = ad.Author;
            }
            else{ ad.Author = adModel.Author;}
            
            if (adModel.BookName == null)
            {
                ad.BookName = ad.BookName;
            }
            else{ ad.BookName = adModel.BookName;}

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

        public async Task<IEnumerable<Ad>> GetUserAds(string username)
        {
            return await _dataContext.Ads.Where(x => x.User.UserName == username).ToListAsync();
        }
    }
}