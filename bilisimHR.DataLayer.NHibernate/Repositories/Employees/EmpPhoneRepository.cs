using bilisimHR.DataLayer.Core.Entities.Employees;
using bilisimHR.DataLayer.Core.Repositories.Employees;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Employees
{
    public class EmpPhoneRepository : RepositoryBase<EmpPhone, int>, IEmpPhoneRepository
    {
        public EmpPhoneRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
