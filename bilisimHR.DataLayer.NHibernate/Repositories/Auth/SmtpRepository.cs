using bilisimHR.Business.Model.Auth;
using bilisimHR.Business.Model.Email;
using bilisimHR.Common.Core.AutoMapping;
using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Auth
{
    public class SmtpRepository : RepositoryBase<Smtp, int>, ISmtpRepository
    {
        public SmtpRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }

        public void SendEmailTest(SmtpModel model)
        {
            MailMessage msg = new MailMessage();

            if (model.DefaultTo != "" && model.DefaultTo != null)
                msg.To.Add(new MailAddress(model.DefaultTo.Trim()));

            string sendFromEmail = model.UserMail;
            string sendFromPassword = model.UserPw;

            msg.From = new MailAddress(sendFromEmail);
            msg.Subject = "Test Mesajı";
            msg.Body = "Bu mail Smtp test amaçlı atılmıştır.";
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(model.SmtpHost);
            client.Port = model.SmtpPort;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            NetworkCredential cred = new NetworkCredential(sendFromEmail, sendFromPassword);
            client.Credentials = cred;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            try
            {
                client.Send(msg);
                msg.Dispose();

            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void SendMail(EmailModel model)
        {
            var smtpailObj = Get(1);
            SmtpModel obj = AutoMapperGenericHelper<Smtp, SmtpModel>.Convert(smtpailObj);


            MailMessage msg = new MailMessage();
            if (obj != null)
            {
                //gelen string list e göre alıcıları msg a ekler.
                if (model.Recipient != null)
                {
                    foreach (string currentEmailAddress in model.Recipient)
                    {
                        msg.To.Add(new MailAddress(currentEmailAddress.Trim()));
                    }
                }
                else
                {
                    //gelen yoksa default kullanıcıyı ekler.
                    msg.To.Add(new MailAddress(obj.DefaultTo.Trim()));
                }
                //filename ve filecontent dictionary den okunup attachment olarak eklenir
                if (model.dictionaryMail != null && model.dictionaryMail.Keys.Count() > 0)
                {
                    foreach (KeyValuePair<string, byte[]> entry in model.dictionaryMail)
                    {
                        Attachment attc = new Attachment(new MemoryStream(entry.Value), entry.Key);
                        msg.Attachments.Add(attc);
                    }
                }


                string sendFromEmail = obj.UserMail;
                string sendFromPassword = obj.UserPw;

                msg.From = new MailAddress(sendFromEmail);
                msg.Subject = model.Subject;
                msg.Body = model.Body;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(obj.SmtpHost);
                client.Port = obj.SmtpPort;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                NetworkCredential cred = new NetworkCredential(sendFromEmail, sendFromPassword);
                client.Credentials = cred;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                try
                {
                    client.Send(msg);
                    msg.Dispose();

                }
                catch (Exception e)
                {
                    e.ToString();
                }
            }
        }
    }
}

