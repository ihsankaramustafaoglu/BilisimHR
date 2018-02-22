using bilisimHR.Common.Core;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;

namespace bilisimHR.Infrastructure.Dependency.CastleWindsor
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath)).BasedOn(typeof(IService)).WithService
                .AllInterfaces().Configure(c => c.LifeStyle.PerWebRequest.LifestylePerWebRequest().Interceptors<UnitOfWorkInterceptor>()));

            // container.AddFacility<TypedFactoryFacility>();
            
            //container.Register(
            //    Classes.FromAssemblyNamed("bilisimHR.Services.Auth")
            //        .Pick()
            //        .WithServiceAllInterfaces()
            //        .LifestylePerWebRequest()
            //        .Configure(x => x.Named(x.Implementation.Name))
            //              .ConfigureIf(x => typeof(IService).IsAssignableFrom(x.Implementation),
            //                y => y.Interceptors<UnitOfWorkInterceptor>()));
        }
    }
}
