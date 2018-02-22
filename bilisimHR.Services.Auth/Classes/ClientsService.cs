using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core.AutoMapping;
using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace bilisimHR.Services.Auth.Classes
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository _clientRepository;

        public ClientsService(IClientsRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }


        public Task<object> InsertAsync(ClientsModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Client ArgumentNullException Insert Async");
            
            Clients dto = AutoMapperGenericHelper<ClientsModel, Clients>.Convert(model);
            var id = _clientRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(ClientsModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Client ArgumentNullException Insert Async");
            
            Clients dto = AutoMapperGenericHelper<ClientsModel, Clients>.Convert(model);
            _clientRepository.Update(dto);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(int id)
        {
            _clientRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<ClientsModel>> GetAllAsync()
        {
            var dal = _clientRepository.GetAll();
            
            if (dal == null)
                return Task.FromResult<IList<ClientsModel>>(null);
            else
            {
                IQueryable<ClientsModel> modelList = AutoMapperGenericHelper<Clients, ClientsModel>.ConvertAsQueryable(dal);
                return Task.FromResult<IList<ClientsModel>>(modelList.ToList());
            }
        }

        public Task<ClientsModel> GetAsync(int id)
        {
            var dal = _clientRepository.Get(id);

            if (dal == null)
                return Task.FromResult<ClientsModel>(null);
            else
            {
                ClientsModel model = AutoMapperGenericHelper<Clients, ClientsModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<ClientsModel> GetByClientIdAsync(string clientId)
        {
            var dal = _clientRepository.GetByClientIdAsync(clientId);

            if (dal == null)
                return Task.FromResult<ClientsModel>(null);
            else
            {
                ClientsModel model = AutoMapperGenericHelper<Clients, ClientsModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<ClientsModel> GetBy(Dictionary<string, string> whereParameters)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ClientsModel> SelectBy(Expression<Func<ClientsModel, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
