using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Auth
{
    public class Pages : Entity<int>
    {
        public virtual string Name { get; set; }

        // public virtual IList<Roles> Roles { get; protected set; }

        public virtual IList<RoleInPages> RoleInPages { get; protected set; }

        public virtual IList<ControllerActions> ControllerActions { get; protected set; }
    }
}
