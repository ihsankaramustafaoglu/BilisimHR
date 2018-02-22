using bilisimHR.Common.Core.Entity;
using bilisimHR.Common.Helper;

namespace bilisimHR.DataLayer.Core.Entities.Auth
{
    public class Clients : Entity<int>
    {
        public virtual string ClientId { get; set; }

        public virtual string Secret { get; set; }

        public virtual string Name { get; set; }

        public virtual ApplicationTypes ApplicationType { get; set; }

        public virtual bool Active { get; set; }

        public virtual int RefreshTokenLifeTime { get; set; }

        public virtual string AllowedOrigin { get; set; }
    }
}
