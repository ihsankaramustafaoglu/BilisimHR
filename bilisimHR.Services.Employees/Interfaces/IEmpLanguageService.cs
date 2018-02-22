using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bilisimHR.Services.Employees.Interfaces
{
    public interface IEmpLanguageService : IService
    {
        Task<object> InsertAsync(EmpLanguageModel model);

        Task UpdateAsync(EmpLanguageModel model);

        Task DeleteAsync(int id);

        Task<IList<EmpLanguageModel>> GetAllAsync();

        Task<EmpLanguageModel> GetAsync(int id);
    }
}
