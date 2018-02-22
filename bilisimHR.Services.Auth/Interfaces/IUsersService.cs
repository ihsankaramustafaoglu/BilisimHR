using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Interfaces
{
    public interface IUsersService : IService
    {
        Task<object> InsertAsync(UsersModel model);

        Task UpdateAsync(UsersModel model);

        Task DeleteAsync(int id);

        Task<IList<UsersModelLite>> GetAllAsync();

        Task<UsersModelLite> GetAsync(int id);
        
        Task<UsersModel> GetBy(Expression<Func<UsersModel, bool>> expression);

        IQueryable<UsersModel> SelectBy(Expression<Func<UsersModel, bool>> expression);

        Task<UsersModel> GetByEmailAsync(string email);

        Task<UsersModel> GetByUserNameAsync(string userName);

        Task InsertRolesAsync(int id, IList<int> roles);

        Task DeleteRolesAsync(int id, IList<int> roles);
    }
}
