using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Auth
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken, int>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }

        public RefreshToken GetByClientIdAsync(string clientId)
        {
            return Session.QueryOver<RefreshToken>().Where(k => k.ClientId == clientId).SingleOrDefault();
        }

        public RefreshToken GetByClientIdAndUserNameAsync(string clientId, string userName)
        {
            return Session.QueryOver<RefreshToken>().Where(k => k.ClientId == clientId && k.UserName == userName).SingleOrDefault();
        }

        public RefreshToken GetByRefTokenAsync(string refreshToken)
        {
            return Session.QueryOver<RefreshToken>().Where(k => k.RefToken == refreshToken).SingleOrDefault();
        }
    }
}
