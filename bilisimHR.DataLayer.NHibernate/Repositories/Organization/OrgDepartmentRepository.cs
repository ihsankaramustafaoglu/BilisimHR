using bilisimHR.DataLayer.Core.Entities.Organization;
using bilisimHR.DataLayer.Core.Repositories.Organization;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Organization
{
    public class OrgDepartmentRepository : RepositoryBase<OrgDepartment, int>, IOrgDepartmentRepository
    {
        public OrgDepartmentRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
