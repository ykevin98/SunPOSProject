#region Usings

using AutoMapper;
using SunPOSData.Models;
using SunPOSServices.ViewModels;

#endregion

namespace SunPOSServices.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<RestaurantViewModel, Restaurant>().ReverseMap();
            CreateMap<MenuViewModel, Menu>().ReverseMap();
            CreateMap<ResultViewModel, Result>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<CartViewModel, Cart>().ReverseMap();
        }
    }
}
