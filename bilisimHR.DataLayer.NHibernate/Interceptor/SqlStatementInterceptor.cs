using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.SqlCommand;

namespace bilisimHR.DataLayer.NHibernate.Interceptor
{
   public class SqlStatementInterceptor : EmptyInterceptor
{
       public override SqlString OnPrepareStatement(SqlString sql)
       {
            Trace.WriteLine(sql.ToString());
            return sql;
        }
}
}
