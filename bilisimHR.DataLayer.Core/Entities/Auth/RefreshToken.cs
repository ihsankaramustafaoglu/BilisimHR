using bilisimHR.Common.Core.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.DataLayer.Core.Entities.Auth
{
    public class RefreshToken : Entity<int>
    {
        public virtual string RefToken { get; set; }

        public virtual string UserName { get; set; }

        public virtual string ClientId { get; set; }

        public virtual DateTime IssuedUtc { get; set; }

        public virtual DateTime ExpiresUtc { get; set; }

        [StringLength(500)]
        public virtual string ProtectedTicket { get; set; }
    }
}
