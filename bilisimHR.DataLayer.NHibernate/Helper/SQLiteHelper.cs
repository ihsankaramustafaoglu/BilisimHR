
using bilisimHR.DataLayer.NHibernate.Envers;
using bilisimHR.DataLayer.NHibernate.Mappings.Auth;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace bilisimHR.DataLayer.NHibernate.Helper
{
    public class SQLiteHelper
    {
        public static FluentConfiguration ConfigurationFactory()
        {
            try
            {
                var configuration = Fluently.Configure().Database(SQLiteConfiguration.Standard.InMemory())
                    .Mappings(m =>
                    {
                        List<Type> mappingEntities = Assembly.GetAssembly(typeof(ClientsMap)).GetTypes()
                            .Where(t => t.Namespace.Contains("Mappings") && t.Name.Contains("Map")).ToList();

                        foreach (Type type in mappingEntities)
                        {
                            if (!type.Name.Contains("EntityBaseMap"))
                                m.FluentMappings.Add(type).Conventions.Add<StringLengthConvention>();
                        }

                        m.FluentMappings.Add(typeof(CustomRevInfoMap)).Conventions.Add<StringLengthConvention>();
                        m.FluentMappings.Add(typeof(CustomRevInfoMap));
                    });

                return configuration;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
