using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bilisimHR.Services.Employees.Interfaces
{
    public interface IEmpEmployeeService : IService
    {
        Task<object> InsertAsync(EmpEmployeeModel model);

        Task UpdateAsync(EmpEmployeeModel model);

        Task DeleteAsync(int id);

        Task<IList<EmpEmployeeModel>> GetAllAsync();

        Task<EmpEmployeeModel> GetAsync(int id);
    }
}
