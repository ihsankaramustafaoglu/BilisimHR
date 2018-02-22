using bilisimHR.Common.Core;
using bilisimHR.DataLayer.Core.Entities.Auth;

namespace bilisimHR.DataLayer.Core.Repositories.Auth
{
    public interface IClientsRepository : IRepository<Clients, int>
    {
        Clients GetByClientIdAsync(string clientId);
    }
}
