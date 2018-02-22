using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Auth
{
    public class PagesRepository : RepositoryBase<Pages, int>, IPagesRepository
    {
        public PagesRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
