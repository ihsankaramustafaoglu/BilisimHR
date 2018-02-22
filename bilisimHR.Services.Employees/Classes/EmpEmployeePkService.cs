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
    public class EmpEmployeePkService : IEmpEmployeePkService
    {
        private readonly IEmpEmployeePkRepository _empEmployeePkRepository;

        public EmpEmployeePkService(IEmpEmployeePkRepository empEmployeePkRepository)
        {
            _empEmployeePkRepository = empEmployeePkRepository;
        }

        public Task DeleteAsync(int id)
        {
            _empEmployeePkRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<EmpEmployeePkModel>> GetAllAsync()
        {
            var dal = _empEmployeePkRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<EmpEmployeePkModel>>(null);
            else
            {
                IList<EmpEmployeePkModel> modelList = new List<EmpEmployeePkModel>();

                foreach (var user in dal)
                    modelList.Add(AutoMapperGenericHelper<EmpEmployeePk, EmpEmployeePkModel>.Convert(user));

                return Task.FromResult<IList<EmpEmployeePkModel>>(modelList);
                //IQueryable<EmpEmployeePkModel> modelList = AutoMapperGenericHelper<EmpEmployeePk, EmpEmployeePkModel>.ConvertAsQueryable(dal);
                //return Task.FromResult<IList<EmpEmployeePkModel>>(modelList.ToList());
            }
        }

        public Task<EmpEmployeePkModel> GetAsync(int id)
        {
            var dal = _empEmployeePkRepository.Get(id);

            if (dal == null)
                return Task.FromResult<EmpEmployeePkModel>(null);
            else
            {
                EmpEmployeePkModel model = AutoMapperGenericHelper<EmpEmployeePk, EmpEmployeePkModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(EmpEmployeePkModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpEmployeePkModel ArgumentNullException Insert Async");

            EmpEmployeePk dto = AutoMapperGenericHelper<EmpEmployeePkModel, EmpEmployeePk>.Convert(model);

            

            var id = _empEmployeePkRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(EmpEmployeePkModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpEmployeePkModel ArgumentNullException Insert Async");

            EmpEmployeePk dto = AutoMapperGenericHelper<EmpEmployeePkModel, EmpEmployeePk>.Convert(model);

            

            _empEmployeePkRepository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
