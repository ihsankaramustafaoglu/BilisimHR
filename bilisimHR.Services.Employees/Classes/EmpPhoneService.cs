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
    public class EmpPhoneService : IEmpPhoneService
    {
        private readonly IEmpPhoneRepository _empPhoneRepository;
        private readonly IEmpEmployeePkRepository _empEmployeePkRepository;

        public EmpPhoneService(IEmpPhoneRepository empPhoneRepository, IEmpEmployeePkRepository empEmployeePkRepository)
        {
            _empPhoneRepository = empPhoneRepository;
            _empEmployeePkRepository = empEmployeePkRepository;
        }

        public Task DeleteAsync(int id)
        {
            _empPhoneRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<EmpPhoneModel>> GetAllAsync()
        {
            var dal = _empPhoneRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<EmpPhoneModel>>(null);
            else
            {
                IList<EmpPhoneModel> modelList = new List<EmpPhoneModel>();

                foreach (var user in dal)
                    modelList.Add(AutoMapperGenericHelper<EmpPhone, EmpPhoneModel>.Convert(user));

                return Task.FromResult<IList<EmpPhoneModel>>(modelList);
                //IQueryable<EmpPhoneModel> modelList = AutoMapperGenericHelper<EmpPhone, EmpPhoneModel>.ConvertAsQueryable(dal);
                //return Task.FromResult<IList<EmpPhoneModel>>(modelList.ToList());
            }
        }

        public Task<EmpPhoneModel> GetAsync(int id)
        {
            var dal = _empPhoneRepository.Get(id);

            if (dal == null)
                return Task.FromResult<EmpPhoneModel>(null);
            else
            {
                EmpPhoneModel model = AutoMapperGenericHelper<EmpPhone, EmpPhoneModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(EmpPhoneModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpPhoneModel ArgumentNullException Insert Async");

            EmpPhone dto = AutoMapperGenericHelper<EmpPhoneModel, EmpPhone>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            var id = _empPhoneRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(EmpPhoneModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpPhoneModel ArgumentNullException Insert Async");

            EmpPhone dto = AutoMapperGenericHelper<EmpPhoneModel, EmpPhone>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            _empPhoneRepository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
