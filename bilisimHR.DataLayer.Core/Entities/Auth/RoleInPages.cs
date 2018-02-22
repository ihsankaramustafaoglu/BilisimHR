using bilisimHR.Common.Core.Entity;
using bilisimHR.Common.Helper;

namespace bilisimHR.DataLayer.Core.Entities.Auth
{
    public class RoleInPages : Entity<int>
    {
        public virtual Roles Role { get; set; }

        public virtual Pages Page { get; set; }

        public virtual bool Create { get; set; }

        public virtual bool Read { get; set; }

        public virtual bool Update { get; set; }

        public virtual bool Delete { get; set; }
    }
}
