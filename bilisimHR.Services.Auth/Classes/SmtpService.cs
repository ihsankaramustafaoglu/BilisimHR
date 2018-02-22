using bilisimHR.Business.Model.Auth;
using bilisimHR.Business.Model.Email;
using bilisimHR.Common.Core.AutoMapping;
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
    public class SmtpService : ISmtpService
    {
        private readonly ISmtpRepository _smtpRepository;

        public SmtpService(ISmtpRepository smtpRepository)
        {
            _smtpRepository = smtpRepository;
        }

        public Task DeleteAsync(int id)
        {
            _smtpRepository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<SmtpModel>> GetAllAsync()
        {
            var dal = _smtpRepository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<SmtpModel>>(null);
            else
            {
                IQueryable<SmtpModel> modelList = AutoMapperGenericHelper<Smtp, SmtpModel>.ConvertAsQueryable(dal);
                return Task.FromResult<IList<SmtpModel>>(modelList.ToList());
            }
        }

        public Task<SmtpModel> GetAsync(int id)
        {
            var dal = _smtpRepository.Get(id);

            if (dal == null)
                return Task.FromResult<SmtpModel>(null);
            else
            {
                SmtpModel model = AutoMapperGenericHelper<Smtp, SmtpModel>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(SmtpModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Smtp ArgumentNullException Insert Async");

            Smtp dto = AutoMapperGenericHelper<SmtpModel, Smtp>.Convert(model);
            var id = _smtpRepository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public void SendEmailTest(SmtpModel model)
        {
            if (model == null)
                throw new ArgumentNullException("SmtpMail ArgumentNullException Testing Async");

            //Smtp dto = AutoMapperGenericHelper<SmtpModel, Smtp>.Convert(model);
            _smtpRepository.SendEmailTest(model);
        }

        public void SendMail(EmailModel model)
        {
            if (model == null)
                throw new ArgumentNullException("SmtpMail ArgumentNullException Sending Async");

            //Smtp dto = AutoMapperGenericHelper<SmtpModel, Smtp>.Convert(model);
            _smtpRepository.SendMail(model);
        }

        public Task UpdateAsync(SmtpModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Smtp ArgumentNullException Update Async");

            Smtp dto = AutoMapperGenericHelper<SmtpModel, Smtp>.Convert(model);
            _smtpRepository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
