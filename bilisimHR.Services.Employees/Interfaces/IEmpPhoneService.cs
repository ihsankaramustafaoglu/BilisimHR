using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bilisimHR.Services.Employees.Interfaces
{
    public interface IEmpPhoneService : IService
    {
        Task<object> InsertAsync(EmpPhoneModel model);

        Task UpdateAsync(EmpPhoneModel model);

        Task DeleteAsync(int id);

        Task<IList<EmpPhoneModel>> GetAllAsync();

        Task<EmpPhoneModel> GetAsync(int id);
    }
}
