using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bilisimHR.Services.Employees.Interfaces
{
    public interface IEmpLeaveDetailService : IService
    {
        Task<object> InsertAsync(EmpLeaveDetailModel model);

        Task UpdateAsync(EmpLeaveDetailModel model);

        Task DeleteAsync(int id);

        Task<IList<EmpLeaveDetailModel>> GetAllAsync();

        Task<EmpLeaveDetailModel> GetAsync(int id);
    }
}
