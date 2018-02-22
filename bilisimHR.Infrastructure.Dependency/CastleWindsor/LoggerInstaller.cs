using bilisimHR.Common.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Web.Http.Tracing;

namespace bilisimHR.Infrastructure.Dependency.CastleWindsor
{
    public class LoggerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            #region Log Register

            #region Register With Parameters
            // With Parameter
            //string confFileGeneric = ConfigurationManager.AppSettings["LoggerConfigFile"] == null ? string.Empty : ConfigurationManager.AppSettings["LoggerConfigFile"].ToString();
            //string assemblyFolderGeneric = AppDomain.CurrentDomain.RelativeSearchPath + @"\" + confFileGeneric;

            //_container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath)).BasedOn(typeof(ICoreLogger)).WithService
            //    .AllInterfaces().Configure(c => c.LifeStyle.Singleton.LifestyleTransient()
            //    .DynamicParameters((kernel, parameters) => parameters["configurationFile"] = assemblyFolderGeneric)));

            //string confFileTrace = ConfigurationManager.AppSettings["TracerConfigFile"] == null ? string.Empty : ConfigurationManager.AppSettings["TracerConfigFile"].ToString();
            //string assemblyFolderTrace = AppDomain.CurrentDomain.RelativeSearchPath + @"\" + confFileTrace;

            //_container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath)).BasedOn(typeof(ITraceWriter)).WithService
            //    .AllInterfaces().Configure(c => c.LifeStyle.Singleton.LifestyleTransient()
            //    .DynamicParameters((kernel, parameters) => parameters["configurationFile"] = assemblyFolderTrace)));
            #endregion

            #region Register Without Parameters

            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath)).BasedOn(typeof(ICoreLogger)).WithService
                .AllInterfaces().Configure(c => c.LifeStyle.Singleton.LifestyleTransient()));

            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath)).BasedOn(typeof(ITraceWriter)).WithService
                .AllInterfaces().Configure(c => c.LifeStyle.Singleton.LifestyleTransient()));

            #endregion

            #endregion
        }
    }
}
