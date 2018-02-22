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
    public class EmpPositionDetailService : IEmpPositionDetailService
    {
        private readonly IEmpPositionDetailRepository _empPositionDetailRepository;
        private readonly IEmpEmployeePkRepository _empEmployeePkRepository;

        public EmpPositionDetailService(IEmpPositionDetailRepository empPositionDetailRepository, IEmpEmployeePkRepository empEmployeePkRepository)
        {
            _empPositionDetailRepository = empPositionDetailRepository;
            _empEmployeePkRepository = empEmployeePkRepository;
        }

        public Task DeleteAsync(int id)
        {
            _empPositionDetailRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<EmpPositionDetailModel>> GetAllAsync()
        {
            var dal = _empPositionDetailRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<EmpPositionDetailModel>>(null);
            else
            {
                IList<EmpPositionDetailModel> modelList = new List<EmpPositionDetailModel>();

                foreach (var user in dal)
                    modelList.Add(AutoMapperGenericHelper<EmpPositionDetail, EmpPositionDetailModel>.Convert(user));

                return Task.FromResult<IList<EmpPositionDetailModel>>(modelList);
                //IQueryable<EmpPositionDetailModel> modelList = AutoMapperGenericHelper<EmpPositionDetail, EmpPositionDetailModel>.ConvertAsQueryable(dal);
                //return Task.FromResult<IList<EmpPositionDetailModel>>(modelList.ToList());
            }
        }

        public Task<EmpPositionDetailModel> GetAsync(int id)
        {
            var dal = _empPositionDetailRepository.Get(id);

            if (dal == null)
                return Task.FromResult<EmpPositionDetailModel>(null);
            else
            {
                EmpPositionDetailModel model = AutoMapperGenericHelper<EmpPositionDetail, EmpPositionDetailModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(EmpPositionDetailModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpPositionDetailModel ArgumentNullException Insert Async");

            EmpPositionDetail dto = AutoMapperGenericHelper<EmpPositionDetailModel, EmpPositionDetail>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            var id = _empPositionDetailRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(EmpPositionDetailModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpPositionDetailModel ArgumentNullException Insert Async");

            EmpPositionDetail dto = AutoMapperGenericHelper<EmpPositionDetailModel, EmpPositionDetail>.Convert(model);

            EmpEmployeePk pk1 = _empEmployeePkRepository.Get((int)model.EmployeePkId);
            dto.EmpEmployeePk = pk1;
            if (pk1 == null)
                throw new ArgumentNullException("EmployeePkId ArgumentNullException Insert Async");


            _empPositionDetailRepository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
