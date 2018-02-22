using bilisimHR.DataLayer.Core.Entities.Parameters;
using bilisimHR.DataLayer.Core.Repositories.Parameters;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Parameters
{
    public class CodesTableRepository : RepositoryBase<CodesTable, int>, ICodesTableRepository
    {
        public CodesTableRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }

        public CodesTable GetByTableName(string tableName)
        {
            return Session.QueryOver<CodesTable>().Where(k => k.TableName == tableName).SingleOrDefault();
        }
    }
}
