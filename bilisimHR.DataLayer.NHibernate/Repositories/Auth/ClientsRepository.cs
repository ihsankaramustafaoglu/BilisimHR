using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using NHibernate;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Auth
{
    public class ClientsRepository : RepositoryBase<Clients, int>, IClientsRepository
    {
        public ClientsRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }

        public Clients GetByClientIdAsync(string clientId)
        {
            return Session.QueryOver<Clients>().Where(k => k.ClientId == clientId).SingleOrDefault();
        }
    }
}
