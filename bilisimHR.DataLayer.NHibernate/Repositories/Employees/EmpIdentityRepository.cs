using bilisimHR.DataLayer.Core.Entities.Employees;
using bilisimHR.DataLayer.Core.Repositories.Employees;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Employees
{
    public class EmpIdentityRepository : RepositoryBase<EmpIdentity, int>, IEmpIdentityRepository
    {
        public EmpIdentityRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
