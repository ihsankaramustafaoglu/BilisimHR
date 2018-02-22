using bilisimHR.Business.Model.Auth;
using bilisimHR.Business.Model.Email;
using bilisimHR.Common.Core;
using bilisimHR.DataLayer.Core.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.DataLayer.Core.Repositories.Auth
{
    public interface ISmtpRepository : IRepository<Smtp, int>
    {
        void SendEmailTest(SmtpModel model);
        void SendMail(EmailModel model);

    }
}
