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
    public class EmpLeaveDetailService : IEmpLeaveDetailService
    {
        private readonly IEmpLeaveDetailRepository _empLeaveDetailRepository;

        public EmpLeaveDetailService(IEmpLeaveDetailRepository empLeaveDetailRepository)
        {
            _empLeaveDetailRepository = empLeaveDetailRepository;
        }

        public Task DeleteAsync(int id)
        {
            _empLeaveDetailRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<EmpLeaveDetailModel>> GetAllAsync()
        {
            var dal = _empLeaveDetailRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<EmpLeaveDetailModel>>(null);
            else
            {
                IList<EmpLeaveDetailModel> modelList = new List<EmpLeaveDetailModel>();

                foreach (var user in dal)
                    modelList.Add(AutoMapperGenericHelper<EmpLeaveDetail, EmpLeaveDetailModel>.Convert(user));

                return Task.FromResult<IList<EmpLeaveDetailModel>>(modelList);
                //IQueryable<EmpLeaveDetailModel> modelList = AutoMapperGenericHelper<EmpLeaveDetail, EmpLeaveDetailModel>.ConvertAsQueryable(dal);
                //return Task.FromResult<IList<EmpLeaveDetailModel>>(modelList.ToList());
            }
        }

        public Task<EmpLeaveDetailModel> GetAsync(int id)
        {
            var dal = _empLeaveDetailRepository.Get(id);

            if (dal == null)
                return Task.FromResult<EmpLeaveDetailModel>(null);
            else
            {
                EmpLeaveDetailModel model = AutoMapperGenericHelper<EmpLeaveDetail, EmpLeaveDetailModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(EmpLeaveDetailModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpLeaveDetailModel ArgumentNullException Insert Async");

            EmpLeaveDetail dto = AutoMapperGenericHelper<EmpLeaveDetailModel, EmpLeaveDetail>.Convert(model);

            

            var id = _empLeaveDetailRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(EmpLeaveDetailModel model)
        {
            if (model == null)
                throw new ArgumentNullException("EmpLeaveDetailModel ArgumentNullException Insert Async");

            EmpLeaveDetail dto = AutoMapperGenericHelper<EmpLeaveDetailModel, EmpLeaveDetail>.Convert(model);

            

            _empLeaveDetailRepository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
