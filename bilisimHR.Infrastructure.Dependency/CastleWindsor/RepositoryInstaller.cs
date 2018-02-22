using bilisimHR.Common.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;

namespace bilisimHR.Infrastructure.Dependency.CastleWindsor
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath)).BasedOn(typeof(IRepository<,>)).WithService
                .AllInterfaces().Configure(c => c.LifeStyle.PerWebRequest.LifestylePerWebRequest()));

            //container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
            //    .Pick()
            //    .WithServiceAllInterfaces()
            //    .LifestylePerWebRequest()
            //    .Configure(x => x.Named(x.Implementation.Name))
            //          .ConfigureIf(x => typeof(IRepository<,>).IsAssignableFrom(x.Implementation), null));
        }
    }
}
