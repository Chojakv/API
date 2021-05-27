using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Ad;
using AutoMapper;
using Domain.Domain;
using Domain.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Infrastructure.Services
{
    public class AdService : IAdService
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AdService(DataContext dataContext, IWebHostEnvironment webHostEnvironment, IMapper mapper, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        public async Task<PayloadResult<Ad>> CreateAdAsync(string userId, AdCreationModel adModel)
        {
            var ad = _mapper.Map<AdCreationModel, Ad>(adModel);
            ad.UserId = userId;
            ad.User = await _dataContext.Users.FindAsync(userId);
            ad.Category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == adModel.CategoryId);
            
            await _dataContext.AddAsync(ad);
        
            var success = await _dataContext.SaveChangesAsync() > 0;

            if (!success)
            {
                return new PayloadResult<Ad>
                {
                    Errors = new[] {"Could not save changes."},
                    Success = false
                };
            }
            return new PayloadResult<Ad>
            {
                Payload = ad
            };
        }
        
        public async Task<PagedList<Ad>> GetAdsAsync(GetAllAdsFilters filters, PaginationFilters pagination, string sort)
        {
            var collection = _dataContext.Ads as IQueryable<Ad>;
        
            collection = GetFilers(filters, collection);
        
            collection = SortAds(collection, sort);
        
            return PagedList<Ad>.ToPagedList(collection, pagination.PageNumber, pagination.PageSize);
        }
        
        public async Task<Ad> GetAdByIdAsync(Guid adId)
        {
            return await _dataContext.Ads.Include(x=>x.User).Include(x=>x.Category).Include(x=>x.Images).FirstOrDefaultAsync(x =>x.Id == adId);
        }
        public async Task<PayloadResult<Ad>> UpdateAdAsync(Guid adId, AdUpdateModel adModel)
        {
            var ad = await _dataContext.Ads.FirstOrDefaultAsync(x => x.Id == adId);

            if (ad == null)
                return new PayloadResult<Ad>
                {
                    Errors = new[] {$"Ad with Id '{adId}' does not exists."},
                };
            
            ad.Title = adModel.Title ?? ad.Title;
            ad.Author = adModel.Author ?? ad.Author;
            ad.Content = adModel.Content ?? ad.Content;
            ad.BookName = adModel.BookName ?? ad.BookName;
            ad.Price = adModel.Price;
            ad.LastEditedDate = DateTime.UtcNow;

            ad.User = await _dataContext.Users.FindAsync(ad.UserId);
            
            var updated = await _dataContext.SaveChangesAsync() > 0;

            if (!updated)
            {
                return new PayloadResult<Ad>
                {
                    Errors = new[] {"Could not save changes."},
                    Success = false
                };
            }
            return new PayloadResult<Ad>
            {
                Payload = ad,
                Success = true
            };
        }
        public async Task<BaseRequestResult> DeleteAdAsync(Guid adId)
        {
            var ad = await GetAdByIdAsync(adId);

            if (ad == null)
                return new BaseRequestResult
                {
                    Errors = new[] {$"Ad with Id '{adId}' does not exists."}
                };
            
            _dataContext.Ads.Remove(ad);
            
            var deleted =  await _dataContext.SaveChangesAsync() > 0;

            if (!deleted)
            {
                return new BaseRequestResult
                {
                    Errors = new[] {$"Could not save changes"},
                    Success = false
                };
            }
            return new BaseRequestResult
            {
                Success = true
            };
        }
        public async Task<BaseRequestResult> UserOwnsPostAsync(Guid adId, string getUserId)
        {
            var ad = await _dataContext.Ads.AsNoTracking().SingleOrDefaultAsync(x => x.Id == adId);
            
            if (ad == null)
            {
                return new BaseRequestResult
                {
                    Errors = new[] {$"Ad with Id '{adId}' does not exists."}
                };
            }

            if (ad.UserId != getUserId)
            {
                return new BaseRequestResult
                {
                    Errors = new[] {"You do not own this ad."}
                };
            }

            return new BaseRequestResult
            {
                Success = true
            };
        }
        
        public async Task<IEnumerable<Ad>> GetUserAdsAsync(string username)
        {
            return await _dataContext.Ads.Where(x => x.User.UserName == username).Include(x=>x.User).Include(x=>x.Category).Include(x=>x.Images).ToListAsync();
        }

        public async Task UploadAdImages(Guid adId, AdUploadPhotosModel images)
        {
            var url = _configuration.GetValue<string>("AdImageUrl");
            
            foreach (var photo in images.Images)
            {
                var fileName = photo.FileName;
                var extension = Path.GetExtension(fileName);
                
                var newFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath,"wwwroot", "AdImages", newFileName);

                await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await photo.CopyToAsync(fileStream);

                var photoUrl = $"{url}/{newFileName}";
                
                var adPhoto = new AdPhotoDetailsModel
                {
                    Id = Guid.NewGuid(),
                    AdId = adId,
                    ImageUrl = photoUrl
                };

                var mapped = _mapper.Map<AdImage>(adPhoto);
                
                await _dataContext.AdImages.AddAsync(mapped);
            }
            
            await _dataContext.SaveChangesAsync();
        }

        private static IQueryable<Ad> SortAds(IQueryable<Ad> collection, string sort)
        {
            return sort switch
            {
                "title" => collection.OrderBy(x => x.Title),
                "+price" => collection.OrderBy(x => x.Price),
                "-price" => collection.OrderByDescending(x => x.Price),
                "date" => collection.OrderByDescending(x => x.CreationDate),
                _ => collection
            };
        }
        
        private  static IQueryable<Ad> GetFilers(GetAllAdsFilters filters, IQueryable<Ad> collection)
        {
            collection = filters.Condition switch
            {
                "New" => collection.Where(x => x.Condition == Condition.New),
                "Used" => collection.Where(x => x.Condition == Condition.Used),
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
                collection = collection.Where(x => x.Author.Contains(filters.Author));
            }
            
            if (!string.IsNullOrWhiteSpace(filters.Title)) 
            {
                filters.Title = filters.Title.Trim();
                collection = collection.Where(x => x.Title.Contains(filters.Title));
            }
            
            return collection.Include(x=>x.User).Include(x=>x.Category).Include(x=>x.Images);
        }
        
        
    }
}