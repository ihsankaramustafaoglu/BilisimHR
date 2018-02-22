using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.NHibernate.Helper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;

namespace bilisimHR.Infrastructure.Dependency.CastleWindsor
{
    public class NHibernateInstaller : IWindsorInstaller
    {
        private string _assemblyPath;

        public NHibernateInstaller(string assemblyPath)
        {
            _assemblyPath = assemblyPath;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(
            //    Component.For<ISessionFactory>().UsingFactoryMethod((k, m, c) => NHibernateSessionFactory.CreateSessionFactory(_assemblyPath, DBTypes.Oracle)).LifeStyle.Singleton,
            //        Component.For<ISession>().UsingFactory<ISessionFactory, ISession>(factory => factory.OpenSession()).LifestylePerThread()
            //    );

            var sessionFactory = NHibernateSessionFactory.CreateSessionFactory(DBTypes.Oracle);
            container.Register(Component.For<ISessionFactory>().UsingFactoryMethod((k) => sessionFactory).LifestyleSingleton());
        }
    }
}
