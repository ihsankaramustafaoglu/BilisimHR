using bilisimHR.DataLayer.Core.Entities.Auth;
using FluentNHibernate.Mapping;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Auth
{
    public class RefreshTokenMap : EntityBaseMap<RefreshToken>
    {
        //Constructor
        public RefreshTokenMap()
        {
            Table("REFRESH_TOKEN");
            LazyLoad();
            Map(x => x.RefToken).Column("REF_TOKEN").Not.Nullable(); 
            Map(x => x.UserName).Column("USERNAME").Not.Nullable();
            Map(x => x.ClientId).Column("CLIENT_ID").Not.Nullable();
            Map(x => x.IssuedUtc).Column("ISSUED_UTC").Not.Nullable();
            Map(x => x.ExpiresUtc).Column("EXPIRES_UTC").Not.Nullable();
            Map(x => x.ProtectedTicket).Column("PROTECTED_TICKET").Not.Nullable();
        }
    }
}
