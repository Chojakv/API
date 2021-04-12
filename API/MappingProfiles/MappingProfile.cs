using API.Contracts.Requests.Queries;
using API.Domain;
using API.Filters;
using API.Models;
using API.Models.Ad;
using API.Models.AppUser;
using API.Models.Category;
using API.Models.Messages;
using API.Models.Photo;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace API.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryCreationModel, Category>().ReverseMap();
            CreateMap<CategoryDetailsModel, Category>().ReverseMap();
            CreateMap<CategoryCreationModel, CategoryDetailsModel>().ReverseMap();

            CreateMap<AdCreationModel, AdDetailsModel>().ReverseMap();
            CreateMap<AdCreationModel, Ad>();
                
            CreateMap<AdDetailsModel, Ad> ();
            CreateMap<Ad, AdDetailsModel>()
                .ForMember(dest => dest.CreatedBy, act => act.MapFrom(src => src.User.UserName));
            
            CreateMap<AdUpdateModel, Ad>().ReverseMap();
            CreateMap<AdUpdateModel, AdDetailsModel>().ReverseMap();
            CreateMap<AdUpdateModel, AppUser>().ReverseMap();

            CreateMap<AppUser, AppUserDetailsModel>();
            CreateMap<AppUser, AppUserUpdateModel>().ReverseMap();
            
            CreateMap<GetAllAdsQueries, GetAllAdsFilters>();
            CreateMap<PaginationQuery, PaginationFilters>();

            CreateMap<IFormFile, PhotoDetailsModel>().ReverseMap();
            CreateMap<IFormFile, Photo>().ReverseMap();
            CreateMap<Photo, PhotoDetailsModel>().ReverseMap();
            
            CreateMap<SendMessageModel, DetailsSentMessageModel>().ReverseMap();
            CreateMap<SendMessageModel, Message>().ReverseMap();
            CreateMap<Message, DetailsSentMessageModel>().ReverseMap();
            CreateMap<Message, DetailsReceivedMessageModel>().ReverseMap();



        }
    }
}