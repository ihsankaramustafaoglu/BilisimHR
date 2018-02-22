using bilisimHR.DataLayer.Core.Repositories.Employees;
using bilisimHR.Services.Employees.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bilisimHR.Business.Model.Employees;
using bilisimHR.DataLayer.Core.Entities.Employees;
using bilisimHR.Common.Core.AutoMapping;

namespace bilisimHR.Services.Employees.Classes
{
    public class EmpEmployeeService : IEmpEmployeeService
    {
        private readonly IEmpEmployeeRepository _empEmployeeRepository;
        private readonly IEmpEmployeePkRepository _empEmployeePkRepository;

        public EmpEmployeeService(IEmpEmployeeRepository empEmployeeRepository, IEmpEmployeePkRepository empEmployeePkRepository)
        {
            _empEmployeeRepository = empEmployeeRepository;
            _empEmployeePkRepository = empEmployeePkRepository;
        }

        public Task DeleteAsync(int id)
        {
            _empEmployeeRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<EmpEmployeeModel>> GetAllAsync()
        {
            var dal = _empEmployeeRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<EmpEmployeeModel>>(null);
            else
            {
                IList<EmpEmployeeModel> modelList = new List<EmpEmployeeModel>();

                foreach (var user in dal)
                    modelList.Add(AutoMapperGenericHelper<EmpEmployee, EmpEmployeeModel>.Convert(user));

                return Task.FromResult<IList<EmpEmployeeModel>>(modelList);
                //IQueryable<EmpEmployeeModel> modelList = AutoMapperGenericHelper<EmpEmployee, EmpEmployeeModel>.ConvertAsQueryable(dal);
                //return Task.FromResult<IList<EmpEmployeeModel>>(modelList.ToList());
            }
        }

        public Task<EmpEmployeeModel> GetAsync(int id)
        {
            var dal = _empEmployeeRepository.Get(id);

            if (dal == null)
                return Task.FromResult<EmpEmployeeModel>(null);
            else
            {
                EmpEmployeeModel model = AutoMapperGenericHelper<EmpEmployee, EmpEmployeeModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(EmpEmployeeModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpEmployeeModel ArgumentNullException Insert Async");

            EmpEmployee dto = AutoMapperGenericHelper<EmpEmployeeModel, EmpEmployee>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            var id = _empEmployeeRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(EmpEmployeeModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpEmployeeModel ArgumentNullException Insert Async");

            EmpEmployee dto = AutoMapperGenericHelper<EmpEmployeeModel, EmpEmployee>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            _empEmployeeRepository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
