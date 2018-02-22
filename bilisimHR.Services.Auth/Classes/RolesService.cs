using bilisimHR.DataLayer.Core.Repositories.Auth;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.Collections.Generic;
using bilisimHR.Business.Model.Auth;
using bilisimHR.DataLayer.Core.Entities.Auth;
using System.Threading.Tasks;
using bilisimHR.Common.Core.AutoMapping;
using System.Linq;
using System.Linq.Expressions;

namespace bilisimHR.Services.Auth.Classes
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IPagesRepository _pagesRepository;
        private readonly IRoleInPagesRepository _roleInPageRepository;

        public RolesService(IRolesRepository rolesRepository, IUsersRepository usersRepository, IPagesRepository pagesRepository, IRoleInPagesRepository roleInPageRepository)
        {
            _rolesRepository = rolesRepository;
            _usersRepository = usersRepository;
            _pagesRepository = pagesRepository;
            _roleInPageRepository = roleInPageRepository;
        }

        public Task<object> InsertAsync(RolesModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Role ArgumentNullException Insert Async");
            
            Roles dto = AutoMapperGenericHelper<RolesModel, Roles>.Convert(model);
            var id = _rolesRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(RolesModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Role ArgumentNullException UpdateAsync");


            Roles dto = AutoMapperGenericHelper<RolesModel, Roles>.Convert(model);
            _rolesRepository.Update(dto);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(int id)
        {
            _rolesRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<RolesModel>> GetAllAsync()
        {
            DateTime start = DateTime.Now;
            var dal = _rolesRepository.GetAll();
            DateTime end = DateTime.Now;

            TimeSpan span = end - start;
            int ms = (int)span.TotalMilliseconds;
            
            if (dal == null)
                return Task.FromResult<IList<RolesModel>>(null);
            else
            {
                DateTime startMap = DateTime.Now;

                IQueryable<RolesModel> modelList = AutoMapperGenericHelper<Roles, RolesModel>.ConvertAsQueryable(dal);
                return Task.FromResult<IList<RolesModel>>(modelList.ToList());

                //IList<RolesModel> modelList2 = AutoMapperGenericHelper<Roles, RolesModel>.ConvertAsList(dal.ToList());
                //var z = modelList2.ToList();
                //IQueryable<RolesModel> modelList = AutoMapperGenericHelper<Roles, RolesModel>.ConvertAsQueryable(dal);
                //var x = modelList.ToList();
                //var y = x;


                // IList<Roles> roleList = dal.ToList();
                // IList<RolesModelBase> modelList = AutoMapperGenericHelper<Roles, RolesModelBase>.ConvertAsList(roleList);
                //DateTime endMap = DateTime.Now;

                //TimeSpan spanMap = endMap - startMap;
                //int msMap = (int)spanMap.TotalMilliseconds;

                //return Task.FromResult<IList<RolesModel>>(modelList.ToList());
            }
        }

        public Task<IList<RolesModelLite>> GetAllLiteAsync()
        {
            try
            {
                var dal = _rolesRepository.GetAll();

                if (dal == null)
                    return Task.FromResult<IList<RolesModelLite>>(null);
                else
                {
                    var modelList = dal.Select(u => new RolesModelLite()
                    {
                        Id = u.Id,
                        Name = u.Name
                    });

                    return Task.FromResult<IList<RolesModelLite>>(modelList.ToList());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<RolesModel> GetAsync(int id)
        {
            var dal = _rolesRepository.Get(id);

            if (dal == null)
                return Task.FromResult<RolesModel>(null);
            else
            {
                RolesModel model = AutoMapperGenericHelper<Roles, RolesModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<RolesModel> GetBy(Expression<Func<RolesModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RolesModel> SelectBy(Expression<Func<RolesModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task InsertUsersAsync(int id, IList<int> users)
        {
            var role = _rolesRepository.Get(id);

            if (role == null)
                throw new Exception("Role couldn't find!....");
            else
            {
                foreach (int userId in users)
                {
                    var user = _usersRepository.Get(userId);

                    if (user == null)
                        throw new Exception("User couldn't find(" + userId + ")!....");
                    
                    role.Users.Add(user);
                    _rolesRepository.Update(role);
                }
            }

            return Task.FromResult<object>(null);
        }

        public Task DeleteUsersAsync(int id, IList<int> users)
        {
            var role = _rolesRepository.Get(id);

            if (role == null)
                throw new Exception("Role couldn't find!....");
            else
            {
                foreach (int userId in users)
                {
                    var user = _usersRepository.Get(userId);

                    if (user == null)
                        throw new Exception("User couldn't find(" + userId + ")!....");

                    role.Users.Remove(user);
                    _rolesRepository.Update(role);
                }
            }

            return Task.FromResult<object>(null);
        }

        public Task InsertPagesAsync(int id, IList<NewPagesModel> pages)
        {
            var role = _rolesRepository.Get(id);

            if (role == null)
                throw new Exception("Role couldn't find!....");
            else
            {
                foreach (NewPagesModel pageModel in pages)
                {
                    var page = _pagesRepository.Get(pageModel.PageId);

                    if (page == null)
                        throw new Exception("Page couldn't find(" + pageModel.PageId + ")!....");

                    _roleInPageRepository.Insert(new RoleInPages()
                    {
                        Page = page,
                        Role = role,
                        Create = pageModel.Create,
                        Read = pageModel.Read,
                        Update = pageModel.Update,
                        Delete = pageModel.Delete,
                    });
                }
            }

            return Task.FromResult<object>(null);
        }

        public Task DeletePagesAsync(int id, IList<int> pages)
        {
            var role = _rolesRepository.Get(id);

            if (role == null)
                throw new Exception("Role couldn't find!....");
            else
            {
                foreach (int pageId in pages)
                {
                    var page = _pagesRepository.Get(pageId);

                    if (page == null)
                        throw new Exception("Page couldn't find(" + pageId + ")!....");

                    var roleInPages = _roleInPageRepository.GetAll();
                    var result = roleInPages.Where(r => r.Page == page && r.Role == role).FirstOrDefault();

                    _roleInPageRepository.Delete(result.Id);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}
