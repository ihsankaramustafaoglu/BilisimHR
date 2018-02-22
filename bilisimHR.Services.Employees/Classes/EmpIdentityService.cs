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
    public class EmpIdentityService : IEmpIdentityService
    {
        private readonly IEmpIdentityRepository _empIdentityRepository;
        private readonly IEmpEmployeePkRepository _empEmployeePkRepository;

        public EmpIdentityService(IEmpIdentityRepository empIdentityRepository, IEmpEmployeePkRepository empEmployeePkRepository)
        {
            _empIdentityRepository = empIdentityRepository;
            _empEmployeePkRepository = empEmployeePkRepository;
        }

        public Task DeleteAsync(int id)
        {
            _empIdentityRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<EmpIdentityModel>> GetAllAsync()
        {
            var dal = _empIdentityRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<EmpIdentityModel>>(null);
            else
            {
                IList<EmpIdentityModel> modelList = new List<EmpIdentityModel>();

                foreach (var user in dal)
                    modelList.Add(AutoMapperGenericHelper<EmpIdentity, EmpIdentityModel>.Convert(user));

                return Task.FromResult<IList<EmpIdentityModel>>(modelList);
                //IQueryable<EmpIdentityModel> modelList = AutoMapperGenericHelper<EmpIdentity, EmpIdentityModel>.ConvertAsQueryable(dal);
                //return Task.FromResult<IList<EmpIdentityModel>>(modelList.ToList());
            }
        }

        public Task<EmpIdentityModel> GetAsync(int id)
        {
            var dal = _empIdentityRepository.Get(id);

            if (dal == null)
                return Task.FromResult<EmpIdentityModel>(null);
            else
            {
                EmpIdentityModel model = AutoMapperGenericHelper<EmpIdentity, EmpIdentityModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(EmpIdentityModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpIdentityModel ArgumentNullException Insert Async");

            EmpIdentity dto = AutoMapperGenericHelper<EmpIdentityModel, EmpIdentity>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            var id = _empIdentityRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(EmpIdentityModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpIdentityModel ArgumentNullException Insert Async");

            EmpIdentity dto = AutoMapperGenericHelper<EmpIdentityModel, EmpIdentity>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            _empIdentityRepository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
