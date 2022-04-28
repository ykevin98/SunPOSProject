#region Usings

using SunPOSServices.Services;
using SunPOSServices.ViewModels;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace SunPOSAPI.Controllers
{
    #region Controller

    [ApiController]
    [Route("api/[controller]")]
    public class SunPOSController : ControllerBase
    {
        #region Private Members

        private readonly ILogger<SunPOSController> _logger;
        private readonly ISunPOSService _service;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        public SunPOSController(ILogger<SunPOSController> logger, ISunPOSService service, IConfiguration configuration)
        {
            _logger = logger;
            _service = service;
            _configuration = configuration;
        }

        #endregion

        #region APIActions

        [HttpGet]
        [Route("GetRestaurant")]
        [ProducesResponseType(typeof(RestaurantViewModel), StatusCodes.Status200OK)]
        public IActionResult GetRestaurant (string restaurantName)
        {
            var restaurant = _service.GetRestaurant(restaurantName);

            return Ok(restaurant);
        }

        [HttpGet]
        [Route("GetCategories")]
        [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status200OK)]
        public IActionResult GetCategories (Guid restaurantId)
        {
            var categories = _service.GetCategories(restaurantId);

            return Ok(categories);
        }

        [HttpGet]
        [Route("GetMenu")]
        [ProducesResponseType(typeof(MenuViewModel), StatusCodes.Status200OK)]
        public IActionResult GetMenus (string restaurantName, Guid categoryId)
        {
            var menuItems = _service.GetMenuItems(restaurantName, categoryId);

            return Ok(menuItems);
        }

        [HttpGet]
        [Route("GetUser")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        public IActionResult GetUser (string userName)
        {
            var user = _service.GetUser(userName);

            return Ok(user);
        }

        [HttpGet]
        [Route("GetShoppingCart")]
        [ProducesResponseType(typeof(CartViewModel), StatusCodes.Status200OK)]
        public IActionResult GetShoppingCart (Guid userId, Guid restaurantId)
        {
            var shoppingCart = _service.GetShoppingCart(userId, restaurantId);

            return Ok(shoppingCart);
        }

        [HttpPost]
        [Route("AddToCart")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status200OK)]
        public IActionResult AddToCart ([FromBody] MenuViewModel menuItem, Guid userId, Guid restaurantId)
        {
            var result = _service.AddToCart(menuItem, userId, restaurantId);

            return Ok(result);
        }

        [HttpPost]
        [Route("AddUser")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status200OK)]
        public IActionResult AddUser ([FromBody] UserViewModel user)
        {
            var result = _service.AddUser(user);

            return Ok(result);
        }

        #endregion
    }

    #endregion
}
