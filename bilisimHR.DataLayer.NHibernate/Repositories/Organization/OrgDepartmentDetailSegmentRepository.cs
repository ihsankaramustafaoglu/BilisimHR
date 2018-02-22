using bilisimHR.DataLayer.Core.Entities.Organization;
using bilisimHR.DataLayer.Core.Repositories.Organization;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Organization
{
    public class OrgDepartmentDetailSegmentRepository : RepositoryBase<OrgDepartmentDetailSegment, int>, IOrgDepartmentDetailSegmentRepository
    {
        public OrgDepartmentDetailSegmentRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
