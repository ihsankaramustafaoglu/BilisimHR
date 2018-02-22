using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core.AutoMapping;
using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace bilisimHR.Services.Auth.Classes
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRolesRepository _rolesRepository;

        public UsersService(IUsersRepository usersRepository, IRolesRepository rolesRepository)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
        }

        public Task<object> InsertAsync(UsersModel model)
        {
            if (model == null)
                throw new ArgumentNullException("User ArgumentNullException Insert Async");

            Users dto = AutoMapperGenericHelper<UsersModel, Users>.Convert(model);
            var id = _usersRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(UsersModel model)
        {
            if (model == null)
                throw new ArgumentNullException("User ArgumentNullException Insert Async");

            Users dto = AutoMapperGenericHelper<UsersModel, Users>.Convert(model);
            _usersRepository.Update(dto);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(int id)
        {
            _usersRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<UsersModelLite>> GetAllAsync()
        {
            var dal = _usersRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<UsersModelLite>>(null);
            else
            {
                // UsersModelLite
                var modelList = dal.Select(u => new UsersModelLite() {
                    Id = u.Id,
                    InsertedBy = u.InsertedBy,
                    InsertedDate = u.InsertedDate,
                    UpdatedBy = u.UpdatedBy,
                    UpdatedDate = u.UpdatedDate,
                    Email = u.Email,
                    EmailConfirmed = u.EmailConfirmed,
                    PhoneNumber = u.PhoneNumber,
                    PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                    TwoFactorEnabled = u.TwoFactorEnabled,
                    LockoutEndDate = u.LockoutEndDate,
                    LockoutEnabled = u.LockoutEnabled,
                    AccessFailedCount = u.AccessFailedCount,
                    UserName = u.UserName,
                    Roles = u.Roles.Select(r => r.Id).ToList()
                });

                // IQueryable<UsersModel> modelList = AutoMapperGenericHelper<Users, UsersModel>.ConvertAsQueryable(dal);
                return Task.FromResult<IList<UsersModelLite>>(modelList.ToList());
            }
        }

        public Task<UsersModelLite> GetAsync(int id)
        {
            try
            {
                var dal = _usersRepository.Get(id);

                if (dal == null)
                    return Task.FromResult<UsersModelLite>(null);
                else
                {
                    var model = new UsersModelLite()
                    {
                        Id = dal.Id,
                        InsertedBy = dal.InsertedBy,
                        InsertedDate = dal.InsertedDate,
                        UpdatedBy = dal.UpdatedBy,
                        UpdatedDate = dal.UpdatedDate,
                        Email = dal.Email,
                        EmailConfirmed = dal.EmailConfirmed,
                        PhoneNumber = dal.PhoneNumber,
                        PhoneNumberConfirmed = dal.PhoneNumberConfirmed,
                        TwoFactorEnabled = dal.TwoFactorEnabled,
                        LockoutEndDate = dal.LockoutEndDate,
                        LockoutEnabled = dal.LockoutEnabled,
                        AccessFailedCount = dal.AccessFailedCount,
                        UserName = dal.UserName,
                        Roles = dal.Roles.Select(r => r.Id).ToList()
                    };

                    //UsersModel model = new UsersModel();
                    //ReducedAutoMapper.Instance.CreateMap<Users, UsersModel>();
                    //ReducedAutoMapper.Instance.CreateMap<Roles, RolesModel>();
                    //ReducedAutoMapper.Instance.CreateMap<Roles, RolesModel>();
                    //model = ReducedAutoMapper.Instance.Map<Users, UsersModel>(dal as Users);
                    //UsersModel model = AutoMapperGenericHelper<Users, UsersModel>.Convert(dal);

                    return Task.FromResult(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task<UsersModel> GetByEmailAsync(string email)
        {
            var dal = _usersRepository.GetByEmailAsync(email);

            if (dal == null)
                return Task.FromResult<UsersModel>(null);
            else
            {
                UsersModel model = AutoMapperGenericHelper<Users, UsersModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<UsersModel> GetByUserNameAsync(string userName)
        {
            var dal = _usersRepository.GetByUserNameAsync(userName);

            if (dal == null)
                return Task.FromResult<UsersModel>(null);
            else
            {
                UsersModel model = AutoMapperGenericHelper<Users, UsersModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<UsersModel> GetBy(Expression<Func<UsersModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UsersModel> SelectBy(Expression<Func<UsersModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task InsertRolesAsync(int id, IList<int> roles)
        {
            var user = _usersRepository.Get(id);

            if (user == null)
                throw new Exception("User couldn't find!....");
            else
            {
                foreach (int roleId in roles)
                {
                    var role = _rolesRepository.Get(roleId);

                    if (role == null)
                        throw new Exception("Role couldn't find(" + roleId + ")!....");

                    user.Roles.Add(role);
                    _usersRepository.Update(user);
                }
            }

            return Task.FromResult<object>(null);
        }

        public Task DeleteRolesAsync(int id, IList<int> roles)
        {
            var user = _usersRepository.Get(id);

            if (user == null)
                throw new Exception("User couldn't find!....");
            else
            {
                foreach (int roleId in roles)
                {
                    var role = _rolesRepository.Get(roleId);

                    if (role == null)
                        throw new Exception("Role couldn't find(" + roleId + ")!....");

                    user.Roles.Remove(role);
                    _usersRepository.Update(user);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}
