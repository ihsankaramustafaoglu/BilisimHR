using bilisimHR.DataLayer.Core.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Auth
{
    public class SmtpMap : EntityBaseMap<Smtp>
    {
        public SmtpMap()
        {
            Table("SMTP_EMAIL");
            LazyLoad();

            Map(x => x.SmtpHost).Column("SMTP_HOST");
            Map(x => x.SmtpPort).Column("SMTP_PORT");
            Map(x => x.UserMail).Column("USER_MAIL");
            Map(x => x.UserPw).Column("USER_PW");
            Map(x => x.DefaultTo).Column("DEFAULT_TO");

        }
    }
}
