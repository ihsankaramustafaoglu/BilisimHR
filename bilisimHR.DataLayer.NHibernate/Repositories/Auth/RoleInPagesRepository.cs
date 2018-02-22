using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using NHibernate;
using System.Linq;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Auth
{
    public class RoleInPagesRepository : RepositoryBase<RoleInPages, int>, IRoleInPagesRepository
    {
        public RoleInPagesRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            // ...
        }

        public IQueryable<RoleInPages> GetByRoleIdAsync(int roleId)
        {
            return Session.Query<RoleInPages>().Where(k => k.Role.Id == roleId);
        }
    }
}
