using bilisimHR.DataLayer.Core.Entities.Employees;
using bilisimHR.DataLayer.Core.Repositories.Employees;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Employees
{
    public class EmpEducationRepository : RepositoryBase<EmpEducation, int>, IEmpEducationRepository
    {
        public EmpEducationRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
