using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core.AutoMapping;
using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Classes
{
    public class ControllerActionsService : IControllerActionsService
    {
        private readonly IControllerActionsRepository _controllerActionsRepository;

        public ControllerActionsService(IControllerActionsRepository controllerActionsRepository)
        {
            _controllerActionsRepository = controllerActionsRepository;
        }

        public Task<object> InsertAsync(ControllerActionsModel model)
        {
            if (model == null)
                throw new ArgumentNullException("ControllerActions ArgumentNullException Insert Async");

            ControllerActions dto = AutoMapperGenericHelper<ControllerActionsModel, ControllerActions>.Convert(model);
            var id = _controllerActionsRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(ControllerActionsModel model)
        {
            if (model == null)
                throw new ArgumentNullException("ControllerActions ArgumentNullException Insert Async");

            ControllerActions dto = AutoMapperGenericHelper<ControllerActionsModel, ControllerActions>.Convert(model);
            _controllerActionsRepository.Update(dto);

            return Task.FromResult<object>(null);
        }

        public Task SaveOrUpdateAsync(ControllerActionsModel model)
        {

            if (model == null)
                throw new ArgumentNullException("ControllerActions ArgumentNullException Insert Async");

            ControllerActions dto = AutoMapperGenericHelper<ControllerActionsModel, ControllerActions>.Convert(model);
            _controllerActionsRepository.SaveOrUpdate(dto);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(int id)
        {
            _controllerActionsRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<ControllerActionsModel>> GetAllAsync()
        {
            var dal = _controllerActionsRepository.GetAll();
            
            
            if (dal == null)
                return Task.FromResult<IList<ControllerActionsModel>>(null);
            else
            {
                DateTime start = DateTime.Now;
                IQueryable<ControllerActionsModel> modelList = AutoMapperGenericHelper<ControllerActions, ControllerActionsModel>.ConvertAsQueryable(dal);
                DateTime end = DateTime.Now;

                TimeSpan span = end - start;
                int ms = (int)span.TotalMilliseconds;

                DateTime startToList = DateTime.Now;
                var list = dal.ToList();
                DateTime endToList = DateTime.Now;

                TimeSpan spanList = endToList - startToList;
                int msList = (int)spanList.TotalMilliseconds;

                return Task.FromResult<IList<ControllerActionsModel>>(modelList.ToList());
            }
        }

        public Task<ControllerActionsModel> GetAsync(int id)
        {
            var dal = _controllerActionsRepository.Get(id);

            if (dal == null)
                return Task.FromResult<ControllerActionsModel>(null);
            else
            {
                ControllerActionsModel model = AutoMapperGenericHelper<ControllerActions, ControllerActionsModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<ControllerActionsModel> GetBy(Dictionary<string, string> whereParameters)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ControllerActionsModel> SelectBy(Expression<Func<ControllerActionsModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<ControllerActionsModel> GetControllerActionsByControllerAndActionAsync(string controller, string action)
        {
            var dal = _controllerActionsRepository.GetByControllerAndActionAsync(controller, action);

            if (dal == null)
                return Task.FromResult<ControllerActionsModel>(null);
            else
            {
                ControllerActionsModel model = AutoMapperGenericHelper<ControllerActions, ControllerActionsModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }
    }
}
