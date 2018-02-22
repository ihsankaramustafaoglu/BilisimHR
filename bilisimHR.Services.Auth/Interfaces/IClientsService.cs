using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Interfaces
{
    public interface IClientsService : IService
    {
        #region Client
        Task<object> InsertAsync(ClientsModel model);

        Task UpdateAsync(ClientsModel model);

        Task DeleteAsync(int id);

        Task<IList<ClientsModel>> GetAllAsync();

        Task<ClientsModel> GetAsync(int id);

        Task<ClientsModel> GetBy(Dictionary<string, string> whereParameters);

        IQueryable<ClientsModel> SelectBy(Expression<Func<ClientsModel, bool>> expression);

        Task<ClientsModel> GetByClientIdAsync(string clientId);
        #endregion
    }
}
