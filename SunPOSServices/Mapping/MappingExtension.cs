#region Usings

using AutoMapper;
using SunPOSData.Models;
using SunPOSServices.ViewModels;
using System.Collections.Generic;

#endregion

namespace SunPOSServices.Mapping
{
    public static class MappingExtension
    {
        #region ToViewModels

        public static CategoryViewModel ToViewModel(this Category model, IMapper mapper)
        {
            return mapper.Map<CategoryViewModel>(model);
        }

        public static IEnumerable<CategoryViewModel> ToViewModels(this IEnumerable<Category> models, IMapper mapper)
        {
            return mapper.Map<IEnumerable<CategoryViewModel>>(models);
        }

        public static RestaurantViewModel ToViewModel(this Restaurant model, IMapper mapper)
        {
            return mapper.Map<RestaurantViewModel>(model);
        }

        public static IEnumerable<RestaurantViewModel> ToViewModels(this IEnumerable<Restaurant> models, IMapper mapper)
        {
            return mapper.Map<IEnumerable<RestaurantViewModel>>(models);
        }

        public static IEnumerable<MenuViewModel> ToViewModels(this IEnumerable<Menu> models, IMapper mapper)
        {
            return mapper.Map<IEnumerable<MenuViewModel>>(models);
        }

        #endregion
    }
}
