#region usings

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

#endregion

namespace SunPOSData.Repositories
{
    /// <summary>
    /// Provide an easy way to interact with database using EntityFrameWork (EF)
    /// This provides you the ability to select/insert/update/delete/etc. single of multiple objects of type T, whose properties being mapped to a database table's columns
    /// </summary>
    /// <typeparam name="T">The object type or model that is mapped to a database and is registered with EF</typeparam>
    public interface IRepository<T> where T : class
    {

        #region Query Data

        /// <summary>
        /// Gets total number of records from the database
        /// </summary>
        int Count();
        /// <summary>
        /// Gets total number of records from the database that satisfy the input predicate
        /// </summary>
        /// <param name="predicates"></param>
        int Count(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Gets all records of the table from the database as T objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> All();
        /// <summary>
        /// Gets the linq query that gets all records of the table from the database as T objects
        /// </summary>
        /// <returns></returns>
        IQueryable<T> AllAsIQueryable();
        /// <summary>
        /// Gets all records of the table from the database as T objects which include the specified one-level-deep sub objects/properties
        /// Note: this can drill down to only 1 level deep. If object A contains object B1, and B2, and B1 contains object C. The include properties can only be B1 and/or B2
        /// </summary>
        /// <param name="includeProperties">The list of properties to be included</param>
        /// <returns></returns>
        IEnumerable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties);
        /// <summary>
        /// Gets the linq query that gets all records of the table from the database as T objects which include the specified one-level-deep sub objects/properties
        /// Note: this can drill down to only 1 level deep. If object A contains object B1, and B2, and B1 contains object C. The include properties can only be B1 and/or B2
        /// </summary>
        /// <param name="includeProperties">The list of properties to be included</param>
        /// <returns></returns>
        IQueryable<T> AllIncludeAsIQueryable(params Expression<Func<T, object>>[] includeProperties);
        T FindByKey(params object[] keyValues);
        /// <summary>
        /// Gets the records that sastify the input predicate
        /// </summary>
        /// <param name="id">The key of the table that the T type maps to</param>
        /// <param name="idFieldName">The name of the id field</param>
        ///         /// <summary>
        /// Get all records from the database table that satisfy the input predicate as T objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Gets the linq query that gets all records as T objects from the database table that satisfy the input predicate
        /// </summary>
        /// <returns></returns>
        IQueryable<T> FindByIQueryable(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Gets the records that has key values match with the provided key values
        /// </summary>
        /// <param name="keyValues">The key values of the table that the T type maps to</param>
        /// <returns>The first row that matches, as a T object</returns>
        /// <summary>
        /// Gets all records of the table from the database that satify the input predicate as T objects which include the specified one-level-deep sub-objects
        /// Note: this can drill down to only 1 level deep. If object A contains object B1, and B2, and B1 contains object C. The include properties can only be B1 and/or B2
        /// </summary>
        /// <param name="predicate">The specified condition</param>
        /// <param name="includeProperties">The list of sub objects/properties to be included</param>
        IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        /// <summary>
        /// Gets the linq query that gets all records of the table from the database that satify the input predicate as T objects which include the specified one-level-deep sub-objects
        /// Note: this can drill down to only 1 level deep. If object A contains object B1, and B2, and B1 contains object C. The include properties can only be B1 and/or B2
        /// </summary>
        /// <param name="predicate">The specified condition</param>
        /// <param name="includeProperties">The list of sub objects/properties to be included</param>
        IQueryable<T> FindByIncludeAsIQueryable(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        #endregion

        #region Insert

        /// <summary>
        /// Keep track of an item of type T, in order for it to be inserted into database later
        /// </summary>
        /// <param name="entity">The entity to be inserted later</param>
        void Add(T entity);
        /// <summary>
        /// Keep track of a list of items of type T, in order for them to be BATCH inserted into database later
        /// </summary>
        /// <param name="entities">The list of the entities to be inserted</param>
        void AddRange(IEnumerable<T> entities);
        /// <summary>
        /// Keep track of a list of items of type T, in order for them to be BATCH inserted into database later
        /// </summary>
        /// <param name="entites">The list of the entities to be inserted</param>
        void AddRange(params T[] entites);

        #endregion

        #region Update

        /// <summary>
        /// Keep track of an item of type T, in order for it to be updated into database later
        /// </summary>
        /// <param name="entity">The entity to be updated</param>
        void Update(T entity);
        /// <summary>
        /// Keep track of a list of items of type T, in order for them to be updated in database later
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(IEnumerable<T> entities);
        /// <summary>
        /// Keep track of a list of items of type T to a place holder, in order for them to be updated in database later
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(params T[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Keep track of the id of an item of type T, in order for it to be removed from database later
        /// </summary>
        /// <param name="keyValues">The key value of T that needs to be removed from the database</param>
        void RemoveByKey(params object[] keyValues);
        /// <summary>
        /// Keep track of the id of an item of type T, in oder for it to be removed from database later
        /// DO NOT USE this method if you don't know the name of the id field
        /// </summary>
        /// <param name="idFieldName">The name of the id field</param>
        /// <param name="id">The id of the item that will be removed from the database</param>
        void Remove(T entity);
        /// <summary>
        /// Keep track of a list of the entities of type T that is to be removed from the database
        /// </summary>
        /// <param name="entities">The list of the entities that is to be removed</param>
        void RemoveRange(IEnumerable<T> entities);
        /// <summary>
        /// Keep track of a list of the entities of type T that is to be removed from the database
        /// </summary>
        /// <param name="entities">The list of the entities that is to be removed</param>
        void RemoveRange(params T[] entities);

        #endregion

        #region Raw Sql Commands

        /// <summary>
        /// Use this method TO QUERY with a raw sql or exec a stored procedure that returns a result set 
        /// </summary>
        /// <param name="sql">The query sql statement or query stored procedure</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <returns>IEnumerable of T</returns>
        /// <Note>
        /// 1) Sql cound be a query of execute stored procedure
        /// 2) These allow parameters
        /// 3) Must return full entity types (EF 1.1)
        /// 4) Result set column names = mapped names
        /// 5) Query must be flat (cannot include related data)
        /// </Note>
        /// <example1>
        /// var name = "Emma"
        /// var authors = AuthorRepo.QueryWithRawSqlOrStoreProcedures("EXEC FilterAuthorByName {0}", name)
        ///                       
        /// **********************Where the stored procedure looks like******************
        ///     @"CREATE PROCEDURE FilterAuthorByName
        ///         @namepart varchar(50)
        ///       AS
        ///       select * from authors where name like '%' + @namepart + '%'"
        /// *****************************************************************************
        /// </example1>
        /// <example2>
        /// AuthorRepo.QueryWithRawSqlOrStoreProcedures("SELECT * FROM author")
        /// </example2>
        IEnumerable<T> QueryWithRawSqlOrStoreProcedure(string sql, params SqlParameter[] parameters);

        /// <summary>
        /// Use this method TO QUERY with a raw sql or exec a stored procedure that returns a result set 
        /// </summary>
        /// <param name="sql">The query sql statement or query stored procedure</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <returns>IQueryable of T which can be further combine witht linq to sort/filter/project/ect the result set</returns>
        /// <Note>
        /// 1) Sql cound be a query of execute stored procedure
        /// 2) These allow parameters
        /// 3) Must return full entity types (EF 1.1)
        /// 4) Result set column names = mapped names
        /// 5) Query must be flat (cannot include related data)
        /// 6) If exec stored procedure that return a result set, the result will be returned into memory first before any sorting/filtering/projecting can applied on it
        /// 7) If the sql is a query, sorting/filtering will happen right from the database before the result set is retunred, which results in a much better performance
        /// </Note>
        /// <example1>
        /// var name = "Emma"
        /// var authors = _context.AuthorRepo
        ///                       .QueryWithRawSqlOrStoreProceduresAsIQueryable("EXEC FilterAuthorByName {0}", name)
        ///                       .OrderByDescending(author => author.Name).ToList();
        ///                       
        ///                       
        /// **********************Where the stored procedure looks like******************
        ///     @"CREATE PROCEDURE FilterAuthorByName
        ///         @namepart varchar(50)
        ///       AS
        ///       select * from authors where name like '%' + @namepart + '%'"
        /// *****************************************************************************
        /// </example1>
        /// <example2>
        /// AuthorRepo.QueryWithRawSqlOrStoreProceduresAsIQueryable("SELECT * FROM author")
        /// </example2>
        IQueryable<T> QueryWithRawSqlOrStoreProcedureAsIQueryable(string sql, params SqlParameter[] parameters);

        /// <summary>
        /// Use this method execute a raw non query sql or exec a non query stored procedure
        /// </summary>
        /// <param name="sql">A non query sql, or a statement to exec a non query stored procedure</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <returns>Number of affected rows</returns>
        /// <FurtherReference>
        /// http://simon-hughes.blogspot.com/search?updated-min=2015-01-01T00:00:00Z&updated-max=2016-01-01T00:00:00Z&max-results=1
        /// </FurtherReference>
        /// <example1>
        /// AuthorRepo.QueryWithRawSqlOrStoreProceduresAsIQueryable("UPDATE authors SET name = REPLACE(name, 'Emma', 'EMMA')")
        ///           .OrderByDescending(author => author.Name).ToList();
        /// </example1>
        /// <example2>
        /// var procResult = new SqlParameter
        /// {
        ///     ParameterName = "@proResult",
        ///     SqlDbType = SqlDbType.VarChar,
        ///     Direction = ParameterDirection.Ouput,
        ///     Size = 50;
        /// }
        /// 
        /// AuthorRepo.QueryWithRawSqlOrStoreProceduresAsIQueryable("EXEC FindLongestName @procResult OUT", procResult)
        /// Console.WriteLine(string.Format("Longest name: {0}", procResult.Value))
        /// 
        /// **********************Where the stored procedure looks like******************
        ///     @"CREATE PROCEDURE FindLongestName
        ///         @procResult varchar(50) OUTPUT
        ///         AS
        ///         BEGIN
        ///         SET NOCOUNT ON;
        ///         SELECT TOP 1 @procResult = name from authors ORDER BY len(name) desc
        ///         END"
        /// </example2>
        int NonQueryRawSqlOrStoreProcedure(string sql, params SqlParameter[] parameters);

        #endregion

        #region Change Trackers

        /// <summary>
        /// For advance users
        /// Tells EF to iterate through the graph from the UNTRACKED root entity and apply the input callbackFunction on each of entities in the graph
        /// If the root entity is already tracked, this will no do anything
        /// </summary>
        /// <param name="graph">The root entity</param>
        /// <param name="callbackAction">Any function to be applied on all entity on the graph</param>
        /// <Example1>
        /// Tell EF to set the rootEntity's and all of its sub objects' entity states accordingly to the callbackFunction
        /// var history = new History();
        /// history.Personnels = new Personnel(); // history contains personnel
        /// historyRepository.TrackGraph(history, e => e.Entry.Sate = EntityState.Added);
        /// </Example1>
        /// <Example2>
        /// private void ApplyStateBasedOnIsKeySet(EntityEntry entry)
        /// {
        ///     if (entry.IsKeySet) // if entry has key 
        ///         entry.State = EntityState.Unchanged;
        ///     else                // if entry's key is null
        ///         entry.State = Entity.Added;
        /// }
        /// 
        /// ...
        /// var history = new History();
        /// history.Personnels = new Personnel(); // history contains personnel
        /// historyRepository.TrackGraph(history, e => ApplyStateBasedOnIsKeySet(e.Entry));
        /// </Example2>
        void TrackGraph(T rootEntity, Action<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntryGraphNode> callbackAction);

        /// <summary>
        /// Set the state of the input to the root entity accordingly to the input entity state
        /// This only apply to the root entity, and will not apply to the sub objects or related data
        /// This applies the input state to the root entity regardless of the its current state
        /// If you want to apply the input state to both the root and the related data, use the add/update/delete/etc. instead 
        /// </summary>
        /// <param name="entity"></param>
        void SetRootEntityState(T entity, EntityState state);

        EntityState GetEntityState(T entity);

        #endregion

        #region Others

        /// <summary>
        /// Reloads the entity from the database overwriting any property values with values from the database.
        /// </summary>
        void ReLoad(T entry);

        /// <summary>
        /// USE WITH CAUTIONS
        /// Get the raw DbSet of type T from EntityFrameWorkCore
        /// This can be use for eagerly loading multiple nested related data
        /// </summary>
        DbSet<T> RawDbSet();

        /// <summary>
        /// Determine whether or not an entity has changes
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool HasChanges(T entity);
        /// <summary>
        /// Determine whether or not an entity is new, and doesn't exist in the database yet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsNew(T entity);

        #endregion

        #region Misc. Note

        /// <AddMethod>
        /// Add puts all entities in the graph into the Added state. This means they will be inserted into the database on SaveChanges.
        /// </AddMethod>

        /// <AttachMethod>
        /// Attach puts all entities in the graph into the Unchanged state.
        ///     However, entities will be put in the Added state if they have store-generated keys (e.g. Identity column) and no key value has been set.
        /// This means that when exclusively using store-generated keys,
        ///     Attach can be used to start tracking a mix of new and existing entities where the existing entities have not changed.
        ///     The new entities will be inserted while the existing entities will not be saved other than to update any necessary FK values.
        /// </AttachMethod>

        /// <UpdateMethod>
        /// Update works the same as Attach except that entities are put in the Modified state instead of the Unchanged state.
        /// This means that when exclusively using store-generated keys, 
        ///     Update can be used to start tracking a mix of new and existing entities where the existing entities may have some modifications.
        ///     The new entities will be inserted while the existing entities will be updated.
        /// </UpdateMethod>

        /// <RemoveMethod>
        /// Remove affects only the entity passed to the method. It does not traverse the graph of reachable entities.
        /// If the entity is in the Unchanged or Modified state, indicating that it exists in the database, then it is put in the Deleted state.
        /// If the entity is in the Added state, indicating that it has not yet been inserted, then it is detached from the context and is no longer tracked.
        /// Remove is usually used with entities that are already being tracked. If the entity is not tracked, then it is first Attached and then placed in the Deleted state.
        /// </RemoveMethod>

        /// <RangeAddMethods>
        /// The AddRange, AttachRange, UpdateRange, and RemoveRange methods work exactly the same as calling the non-Range methods multiple times.
        /// They are slightly more efficient than multiple calls to the non-Range methods,
        ///     but the efficiency gain is very small because none of these methods now automatically call DetectChanges.
        /// </RangeAddMethods>

        /// <EntityStates>
        /// The Detached state is given to any entity that is not being tracked by the context.
        /// The Attached state is given to any entity that is being tracked by the context.
        /// Tracked entities can be in one of four states. The state of an entity determines how it is processed when SaveChanges is called.
        ///     Added: The entity does not yet exist in the database. SaveChanges should insert it.
        ///     Unchanged: The entity exists in the database and has not been modified on the client. SaveChanges should ignore it.
        ///     Modified: The entity exists in the database and has been modified on the client. SaveChanges should send updates for it.
        ///     Deleted: The entity exists in the database but should be deleted. SaveChanges should delete it.
        /// </EntityStates>

        /// <SettingStateDirectly>
        /// The state of an entity can always be set directly using the DbContext.Entry method. For example:
        ///     _context.Entry(anEntity).State = EntityState.Unchanged
        /// Setting the state this way:
        ///     1) Only affects the given entity. It does not traverse the graph of reachable entities.
        ///     2) Always sets the state specified, regardless of the current state of the entity.
        /// </SettingStateDirectly>

        /// <TrackGraph>
        /// The TrackGraph method provides full control over the state set for entities in a graph.
        /// For example, imagine your entities all have integer keys called “Id”,
        ///     and that these keys are set to negative temporary values before they are inserted into the database.
        ///     TrackGraph can then be used to set the state of each entity appropriately and inform EF that the negative values are temporary.
        /// EX:
        /// _context.ChangeTracker.TrackGraph(blog, node =>
        /// {
        ///     var entry = node.Entry;
        ///     
        ///     if ((int)entry.Property("Id").CurrentValue < 0)
        ///     {
        ///         entry.State = EntityState.Added;
        ///         entry.Property("Id").IsTemporary = true;
        ///     }
        ///     else
        ///     {
        ///         entry.State = EntityState.Modified;
        ///     }
        /// }
        /// </TrackGraph>

        /// <LoadingRelatedData>
        /// The Include is known as eagle loading, in which related objects are loaded for all parent objects without any filter can be applied
        /// Eagle loading = load instantly
        /// Ex: _context.HistoryRepo.Includes(history => history.Personnel)
        /// The explicit loading is when parent data is queried first, then it may get filtered, then the related data is loaded for the those parents only
        /// Explicit loading = defer/late loading
        /// Ex1: _context.Entry(aHistoryInstance)
        ///             .Collection(h => h.Personnels)
        ///             .Query() // linq is only available after calling .Query()
        ///             .LinqMethods()
        ///             .Load() // Related data is explicitly loaded after call .Load()
        /// Ex2: _context.Entry(aHistoryInstance)
        ///             .Reference(h => h.Personnels)
        ///             .Query() // linq is only available after calling .Query()
        ///             .LinqMethods()
        ///             .Load() // Related data is explicitly loaded after call .Load()
        ///             
        /// </LoadingRelatedData>

        /// <UseRelatedDataToFilterObjects>
        /// Yes, it is possible. EF will build a nice query that has a where clause which filters ount data based on sub/related data
        /// Ex: _context.History
        ///             .Where(hist => hist.Personel.Name.Contains("e"))
        ///             .ToList();
        /// </UseRelatedDataToFilterObjects>

        #endregion

        #region References

        /*
         * https://docs.microsoft.com/en-us/ef/core/
         * https://msdn.microsoft.com/magazine/mt767693
         * http://simon-hughes.blogspot.com/search?updated-min=2015-01-01T00:00:00Z&updated-max=2016-01-01T00:00:00Z&max-results=1
         * https://blog.oneunicorn.com/2016/11/17/add-attach-update-and-remove-methods-in-ef-core-1-1/#more-457
         */

        #endregion
    }
}
