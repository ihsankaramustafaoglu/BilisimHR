using bilisimHR.Common.Core;
using bilisimHR.DataLayer.NHibernate;
using Castle.DynamicProxy;
using NHibernate;
using System;
using System.Reflection;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace bilisimHR.Infrastructure.Dependency.CastleWindsor
{
    /// <summary>
    /// This interceptor is used to manage transactions.
    /// </summary>
    public class UnitOfWorkInterceptor : IInterceptor
    {
        private readonly ISessionFactory _sessionFactory;

        /// <summary>
        /// Creates a new NhUnitOfWorkInterceptor object.
        /// </summary>
        /// <param name="sessionFactory">Nhibernate session factory.</param>
        public UnitOfWorkInterceptor(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        /// <summary>
        /// Intercepts a method.
        /// </summary>
        /// <param name="invocation">Method invocation arguments</param>
        public void Intercept(IInvocation invocation)
        {
            //If there is a running transaction, just run the method
            //if (UnitOfWork.Current != null || !RequiresDbConnection(invocation.MethodInvocationTarget))
            //{
            //    invocation.Proceed();
            //    return;
            //}

            try
            {
                UnitOfWork.Current = new UnitOfWork(_sessionFactory);
                UnitOfWork.Current.BeginTransaction();
                try
                {
                    invocation.Proceed();
                    UnitOfWork.Current.Commit();
                }
                catch(Exception ex)
                {
                    try
                    {
                        UnitOfWork.Current.Rollback();
                    }
                    catch
                    {
                        var error = ex.Message;
                    }

                    throw;
                }
            }
            finally
            {
                UnitOfWork.Current = null;
            }
        }

        private static bool RequiresDbConnection(MethodInfo methodInfo)
        {
            if (UnitOfWorkHelper.HasUnitOfWorkAttribute(methodInfo))
            {
                return true;
            }

            if (UnitOfWorkHelper.IsRepositoryMethod(methodInfo))
            {
                return true;
            }

            return false;
        }
    }
}
