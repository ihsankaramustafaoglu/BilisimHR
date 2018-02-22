using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core.AutoMapping;
using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Classes
{
    public class RoleInPagesService : IRoleInPagesService
    {
        private readonly IRoleInPagesRepository _roleInPagesRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IPagesRepository _pagesRepository;

        public RoleInPagesService(IRoleInPagesRepository roleInPagesRepository, IRolesRepository rolesRepository, IPagesRepository pagesRepository)
        {
            _roleInPagesRepository = roleInPagesRepository;
            _rolesRepository = rolesRepository;
            _pagesRepository = pagesRepository;
        }

        public Task<object> InsertAsync(RoleInPagesModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Role ArgumentNullException Insert Async");

            RoleInPages dto = AutoMapperGenericHelper<RoleInPagesModel, RoleInPages>.Convert(model);
            var id = _roleInPagesRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(RoleInPagesModel model)
        {
            if (model == null)
                throw new ArgumentNullException("RoleInPages ArgumentNullException UpdateAsync");


            RoleInPages dto = AutoMapperGenericHelper<RoleInPagesModel, RoleInPages>.Convert(model);
            _roleInPagesRepository.Update(dto);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(int id)
        {
            _roleInPagesRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<RoleInPagesModel>> GetAllAsync()
        {
            var dal = _roleInPagesRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<RoleInPagesModel>>(null);
            else
            {
                IQueryable<RoleInPagesModel> modelList = AutoMapperGenericHelper<RoleInPages, RoleInPagesModel>.ConvertAsQueryable(dal);
                return Task.FromResult<IList<RoleInPagesModel>>(modelList.ToList());
            }
        }

        public Task<RoleInPagesModel> GetAsync(int id)
        {
            var dal = _roleInPagesRepository.Get(id);

            if (dal == null)
                return Task.FromResult<RoleInPagesModel>(null);
            else
            {
                RoleInPagesModel model = AutoMapperGenericHelper<RoleInPages, RoleInPagesModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<RoleInPagesModel> GetBy(Dictionary<string, string> whereParameters)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RoleInPagesModel> SelectBy(Expression<Func<RoleInPagesModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IList<RoleInPagesModel>> GetByRoleIdAsync(int roleId)
        {
            try
            {
                var dal = _roleInPagesRepository.GetByRoleIdAsync(roleId);

                if (dal == null)
                    return Task.FromResult<IList<RoleInPagesModel>>(null);
                else
                {
                    IQueryable<RoleInPagesModel> modelList = AutoMapperGenericHelper<RoleInPages, RoleInPagesModel>.ConvertAsQueryable(dal);
                    return Task.FromResult<IList<RoleInPagesModel>>(modelList.ToList());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
