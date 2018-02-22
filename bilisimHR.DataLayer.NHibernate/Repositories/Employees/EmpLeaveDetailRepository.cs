using bilisimHR.DataLayer.Core.Entities.Employees;
using bilisimHR.DataLayer.Core.Repositories.Employees;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Employees
{
    public class EmpLeaveDetailRepository : RepositoryBase<EmpLeaveDetail, int>, IEmpLeaveDetailRepository
    {
        public EmpLeaveDetailRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
