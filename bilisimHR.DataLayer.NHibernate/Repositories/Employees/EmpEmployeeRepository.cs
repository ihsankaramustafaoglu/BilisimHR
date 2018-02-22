using bilisimHR.DataLayer.Core.Entities.Employees;
using bilisimHR.DataLayer.Core.Repositories.Employees;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Employees
{
    public class EmpEmployeeRepository : RepositoryBase<EmpEmployee, int>, IEmpEmployeeRepository
    {
        public EmpEmployeeRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
