
using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Interfaces
{
    public interface IControllerActionsService : IService
    {
        Task<object> InsertAsync(ControllerActionsModel model);

        Task UpdateAsync(ControllerActionsModel model);

        Task SaveOrUpdateAsync(ControllerActionsModel model);

        Task DeleteAsync(int id);

        Task<IList<ControllerActionsModel>> GetAllAsync();

        Task<ControllerActionsModel> GetAsync(int id);

        Task<ControllerActionsModel> GetBy(Dictionary<string, string> whereParameters);

        IQueryable<ControllerActionsModel> SelectBy(Expression<Func<ControllerActionsModel, bool>> expression);

        Task<ControllerActionsModel> GetControllerActionsByControllerAndActionAsync(string controller, string action);
    }
}
