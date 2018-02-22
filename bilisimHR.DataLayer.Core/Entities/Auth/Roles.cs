using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;


namespace bilisimHR.DataLayer.Core.Entities.Auth
{
    public class Roles : Entity<int>
    {
        public virtual string Name { get; set; }

        public virtual IList<Users> Users { get; protected set; }

        // public virtual IList<Pages> Pages { get; protected set; }

        public virtual IList<RoleInPages> RoleInPages { get; protected set; }

    }
}
