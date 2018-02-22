using bilisimHR.DataLayer.Core.Entities.Organization;
using bilisimHR.DataLayer.Core.Repositories.Organization;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Organization
{
    public class OrgDepartmentDetailRepository : RepositoryBase<OrgDepartmentDetail, int>, IOrgDepartmentDetailRepository
    {
        public OrgDepartmentDetailRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
