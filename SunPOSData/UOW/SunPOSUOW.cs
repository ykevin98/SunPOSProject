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
        IEnumerable<Menu> GetMenuItems(string restaurantName, Guid categoryId);
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

        #endregion

        #region Constructors

        public SunPOSUOW (SunPOSContext context, IHttpContextAccessor httpContextAccessor)
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
        
        public IEnumerable<Menu> GetMenuItems(string restaurantName, Guid categoryId)
        {
            var paramaters = new SqlParameter[]
            {
                new SqlParameter { ParameterName = "@RestaurantName", SqlDbType = System.Data.SqlDbType.VarChar, Value = restaurantName}
            };

            _menuRepository = new Repository<Menu>(_context, _httpContextAccessor);

            var results = _menuRepository.QueryWithRawSqlOrStoreProcedure(Constants.StoredProcedures.GetMenu, paramaters)
                    .Where(x => x.CategoryID == categoryId && x.Item != "None" && x.Item != "none" && x.Item != "NONE").ToList();

            return results;
        }

        #endregion
    }

    #endregion
}
