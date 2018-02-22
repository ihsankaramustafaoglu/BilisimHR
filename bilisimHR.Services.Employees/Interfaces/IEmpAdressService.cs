using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bilisimHR.Services.Employees.Interfaces
{
    public interface IEmpAdressService : IService
    {
        Task<object> InsertAsync(EmpAdressModel model);

        Task UpdateAsync(EmpAdressModel model);

        Task DeleteAsync(int id);

        Task<IList<EmpAdressModel>> GetAllAsync();

        Task<EmpAdressModel> GetAsync(int id);
    }
}
