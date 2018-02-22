using bilisimHR.Business.Model.Auth;
using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Classes
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _tokenRepository;

        public RefreshTokenService(IRefreshTokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        #region RefreshToken
        public Task InsertAsync(RefreshTokenModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Refresh Token");

            _tokenRepository.Insert(new RefreshToken
            {
                RefToken = model.RefToken,
                ClientId = model.ClientId,
                UserName = model.UserName,
                IssuedUtc = model.IssuedUtc,
                ExpiresUtc = model.ExpiresUtc,
                ProtectedTicket = model.ProtectedTicket
            });

            return Task.FromResult<object>(null);
        }

        public Task UpdateAsync(RefreshTokenModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Refresh Token");

            throw new NotImplementedException("Update Refresh Token");

            //return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(int id)
        {
            _tokenRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<RefreshTokenModel>> GetAllAsync()
        {
            var dal = _tokenRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<RefreshTokenModel>>(null);
            else
            {
                List<RefreshTokenModel> list = new List<RefreshTokenModel>();
                foreach (RefreshToken obj in dal)
                {
                    list.Add(new RefreshTokenModel
                    {
                        Id = obj.Id,
                        RefToken = obj.RefToken,
                        ClientId = obj.ClientId,
                        UserName = obj.UserName,
                        IssuedUtc = obj.IssuedUtc,
                        ExpiresUtc = obj.ExpiresUtc,
                        ProtectedTicket = obj.ProtectedTicket,
                        InsertedBy = obj.InsertedBy,
                        InsertedDate = obj.InsertedDate,
                        UpdatedBy = obj.UpdatedBy,
                        UpdatedDate = obj.UpdatedDate
                    });
                }

                return Task.FromResult<IList<RefreshTokenModel>>(list);
            }
        }

        public Task<RefreshTokenModel> GetByClientIdAsync(string clientId)
        {
            var dal = _tokenRepository.GetByClientIdAsync(clientId);

            if (dal == null)
                return Task.FromResult<RefreshTokenModel>(null);
            else
                return Task.FromResult(new RefreshTokenModel
                {
                    Id = dal.Id,
                    RefToken = dal.RefToken,
                    ClientId = dal.ClientId,
                    UserName = dal.UserName,
                    IssuedUtc = dal.IssuedUtc,
                    ExpiresUtc = dal.ExpiresUtc,
                    ProtectedTicket = dal.ProtectedTicket,
                    InsertedBy = dal.InsertedBy,
                    InsertedDate = dal.InsertedDate,
                    UpdatedBy = dal.UpdatedBy,
                    UpdatedDate = dal.UpdatedDate
                });
        }

        public Task<RefreshTokenModel> GetByClientIdAndUserNameAsync(string clientId, string userName)
        {
            var dal = _tokenRepository.GetByClientIdAndUserNameAsync(clientId, userName);

            if (dal == null)
                return Task.FromResult<RefreshTokenModel>(null);
            else
                return Task.FromResult(new RefreshTokenModel
                {
                    Id = dal.Id,
                    RefToken = dal.RefToken,
                    ClientId = dal.ClientId,
                    UserName = dal.UserName,
                    IssuedUtc = dal.IssuedUtc,
                    ExpiresUtc = dal.ExpiresUtc,
                    ProtectedTicket = dal.ProtectedTicket,
                    InsertedBy = dal.InsertedBy,
                    InsertedDate = dal.InsertedDate,
                    UpdatedBy = dal.UpdatedBy,
                    UpdatedDate = dal.UpdatedDate
                });
        }

        public Task<RefreshTokenModel> GetByRefTokenAsync(string refreshToken)
        {
            var dal = _tokenRepository.GetByRefTokenAsync(refreshToken);

            if (dal == null)
                return Task.FromResult<RefreshTokenModel>(null);
            else
                return Task.FromResult(new RefreshTokenModel
                {
                    Id = dal.Id,
                    RefToken = dal.RefToken,
                    ClientId = dal.ClientId,
                    UserName = dal.UserName,
                    IssuedUtc = dal.IssuedUtc,
                    ExpiresUtc = dal.ExpiresUtc,
                    ProtectedTicket = dal.ProtectedTicket,
                    InsertedBy = dal.InsertedBy,
                    InsertedDate = dal.InsertedDate,
                    UpdatedBy = dal.UpdatedBy,
                    UpdatedDate = dal.UpdatedDate
                });
        }
        #endregion
    }
}
