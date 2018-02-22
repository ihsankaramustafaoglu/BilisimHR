using bilisimHR.Common.Core.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace bilisimHR.Common.Core
{
    /// <summary>
    /// This interface must be implemented by all repositories to ensure UnitOfWork to work.
    /// Implement by generic version instead of this one.
    /// </summary>
    public interface IRepository
    {

    }

    /// <summary>
    /// This interface is implemented by all repositories to ensure implementation of fixed methods.
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : Entity<TPrimaryKey>
    {
        /// <summary>
        /// Used to get a IQueryable that is used to retrive entities from entire table.
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Gets an entity.
        /// </summary>
        /// <param name="key">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        TEntity Get(TPrimaryKey key);

        /// <summary>
        /// Gets first entity by expression
        /// </summary>
        /// <param name="expression">Expression for select entity</param>
        /// <returns></returns>
        TEntity GetBy(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Gets and entities by expression
        /// </summary>
        /// <param name="expression">Expression for select entities</param>
        /// <returns></returns>
        IQueryable<TEntity> SelectBy(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        TPrimaryKey Insert(TEntity entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// Save or update entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        void SaveOrUpdate(TEntity entity);
    }
}
