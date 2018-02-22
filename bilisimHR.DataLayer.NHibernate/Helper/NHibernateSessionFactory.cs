using bilisimHR.Common.Core.Entity;
using bilisimHR.Common.Helper;
using bilisimHR.Common.Helper.ServiceLocator;
using bilisimHR.Common.Helper.ServiceLocator.RevisionInfo;
using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.NHibernate.Envers;
using bilisimHR.DataLayer.NHibernate.Interceptor;
using FluentNHibernate.Cfg;
using NHibernate.Cfg;
using NHibernate.Envers.Configuration;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreNHibernate = NHibernate;
using EnversNHibernate = NHibernate.Envers;

namespace bilisimHR.DataLayer.NHibernate.Helper
{
    public class NHibernateSessionFactory
    {
        public static CoreNHibernate.ISessionFactory CreateSessionFactoryFromXML(string assemblyPath)
        {
            CoreNHibernate.Cfg.Configuration config = new CoreNHibernate.Cfg.Configuration();

            config.Configure();
            //config.AddFile(@"D:\Bilisim\SVN\bilisimHR\Web\trunk\bilisimHR.DataLayer.NHibernate.Tests\bin\x64\Debug\Mappings\HBM\Roles.hbm.xml");
            //config.AddFile(@"D:\Bilisim\SVN\bilisimHR\Web\trunk\bilisimHR.DataLayer.NHibernate.Tests\bin\x64\Debug\Mappings\HBM\Users.hbm.xml");
            //config.AddFile(@"D:\Bilisim\SVN\bilisimHR\Web\trunk\bilisimHR.DataLayer.NHibernate.Tests\bin\x64\Debug\Mappings\HBM\Category.hbm.xml");
            //config.AddFile(@"D:\Bilisim\SVN\bilisimHR\Web\trunk\bilisimHR.DataLayer.NHibernate.Tests\bin\x64\Debug\Mappings\HBM\Item.hbm.xml");
            config.AddAssembly("bilisimHR.DataLayer.Core");
            //..Todo.....

            new SchemaExport(config).Execute(true, false, false);
            //new SchemaUpdate(config).Execute(false, true);
            return config.BuildSessionFactory();
        }

        public static CoreNHibernate.ISessionFactory CreateSessionFactory(DBTypes dbType)
        {
            FluentConfiguration config = Fluently.Configure();

            switch (dbType)
            {
                case DBTypes.Oracle:
                    config = OracleHelper.ConfigurationFactory();
                    break;
                case DBTypes.MSSQL:
                    config = MsSqlHelper.ConfigurationFactory();
                    break;
                case DBTypes.SQLite:
                    config = SQLiteHelper.ConfigurationFactory();
                    break;
                case DBTypes.MySQL:
                default:
                    throw new NotImplementedException("Not implemented yet...");
            }

            var enversConf = new EnversNHibernate.Configuration.Fluent.FluentConfiguration();
            
            List<Type> domainEntities = Assembly.GetAssembly(typeof(Clients)).GetTypes() // Assembly.Load("bilisimHR.DataLayer.Core").GetTypes()
                .Where(t => (typeof(Entity<int>).IsAssignableFrom(t) && !t.IsGenericType))
                .ToList();

            foreach (Type type in domainEntities)
                enversConf.Audit(type);

            CoreNHibernate.Cfg.Configuration cfg = new CoreNHibernate.Cfg.Configuration();
            cfg = config.BuildConfiguration();

            cfg.BuildMappings();
            cfg.SetInterceptor(new TrackingInterceptor());

            //Envers RevType Values
            //0(ADD), 1(MODIFY) and 2(DELETE)
            ConfigurationKey.AuditTableSuffix.SetUserValue(cfg, "_LOG");
            IRevisionInfoService revInfoService = new RevisionInfoService();

            // Service Locator Registry
            ServiceLocator.RegisterService(revInfoService);
            ServiceLocator.RegisterService(new HttpRequestMessageService());

            enversConf.SetRevisionEntity<CustomRevInfo>(e => e.Id, e => e.RevisionDate, new CustomRevInfoListener());
            cfg.IntegrateWithEnvers(enversConf);

            config.ExposeConfiguration(exp => new SchemaUpdate(cfg).Execute(false, true))
                .ExposeConfiguration(c => { c.CurrentSessionContext<CoreNHibernate.Context.CallSessionContext>(); });
            //config.ExposeConfiguration(exp => new SchemaExport(cfg).Execute(true, true, false));
            return config.BuildSessionFactory();
        }
    }
}
