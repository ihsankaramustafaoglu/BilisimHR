﻿using bilisimHR.Common.Core;
using NHibernate.Context;
using System;
using CoreNhibernate = NHibernate;

namespace bilisimHR.DataLayer.NHibernate
{
    /// <summary>
    /// Implements Unit of work for NHibernate.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets current instance of the NhUnitOfWork.
        /// It gets the right instance that is related to current thread.
        /// </summary>
        public static UnitOfWork Current
        {
            get { return _current; }
            set { _current = value; }
        }

        [ThreadStatic]
        private static UnitOfWork _current;

        /// <summary>
        /// Gets Nhibernate session object to perform queries.
        /// </summary>
        public CoreNhibernate.ISession Session { get; private set; }

        /// <summary>
        /// Reference to the session factory.
        /// </summary>
        private readonly CoreNhibernate.ISessionFactory _sessionFactory;

        /// <summary>
        /// Reference to the currently running transcation.
        /// </summary>
        private CoreNhibernate.ITransaction _transaction;

        /// <summary>
        /// Creates a new instance of NhUnitOfWork.
        /// </summary>
        /// <param name="sessionFactory"></param>
        public UnitOfWork(CoreNhibernate.ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        /// <summary>
        /// Opens database connection and begins transaction.
        /// </summary>
        public void BeginTransaction()
        {
            Session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(Session);
            _transaction = Session.BeginTransaction();
        }

        /// <summary>
        /// Commits transaction and closes database connection.
        /// </summary>
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            finally
            {
                Session.Close();
            }
        }

        /// <summary>
        /// Rollbacks transaction and closes database connection.
        /// </summary>
        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            finally
            {
                Session.Close();
            }
        }
    }
}
