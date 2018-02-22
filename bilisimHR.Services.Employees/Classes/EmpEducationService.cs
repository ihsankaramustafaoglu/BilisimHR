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
    public class EmpEducationService : IEmpEducationService
    {
        private readonly IEmpEducationRepository _empEducationRepository;
        private readonly IEmpEmployeePkRepository _empEmployeePkRepository;

        public EmpEducationService(IEmpEducationRepository empEducationRepository, IEmpEmployeePkRepository empEmployeePkRepository)
        {
            _empEducationRepository = empEducationRepository;
            _empEmployeePkRepository = empEmployeePkRepository;
        }

        public Task DeleteAsync(int id)
        {
            _empEducationRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<EmpEducationModel>> GetAllAsync()
        {
            var dal = _empEducationRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<EmpEducationModel>>(null);
            else
            {
                IList<EmpEducationModel> modelList = new List<EmpEducationModel>();

                foreach (var user in dal)
                    modelList.Add(AutoMapperGenericHelper<EmpEducation, EmpEducationModel>.Convert(user));

                return Task.FromResult<IList<EmpEducationModel>>(modelList);
                //IQueryable<EmpEducationModel> modelList = AutoMapperGenericHelper<EmpEducation, EmpEducationModel>.ConvertAsQueryable(dal);
                //return Task.FromResult<IList<EmpEducationModel>>(modelList.ToList());
            }
        }

        public Task<EmpEducationModel> GetAsync(int id)
        {
            var dal = _empEducationRepository.Get(id);

            if (dal == null)
                return Task.FromResult<EmpEducationModel>(null);
            else
            {
                EmpEducationModel model = AutoMapperGenericHelper<EmpEducation, EmpEducationModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(EmpEducationModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpEducationModel ArgumentNullException Insert Async");

            EmpEducation dto = AutoMapperGenericHelper<EmpEducationModel, EmpEducation>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            var id = _empEducationRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(EmpEducationModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpEducationModel ArgumentNullException Insert Async");

            EmpEducation dto = AutoMapperGenericHelper<EmpEducationModel, EmpEducation>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            _empEducationRepository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
