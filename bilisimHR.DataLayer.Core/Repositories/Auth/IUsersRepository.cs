using bilisimHR.Common.Core;
using bilisimHR.DataLayer.Core.Entities.Auth;

namespace bilisimHR.DataLayer.Core.Repositories.Auth
{
    public interface IUsersRepository : IRepository<Users, int>
    {
        Users GetByEmailAsync(string email);

        Users GetByUserNameAsync(string userName);
    }
}
