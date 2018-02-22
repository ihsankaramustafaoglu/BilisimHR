using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Auth
{
    public class UsersRepository : RepositoryBase<Users, int>, IUsersRepository
    {
        public UsersRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }

        public Users GetByEmailAsync(string email)
        {
            return Session.QueryOver<Users>().Where(k => k.Email == email).SingleOrDefault();
        }

        public Users GetByUserNameAsync(string userName)
        {
            return Session.QueryOver<Users>().Where(k => k.UserName == userName).SingleOrDefault();
        }
    }
}
