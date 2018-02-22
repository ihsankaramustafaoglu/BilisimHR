using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bilisimHR.Services.Employees.Interfaces
{
    public interface IEmpIdentityService : IService
    {
        Task<object> InsertAsync(EmpIdentityModel model);

        Task UpdateAsync(EmpIdentityModel model);

        Task DeleteAsync(int id);

        Task<IList<EmpIdentityModel>> GetAllAsync();

        Task<EmpIdentityModel> GetAsync(int id);
    }
}
