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
    public class PagesService : IPagesService
    {
        private readonly IPagesRepository _pagesRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IControllerActionsRepository _controllerActionsRepository;

        public PagesService(IPagesRepository pagesRepository, IRolesRepository rolesRepository, IControllerActionsRepository controllerActionsRepository)
        {
            _pagesRepository = pagesRepository;
            _rolesRepository = rolesRepository;
            _controllerActionsRepository = controllerActionsRepository;
        }

        public Task<object> InsertAsync(PagesModel model)
        {
            if (model == null)
                throw new ArgumentNullException("PagesModel ArgumentNullException Insert Async");

            Pages dto = AutoMapperGenericHelper<PagesModel, Pages>.Convert(model);
            var id = _pagesRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(PagesModel model)
        {
            if (model == null)
                throw new ArgumentNullException("PagesModel ArgumentNullException Insert Async");

            Pages dto = AutoMapperGenericHelper<PagesModel, Pages>.Convert(model);
            _pagesRepository.Update(dto);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(int id)
        {
            _pagesRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<PagesModel>> GetAllAsync()
        {
            var dal = _pagesRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<PagesModel>>(null);
            else
            {
                IQueryable<PagesModel> modelList = AutoMapperGenericHelper<Pages, PagesModel>.ConvertAsQueryable(dal);
                return Task.FromResult<IList<PagesModel>>(modelList.ToList());
            }
        }

        public Task<IList<PagesModelLite>> GetAllLiteAsync()
        {
            var dal = _pagesRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<PagesModelLite>>(null);
            else
            {
                var modelList = dal.Select(u => new PagesModelLite()
                {
                    Id = u.Id,
                    Name = u.Name
                });

                return Task.FromResult<IList<PagesModelLite>>(modelList.ToList());
            }
        }

        public Task<PagesModel> GetAsync(int id)
        {
            var dal = _pagesRepository.Get(id);

            if (dal == null)
                return Task.FromResult<PagesModel>(null);
            else
            {
                PagesModel model = AutoMapperGenericHelper<Pages, PagesModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<PagesModel> GetBy(Dictionary<string, string> whereParameters)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PagesModel> SelectBy(Expression<Func<PagesModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task InsertControllerActionsAsync(int id, IList<int> controllerAcitons)
        {
            var page = _pagesRepository.Get(id);

            if (page == null)
                throw new Exception("Page couldn't find!....");
            else
            {
                foreach (int controllerActionId in controllerAcitons)
                {
                    var controllerAction = _controllerActionsRepository.Get(controllerActionId);

                    if (controllerAction == null)
                        throw new Exception("ControllerAction couldn't find(" + controllerActionId + ")!....");

                    page.ControllerActions.Add(controllerAction);
                    _pagesRepository.Update(page);
                }
            }

            return Task.FromResult<object>(null);
        }

        public Task DeleteControllerActionsAsync(int id, IList<int> controllerAcitons)
        {
            var page = _pagesRepository.Get(id);

            if (page == null)
                throw new Exception("Page couldn't find!....");
            else
            {
                foreach (int controllerActionId in controllerAcitons)
                {
                    var controllerAction = _controllerActionsRepository.Get(controllerActionId);

                    if (controllerAction == null)
                        throw new Exception("ControllerAction couldn't find(" + controllerActionId + ")!....");

                    page.ControllerActions.Remove(controllerAction);
                    _pagesRepository.Update(page);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}
