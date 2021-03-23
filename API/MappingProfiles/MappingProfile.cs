using API.Contracts.Requests.Queries;
using API.Domain;
using API.Filters;
using API.Models.Ad;
using API.Models.AppUser;
using API.Models.Category;
using AutoMapper;

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
            CreateMap<AdCreationModel, Ad>().ReverseMap();
            CreateMap<AdDetailsModel, Ad>().ReverseMap();
            CreateMap<AdUpdateModel, Ad>().ReverseMap();
            CreateMap<AdUpdateModel, AdDetailsModel>().ReverseMap();
            CreateMap<AdUpdateModel, AppUser>().ReverseMap();

            CreateMap<AppUser, AppUserDetailsModel>();
            CreateMap<AppUser, AppUserUpdateModel>().ReverseMap();


            CreateMap<GetAllAdsQueries, GetAllAdsFilters>();
            CreateMap<PaginationQuery, PaginationFilters>();

        }
    }
}