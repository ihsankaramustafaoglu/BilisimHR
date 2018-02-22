using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Auth
{
    public class RolesRepository : RepositoryBase<Roles, int>, IRolesRepository
    {
        //...
        public RolesRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
