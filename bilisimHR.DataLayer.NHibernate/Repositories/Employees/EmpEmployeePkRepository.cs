using bilisimHR.DataLayer.Core.Entities.Employees;
using bilisimHR.DataLayer.Core.Repositories.Employees;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Employees
{
    public class EmpEmployeePkRepository : RepositoryBase<EmpEmployeePk, int>, IEmpEmployeePkRepository
    {
        public EmpEmployeePkRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
