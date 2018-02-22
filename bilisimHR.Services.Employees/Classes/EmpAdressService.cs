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
    public class EmpAdressService : IEmpAdressService
    {
        private readonly IEmpAdressRepository _empAdressRepository;
        private readonly IEmpEmployeePkRepository _empEmployeePkRepository;

        public EmpAdressService(IEmpAdressRepository empAdressRepository, IEmpEmployeePkRepository empEmployeePkRepository)
        {
            _empAdressRepository = empAdressRepository;
            _empEmployeePkRepository = empEmployeePkRepository;
        }

        public Task DeleteAsync(int id)
        {
            _empAdressRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<EmpAdressModel>> GetAllAsync()
        {
            var dal = _empAdressRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<EmpAdressModel>>(null);
            else
            {
                IList<EmpAdressModel> modelList = new List<EmpAdressModel>();

                foreach (var user in dal)
                    modelList.Add(AutoMapperGenericHelper<EmpAdress, EmpAdressModel>.Convert(user));

                return Task.FromResult<IList<EmpAdressModel>>(modelList);
                //IQueryable<EmpAdressModel> modelList = AutoMapperGenericHelper<EmpAdress, EmpAdressModel>.ConvertAsQueryable(dal);
                //return Task.FromResult<IList<EmpAdressModel>>(modelList.ToList());
            }
        }

        public Task<EmpAdressModel> GetAsync(int id)
        {
            var dal = _empAdressRepository.Get(id);

            if (dal == null)
                return Task.FromResult<EmpAdressModel>(null);
            else
            {
                EmpAdressModel model = AutoMapperGenericHelper<EmpAdress, EmpAdressModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(EmpAdressModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpAdressModel ArgumentNullException Insert Async");

            EmpAdress dto = AutoMapperGenericHelper<EmpAdressModel, EmpAdress>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            var id = _empAdressRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(EmpAdressModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpAdressModel ArgumentNullException Insert Async");

            EmpAdress dto = AutoMapperGenericHelper<EmpAdressModel, EmpAdress>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            _empAdressRepository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
