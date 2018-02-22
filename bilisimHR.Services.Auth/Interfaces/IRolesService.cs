using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Interfaces
{
    public interface IRolesService : IService
    {
        Task<object> InsertAsync(RolesModel model);

        Task UpdateAsync(RolesModel model);

        Task DeleteAsync(int id);

        Task<IList<RolesModel>> GetAllAsync();

        Task<IList<RolesModelLite>> GetAllLiteAsync();

        Task<RolesModel> GetAsync(int id);

        Task<RolesModel> GetBy(Expression<Func<RolesModel, bool>> expression);

        IQueryable<RolesModel> SelectBy(Expression<Func<RolesModel, bool>> expression);

        Task InsertUsersAsync(int id, IList<int> users);

        Task DeleteUsersAsync(int id, IList<int> users);

        Task InsertPagesAsync(int id, IList<NewPagesModel> pages);

        Task DeletePagesAsync(int id, IList<int> pages);
    }
}

























