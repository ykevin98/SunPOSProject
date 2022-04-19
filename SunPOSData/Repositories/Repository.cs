#region usings

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using State = Microsoft.EntityFrameworkCore.EntityState;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
//using System.Data.SqlClient;

#endregion

namespace SunPOSData.Repositories
{
    /// <summary>
    /// Generic Repository that allows interactions with database through Entity FrameWorkCore.
    /// </summary>
    /// <typeparam name="T">Name of an entity, a concrete class</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Variables

        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly WindowsIdentity _identity;

        #endregion

        #region Constructor

        public Repository(DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _dbSet = context.Set<T>();
            //_identity = (WindowsIdentity)httpContextAccessor.HttpContext.User.Identity;
        }

        #endregion

        #region Public Functions

        #region Query Data

        public int Count()
        {
            int count = 0;
            count = _dbSet.AsNoTracking().Count();

            return count;
        }

        public int Count(Expression<Func<T, bool>> predicates)
        {
            int count = 0;
            count = _dbSet.AsNoTracking().Count(predicates);

            return count;
        }

        public IEnumerable<T> All()
        {
            IEnumerable<T> result = null;
            result = AllAsIQueryable().ToList();

            return result;
        }

        public IQueryable<T> AllAsIQueryable()
        {
            IQueryable<T> result = null;
            // result = _dbSet.AsQueryable();
            result = _context.Set<T>();

            return result;
        }

        public IEnumerable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            IEnumerable<T> result = null;
            result = AllIncludeAsIQueryable(includeProperties).ToList();

            return result;
        }

        public IQueryable<T> AllIncludeAsIQueryable(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> result = null;
            IQueryable<T> queryable = AllAsIQueryable();
            result = Includes(queryable, includeProperties);

            return result;
        }

        public T FindByKey(params object[] keyValues)
        {
            T result = null;
            if (keyValues == null || !keyValues.Any())
                throw new ArgumentException("Invalid argument", "keyValues");

            result = _dbSet.Find(keyValues);
            return result;
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> result = null;
            
            result = FindByIQueryable(predicate).ToList();

            return result;
        }

        public IQueryable<T> FindByIQueryable(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> result = null;
            result = _dbSet.Where(predicate);

            return result;
        }

        public IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IEnumerable<T> result = null;
            result = FindByIncludeAsIQueryable(predicate, includeProperties).ToList();
            return result;
        }

        public IQueryable<T> FindByIncludeAsIQueryable(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> result = null;
            IQueryable<T> queryable = FindByIQueryable(predicate);
            result = Includes(queryable, includeProperties);

            return result;
        }

        #endregion

        #region Insert Data

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void AddRange(params T[] entities)
        {
            _dbSet.AddRange(entities);
        }

        #endregion

        #region Update Data

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void UpdateRange(params T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        #endregion

        #region DeleteData

        public void RemoveByKey(params object[] keyValues)
        {
            var entity = FindByKey(keyValues);

            Remove(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void RemoveRange(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        #endregion

        #region Raw Sql Commands

        public IEnumerable<T> QueryWithRawSqlOrStoreProcedure(string sql, params SqlParameter[] parameters)
        {
            IEnumerable<T> result = null;
            result = QueryWithRawSqlOrStoreProcedureAsIQueryable(sql, parameters).ToList();

            return result;
        }

        public IQueryable<T> QueryWithRawSqlOrStoreProcedureAsIQueryable(string sql, params SqlParameter[] parameters)
        {
            IQueryable<T> result = null;
            result = _dbSet.FromSqlRaw(sql, parameters);

            return result;
        }

        public int NonQueryRawSqlOrStoreProcedure(string sql, params SqlParameter[] parameters)
        {
            int count = 0;
            count = _context.Database.ExecuteSqlRaw(sql, parameters);

            return count;
        }

        #endregion

        #region Change Trackers

        public void TrackGraph(T graph, Action<EntityEntryGraphNode> callbackAction)
        {
            _context.ChangeTracker.TrackGraph(graph, callbackAction);
        }

        public void SetRootEntityState(T entity, State state)
        {
            _context.Entry(entity).State = state;
        }

        public State GetEntityState(T entity)
        {
            return _context.Entry(entity).State;
        }

        #endregion

        #region Others

        public DbSet<T> RawDbSet()
        {
            return _dbSet;
        }

        public void ReLoad(T entry)
        {
            _context.Entry(entry).Reload();
        }

        public bool HasChanges(T entity)
        {
            return GetEntityState(entity) == State.Modified;
        }

        public bool IsNew(T entity)
        {
            var state = GetEntityState(entity);
            return state == State.Added || state == State.Detached;
        }

        #endregion

        #endregion

        #region Private Functions        

        /// <summary>
        /// This is known as eagle loading.
        /// Note that:
        /// 1) You cannot apply linq directly on related data that is eagled loading
        /// 2) Use explicit loading if you want to do liqn method on the related data.    
        /// </summary>
        private IQueryable<T> Includes(IQueryable<T> queryable, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> result = null;
            result = includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
            return result;
        }

        #endregion
    }
}
