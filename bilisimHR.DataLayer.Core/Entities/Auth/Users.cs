using bilisimHR.Common.Core.Entity;
using System;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Auth
{
    public class Users : Entity<int>
    {
        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string Salt { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual DateTime LockoutEndDate { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public virtual string UserName { get; set; }

        public virtual IList<Roles> Roles { get; protected set; }
    }
}
