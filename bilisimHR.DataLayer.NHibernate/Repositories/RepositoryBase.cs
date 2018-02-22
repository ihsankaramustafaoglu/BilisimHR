using bilisimHR.Common.Core;
using bilisimHR.Common.Core.Entity;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace bilisimHR.DataLayer.NHibernate.Repositories
{
    /// <summary>
    /// Base class for all repositories those uses NHibernate.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        #region v.1.0
        ///// <summary>
        ///// Gets the NHibernate session object to perform database operations.
        ///// </summary>
        //protected ISession Session { get { return UnitOfWork.Current.Session; } }

        ///// <summary>
        ///// Used to get a IQueryable that is used to retrive object from entire table.
        ///// </summary>
        ///// <returns>IQueryable to be used to select entities from database</returns>
        //public IQueryable<TEntity> GetAll()
        //{
        //    //return Session.Query<TEntity>();
        //    return Session.Query<TEntity>();
        //}

        ///// <summary>
        ///// Gets an entity.
        ///// </summary>
        ///// <param name="key">Primary key of the entity to get</param>
        ///// <returns>Entity</returns>
        //public TEntity Get(TPrimaryKey key)
        //{
        //    return Session.Get<TEntity>(key);
        //}

        ///// <summary>
        ///// Inserts a new entity.
        ///// </summary>
        ///// <param name="entity">Entity</param>
        //public void Insert(TEntity entity)
        //{
        //    Session.Save(entity);
        //}

        ///// <summary>
        ///// Updates an existing entity.
        ///// </summary>
        ///// <param name="entity">Entity</param>
        //public void Update(TEntity entity)
        //{
        //    Session.Update(entity);
        //}

        ///// <summary>
        ///// Deletes an entity.
        ///// </summary>
        ///// <param name="id">Id of the entity</param>
        //public void Delete(TPrimaryKey id)
        //{
        //    Session.Delete(Session.Load<TEntity>(id));
        //}
        #endregion

        #region v.2.0
        public ISessionFactory SessionFactory { get; private set; }

        public ISession Session
        {
            get { return SessionFactory.GetCurrentSession(); }
        }

        public RepositoryBase(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }
        
        public TEntity Get(TPrimaryKey key)
        {
            return Session.Get<TEntity>(key);
        }
        
        public IQueryable<TEntity> GetAll()
        {
            return Session.Query<TEntity>();
        }

        public TEntity GetBy(Expression<Func<TEntity, bool>> expression)
        {
            return GetAll().Where(expression).SingleOrDefault();
        }

        public IQueryable<TEntity> SelectBy(Expression<Func<TEntity, bool>> expression)
        {
            return GetAll().Where(expression).AsQueryable();
        }

        public TPrimaryKey Insert(TEntity entity)
        {
            var id = Session.Save(entity);
            Session.Flush();

            return (TPrimaryKey)id;
        }
        
        public void Update(TEntity entity)
        {
            Session.Update(entity);
            Session.Flush();
        }

        public void Delete(TPrimaryKey id)
        {
            Session.Delete(Session.Load<TEntity>(id));
            Session.Flush();
        }

        public void SaveOrUpdate(TEntity entity)
        {
            Session.SaveOrUpdate(entity);
            Session.Flush();
        }
        #endregion
    }
}
