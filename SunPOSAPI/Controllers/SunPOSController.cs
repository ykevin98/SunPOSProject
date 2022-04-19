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
        #endregion
    }

    #endregion
}
