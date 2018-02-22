using bilisimHR.Common.Core.Entity;
using bilisimHR.Common.Helper;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Auth
{
    public class ControllerActions : Entity<int>
    {
        public virtual string Controller { get; set; }

        public virtual string Action { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<Pages> Pages { get; protected set; }

        public virtual OperationType OperationType { get; set; }
    }
}
