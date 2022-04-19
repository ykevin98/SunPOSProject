#region Usings

using SunPOSServices.ViewModels;
using SunPOSServices.Mapping;
using SunPOSData.UOW;
using AutoMapper;

#endregion

namespace SunPOSServices.Services
{
    #region Interface

    public interface ISunPOSService
    {
        public RestaurantViewModel GetRestaurant(string restaurantName);
        public IEnumerable<CategoryViewModel> GetCategories(Guid restaurantID);
        public IEnumerable<MenuViewModel> GetMenuItems(string restaurantName, Guid categoryId);
    }

    #endregion

    #region Interface Implementation

    public class SunPOSService : ISunPOSService
    {
        #region Private Members

        private readonly IMapper _mapper;
        private readonly ISunPOSUOW _sunPOSUOW;

        #endregion

        #region Constructor

        public SunPOSService(ISunPOSUOW sunPOSUOW, IMapper mapper)
        {
            _mapper = mapper;
            _sunPOSUOW = sunPOSUOW;
        }

        #endregion

        #region Public Methods

        public RestaurantViewModel GetRestaurant(string restaurantName)
        {
            var result = _sunPOSUOW.RestaurantRepository.FindBy(x => x.RestaurantName == restaurantName).FirstOrDefault();

            return result.ToViewModel(_mapper);
        }

        public IEnumerable<CategoryViewModel> GetCategories(Guid restaurantID)
        {
            var results = _sunPOSUOW.CategoryRepository.FindBy(x => x.RestaurantID == restaurantID).OrderBy(x => x.CategoryName).ToList();

            return results.ToViewModels(_mapper);
        }

        public IEnumerable<MenuViewModel> GetMenuItems(string restaurantName, Guid categoryId)
        {
            var results = _sunPOSUOW.GetMenuItems(restaurantName, categoryId);

            return results.ToViewModels(_mapper);
        }

        #endregion
    }

    #endregion
}
