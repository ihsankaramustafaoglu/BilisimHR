using bilisimHR.Common.Core;
using bilisimHR.Common.Helper.ServiceLocator.RevisionInfo;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;

namespace bilisimHR.Infrastructure.Dependency.CastleWindsor
{
    public class WebApiInstaller
    {
        private static IWindsorContainer _container;

        public static T Resolve<T>()
        {
            if (_container == null)
                containerInstaller();

            return tryResolve<T>();
        }

        public static IWindsorContainer Container
        {
            get
            {
                if (_container == null)
                    containerInstaller();

                return _container;
            }
        }

        public static WebApiControllerActivator Installer()
        {
            if (_container == null)
                containerInstaller();
            
            return new WebApiControllerActivator(_container);
        }

        public static void ServiceInstaller()
        {
            if (_container == null)
                createContainer();
            
            _container.Install(new ServiceInstaller());
        }

        public static void RepositoryInstaller()
        {
            if (_container == null)
                createContainer();

            _container.Install(new RepositoryInstaller());
        }

        public static void NHibernateInstaller()
        {
            if (_container == null)
                createContainer();
            
            _container.Install(new NHibernateInstaller(AppDomain.CurrentDomain.RelativeSearchPath));
        }

        public static void ControllerInstaller()
        {
            if (_container == null)
                createContainer();

            _container.Install(new ControllerInstaller());
        }

        public static void LoggerInstaller()
        {
            if (_container == null)
                createContainer();

            _container.Install(new LoggerInstaller());
        }

        public static void UnitOfWorkInterceptor()
        {
            if (_container == null)
                createContainer();

            _container.Register(Component.For<UnitOfWorkInterceptor>().LifeStyle.PerWebRequest.LifestylePerWebRequest());
        }

        private static void createContainer()
        {
            _container = new WindsorContainer();
        }

        private static void containerInstaller()
        {
            if (_container == null)
                createContainer();

            //_container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
            _container.Register(Component.For<UnitOfWorkInterceptor>().LifestyleSingleton());
            _container.Install(new ServiceInstaller());
            _container.Install(new RepositoryInstaller());
            _container.Install(new NHibernateInstaller(AppDomain.CurrentDomain.RelativeSearchPath));
            _container.Install(new ControllerInstaller());
            _container.Install(new LoggerInstaller());
            

            _container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath)).BasedOn(typeof(IRevisionInfoService)).WithService
                .AllInterfaces().Configure(c => c.LifeStyle.PerWebRequest.LifestylePerWebRequest()));
        }

        private static T tryResolve<T>()
        {
            try
            {
                if (_container.Kernel.HasComponent(typeof(T)))
                    return _container.Resolve<T>();

                return default(T);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return default(T);
            }
        }

        static void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            //Intercept all methods of all repositories.
            if (UnitOfWorkHelper.IsRepositoryClass(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }

            //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {
                if (UnitOfWorkHelper.HasUnitOfWorkAttribute(method))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
                    return;
                }
            }
        }
    }
}
