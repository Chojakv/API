using API.Domain;
using API.Models.Ad;
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

            CreateMap<AdCreationModel, Ad>().ReverseMap();
            CreateMap<AdDetailsModel, Ad>().ReverseMap();
            CreateMap<AdUpdateModel, Ad>().ReverseMap();
            CreateMap<AdUpdateModel, AdDetailsModel>().ReverseMap();
        }
    }
}