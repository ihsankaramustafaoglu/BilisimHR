using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Interfaces
{
    public interface IRoleInPagesService : IService
    {
        Task<object> InsertAsync(RoleInPagesModel model);

        Task UpdateAsync(RoleInPagesModel model);

        Task DeleteAsync(int id);

        Task<IList<RoleInPagesModel>> GetAllAsync();

        Task<RoleInPagesModel> GetAsync(int id);

        Task<RoleInPagesModel> GetBy(Dictionary<string, string> whereParameters);

        IQueryable<RoleInPagesModel> SelectBy(Expression<Func<RoleInPagesModel, bool>> expression);

        Task<IList<RoleInPagesModel>> GetByRoleIdAsync(int roleId);
    }
}
