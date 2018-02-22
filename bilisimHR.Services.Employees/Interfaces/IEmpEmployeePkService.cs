using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bilisimHR.Services.Employees.Interfaces
{
    public interface IEmpEmployeePkService : IService
    {
        Task<object> InsertAsync(EmpEmployeePkModel model);

        Task UpdateAsync(EmpEmployeePkModel model);

        Task DeleteAsync(int id);

        Task<IList<EmpEmployeePkModel>> GetAllAsync();

        Task<EmpEmployeePkModel> GetAsync(int id);
    }
}
