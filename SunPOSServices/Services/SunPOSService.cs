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
        public UserViewModel GetUser(string userName);
        public IEnumerable<CartViewModel> GetShoppingCart (Guid userId, Guid restaurantId);
        public ResultViewModel AddToCart(MenuViewModel menuItem, Guid userId, Guid restaurantId);
        public ResultViewModel AddUser(UserViewModel user);
        public ResultViewModel Checkout(IEnumerable<CartViewModel> cartItems);
        public ResultViewModel RemoveCartItem(Guid itemId);
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

        public UserViewModel GetUser(string userName)
        {
            var result = _sunPOSUOW.UserRepository.FindBy(x => x.UserName == userName).First();

            return result.ToViewModel(_mapper);
        }

        public IEnumerable<CartViewModel> GetShoppingCart(Guid userId, Guid restaurantId)
        {
            var results = _sunPOSUOW.CartRepository.FindBy(x => x.UserId == userId && x.RestaurantID == restaurantId)
                .OrderBy(x => x.Item).ToList();

            return results.ToViewModels(_mapper);
        }

        public ResultViewModel AddToCart(MenuViewModel menuItem, Guid userId, Guid restaurantId)
        {
            var result = _sunPOSUOW.AddToCart(menuItem.ToModel(_mapper), userId, restaurantId);

            return result.ToViewModel(_mapper);
        }

        public ResultViewModel AddUser(UserViewModel user)
        {
            var result = _sunPOSUOW.AddUser(user.ToModel(_mapper));

            return result.ToViewModel(_mapper);
        }

        public ResultViewModel Checkout(IEnumerable<CartViewModel> cartItems)
        {
            var result = _sunPOSUOW.Checkout(cartItems.ToModels(_mapper));

            return result.ToViewModel(_mapper);
        }

        public ResultViewModel RemoveCartItem(Guid itemId)
        {
            var result = _sunPOSUOW.RemoveCartItem(itemId);

            return result.ToViewModel(_mapper);
        }

        #endregion
    }

    #endregion
}
