using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bilisimHR.Services.Employees.Interfaces
{
    public interface IEmpPositionDetailService : IService
    {
        Task<object> InsertAsync(EmpPositionDetailModel model);

        Task UpdateAsync(EmpPositionDetailModel model);

        Task DeleteAsync(int id);

        Task<IList<EmpPositionDetailModel>> GetAllAsync();

        Task<EmpPositionDetailModel> GetAsync(int id);
    }
}
