using bilisimHR.DataLayer.Core.Entities.Employees;
using bilisimHR.DataLayer.Core.Repositories.Employees;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Employees
{
    public class EmpLanguageRepository : RepositoryBase<EmpLanguage, int>, IEmpLanguageRepository
    {
        public EmpLanguageRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
