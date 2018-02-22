using bilisimHR.DataLayer.Core.Entities.Organization;
using bilisimHR.DataLayer.Core.Repositories.Organization;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Organization
{
    public class OrgDepartmentPkRepository : RepositoryBase<OrgDepartmentPk, int>, IOrgDepartmentPkRepository
    {
        public OrgDepartmentPkRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
