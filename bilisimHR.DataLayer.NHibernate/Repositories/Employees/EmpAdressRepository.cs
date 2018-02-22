using bilisimHR.DataLayer.Core.Entities.Employees;
using bilisimHR.DataLayer.Core.Repositories.Employees;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Employees
{
    public class EmpAdressRepository : RepositoryBase<EmpAdress, int>, IEmpAdressRepository
    {
        public EmpAdressRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
