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
    public class EmpLanguageService : IEmpLanguageService
    {
        private readonly IEmpLanguageRepository _empLanguageRepository;
        private readonly IEmpEmployeePkRepository _empEmployeePkRepository;

        public EmpLanguageService(IEmpLanguageRepository empLanguageRepository, IEmpEmployeePkRepository empEmployeePkRepository)
        {
            _empLanguageRepository = empLanguageRepository;
            _empEmployeePkRepository = empEmployeePkRepository;
        }

        public Task DeleteAsync(int id)
        {
            _empLanguageRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<EmpLanguageModel>> GetAllAsync()
        {
            var dal = _empLanguageRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<EmpLanguageModel>>(null);
            else
            {
                IList<EmpLanguageModel> modelList = new List<EmpLanguageModel>();

                foreach (var user in dal)
                    modelList.Add(AutoMapperGenericHelper<EmpLanguage, EmpLanguageModel>.Convert(user));

                return Task.FromResult<IList<EmpLanguageModel>>(modelList);
                //IQueryable<EmpLanguageModel> modelList = AutoMapperGenericHelper<EmpLanguage, EmpLanguageModel>.ConvertAsQueryable(dal);
                //return Task.FromResult<IList<EmpLanguageModel>>(modelList.ToList());
            }
        }

        public Task<EmpLanguageModel> GetAsync(int id)
        {
            var dal = _empLanguageRepository.Get(id);

            if (dal == null)
                return Task.FromResult<EmpLanguageModel>(null);
            else
            {
                EmpLanguageModel model = AutoMapperGenericHelper<EmpLanguage, EmpLanguageModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(EmpLanguageModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpLanguageModel ArgumentNullException Insert Async");

            EmpLanguage dto = AutoMapperGenericHelper<EmpLanguageModel, EmpLanguage>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            var id = _empLanguageRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(EmpLanguageModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpLanguageModel ArgumentNullException Insert Async");

            EmpLanguage dto = AutoMapperGenericHelper<EmpLanguageModel, EmpLanguage>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            _empLanguageRepository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
