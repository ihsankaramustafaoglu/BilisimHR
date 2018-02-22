using bilisimHR.Common.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.DataLayer.Core.Entities.Auth
{
    public class Smtp : Entity<int>
    {
        public virtual string SmtpHost { get; set; }

        public virtual int SmtpPort { get; set; }

        public virtual string UserMail { get; set; }

        public virtual string UserPw { get; set; }

        public virtual string DefaultTo { get; set; }

    }
}
