using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bilisimHR.Services.Employees.Interfaces
{
    public interface IEmpEducationService : IService
    {
        Task<object> InsertAsync(EmpEducationModel model);

        Task UpdateAsync(EmpEducationModel model);

        Task DeleteAsync(int id);

        Task<IList<EmpEducationModel>> GetAllAsync();

        Task<EmpEducationModel> GetAsync(int id);
    }
}
