using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Interfaces
{
    public interface IRefreshTokenService : IService
    {
        #region Refresh Token
        Task InsertAsync(RefreshTokenModel model);

        Task UpdateAsync(RefreshTokenModel model);

        Task DeleteAsync(int id);

        Task<IList<RefreshTokenModel>> GetAllAsync();

        Task<RefreshTokenModel> GetByClientIdAsync(string clientId);

        Task<RefreshTokenModel> GetByClientIdAndUserNameAsync(string clientId, string userName);

        Task<RefreshTokenModel> GetByRefTokenAsync(string refreshToken);
        #endregion
    }
}
