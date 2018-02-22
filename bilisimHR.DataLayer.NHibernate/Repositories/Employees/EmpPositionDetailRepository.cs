using bilisimHR.DataLayer.Core.Entities.Employees;
using bilisimHR.DataLayer.Core.Repositories.Employees;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Employees
{
    public class EmpPositionDetailRepository : RepositoryBase<EmpPositionDetail, int>, IEmpPositionDetailRepository
    {
        public EmpPositionDetailRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
