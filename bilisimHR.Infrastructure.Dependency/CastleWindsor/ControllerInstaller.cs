using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace bilisimHR.Infrastructure.Dependency.CastleWindsor
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath)).BasedOn<IController>().LifestylePerWebRequest());
            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath)).BasedOn<IHttpController>().LifestylePerWebRequest());

            //    container.Register(
            //     Component.For<IController>().ImplementedBy<IController>()
            // );

            //    container.Register(
            //    Component.For<IHttpController>().ImplementedBy<IHttpController>()
            //);

        }
    }
}
