using bilisimHR.Common.Core;
using bilisimHR.DataLayer.Core.Entities.Auth;

namespace bilisimHR.DataLayer.Core.Repositories.Auth
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken, int>
    {
        RefreshToken GetByClientIdAsync(string clientId);

        RefreshToken GetByClientIdAndUserNameAsync(string clientId, string userName);

        RefreshToken GetByRefTokenAsync(string refreshToken);
    }
}
