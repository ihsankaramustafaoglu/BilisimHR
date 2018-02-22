using bilisimHR.Business.Model.Auth;
using bilisimHR.Business.Model.Email;
using bilisimHR.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Interfaces
{
    public interface ISmtpService : IService
    {

        #region Smtp
        Task<object> InsertAsync(SmtpModel model);

        Task UpdateAsync(SmtpModel model);

        Task DeleteAsync(int id);

        Task<IList<SmtpModel>> GetAllAsync();

        Task<SmtpModel> GetAsync(int id);

        void SendEmailTest(SmtpModel model);

        void SendMail(EmailModel model);

        #endregion
    }
}
