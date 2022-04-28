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

        public static ResultViewModel ToViewModel(this Result model, IMapper mapper)
        {
            return mapper.Map<ResultViewModel>(model);
        }

        public static UserViewModel ToViewModel(this User model, IMapper mapper)
        {
            return mapper.Map<UserViewModel>(model);
        }

        public static IEnumerable<CartViewModel> ToViewModels(this IEnumerable<Cart> models, IMapper mapper)
        {
            return mapper.Map<IEnumerable<CartViewModel>>(models);
        }

        #endregion

        #region ToDataModels

        public static Menu ToModel(this MenuViewModel viewModel, IMapper mapper)
        {
            return mapper.Map<Menu>(viewModel);
        }

        public static User ToModel(this UserViewModel viewModel, IMapper mapper)
        {
            return mapper.Map<User>(viewModel);
        }

        #endregion
    }
}
