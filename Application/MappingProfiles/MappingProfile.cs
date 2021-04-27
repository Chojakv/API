using Application.Models.Ad;
using Application.Models.AppUser;
using Application.Models.Category;
using Application.Models.Messages;
using Application.Models.Photo;
using Application.Models.Queries;
using AutoMapper;
using Domain.Domain;
using Domain.Filters;
using Microsoft.AspNetCore.Http;

namespace Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryCreationModel, Category>();
            CreateMap<CategoryDetailsModel, Category>().ReverseMap();
            
            CreateMap<AdCreationModel, Ad>();
            CreateMap<AdCreationModel, AdDetailsModel>();
            CreateMap<AdDetailsModel, Ad>();
            CreateMap<Ad, AdDetailsModel>()
                .ForMember(dest => dest.CreatedBy, act => act.MapFrom(src => src.User.UserName));
            
            CreateMap<AdUpdateModel, Ad>();
            CreateMap<AdUpdateModel, AdDetailsModel>();
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