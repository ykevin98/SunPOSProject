#region Usings

//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using SunPOSData.Repositories;
using SunPOSData.Models;
using SunPOSData.Context;
using SunPOSData.Common;
using Microsoft.AspNetCore.Http;

#endregion

namespace SunPOSData.UOW
{
    #region Public Interface

    public interface ISunPOSUOW
    {
        IRepository<Category> CategoryRepository { get; }
        IRepository<Restaurant> RestaurantRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Cart> CartRepository { get; }
        IEnumerable<Menu> GetMenuItems(string restaurantName, Guid categoryId);
        Result AddToCart(Menu menuItem, User user, Guid restaurantId);
        Result AddUser(User user);
    }

    #endregion

    #region Interface Implementation

    public class SunPOSUOW : ISunPOSUOW
    {
        #region Private Members

        private readonly SunPOSContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IRepository<Category> _categoryRepository;
        private IRepository<Restaurant> _restaurantRepository;
        private IRepository<Menu> _menuRepository;
        private IRepository<User> _userRepository;
        private IRepository<Cart> _cartRepository;

        #endregion

        #region Constructors

        public SunPOSUOW(SunPOSContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Public Methods

        public IRepository<Category> CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new Repository<Category>(_context, _httpContextAccessor);
            }
        }

        public IRepository<Restaurant> RestaurantRepository
        {
            get
            {
                return _restaurantRepository ??= new Repository<Restaurant>(_context, _httpContextAccessor);
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                return _userRepository ??= new Repository<User>(_context, _httpContextAccessor);
            }
        }

        public IRepository<Cart> CartRepository
        {
            get
            {
                return _cartRepository ??= new Repository<Cart>(_context, _httpContextAccessor);
            }
        }

        public IEnumerable<Menu> GetMenuItems(string restaurantName, Guid categoryId)
        {
            var paramaters = new SqlParameter[]
            {
                new SqlParameter { ParameterName = "@RestaurantName", SqlDbType = System.Data.SqlDbType.VarChar, Value = restaurantName}
            };

            _menuRepository = new Repository<Menu>(_context, _httpContextAccessor);

            var results = _menuRepository.QueryWithRawSqlOrStoreProcedure(Constants.StoredProcedures.GetMenu, paramaters)
                    .Where(x => x.CategoryID == categoryId && !x.Item.Contains("None") && !x.Item.Contains("none") && !x.Item.Contains("NONE"))
                    .OrderBy(x => x.Item).ToList();

            foreach (var item in results)
            {
                if (item.Description == "none" || item.Description == "None")
                {
                    item.Description = string.Empty;
                }
                /*
                if (item.Item.Contains("BF"))
                {
                    item.Item = item.Item.Replace("BF", "Beef");
                }
                if (item.Item.Contains("CH"))
                {
                    item.Item = item.Item.Replace("CH", "Chicken");
                }
                if (item.Item.Contains("SH"))
                {
                    item.Item = item.Item.Replace("SH", "Shrimp");
                }
                if (item.Item.Contains("Sh"))
                {
                    item.Item = item.Item.Replace("Sh", "Shrimp");
                }
                if (item.Item.Contains("CK"))
                {
                    item.Item = item.Item.Replace("CK", "Chicken");
                }
                */
            }
            return results;
        }

        public Result AddToCart(Menu menuItem, User user, Guid restaurantId)
        {
            using (_context)
            {
                try
                {
                    Cart cart = new Cart
                    {
                        MenuId = menuItem.MenuId,
                        Item = menuItem.Item,
                        Item2 = menuItem.Item2,
                        Price = menuItem.Price,
                        LunchPrice = menuItem.LunchPrice,
                        DinnerPrice = menuItem.DinnerPrice,
                        Description = menuItem.Description,
                        UserId = user.UserID,
                        RestaurantID = restaurantId
                    };

                    _context.Add(cart);

                    _context.SaveChanges();

                    Result result = new Result
                    {
                        Message = "Item added to Cart successfully",
                        IsSuccessful = true
                    };

                    return result;
                }
                catch(Exception exception)
                {
                    Result result = new Result
                    {
                        Message = exception.Message,
                        IsSuccessful = false
                    };

                    return result;
                }
            }
        }

        public Result AddUser(User user)
        {
            using (_context)
            {
                try
                {
                    _context.Add(user);

                    _context.SaveChanges();

                    Result result = new Result
                    {
                        Message = "User added successfully",
                        IsSuccessful = true
                    };

                    return result;
                }
                catch (Exception exception)
                {
                    Result result = new Result
                    {
                        Message = exception.Message,
                        IsSuccessful = false
                    };

                    return result;
                }
            }
        }

        #endregion
    }

    #endregion
}
