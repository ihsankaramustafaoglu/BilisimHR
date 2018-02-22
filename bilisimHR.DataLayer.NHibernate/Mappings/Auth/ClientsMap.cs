using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Auth;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Auth
{
    public class ClientsMap : EntityBaseMap<Clients>
    {
        //Constructor
        public ClientsMap()
        {
            Table("CLIENTS");
            LazyLoad();
            Map(x => x.ClientId).Column("CLIENT_ID").Nullable();
            Map(x => x.Secret).Column("SECRET").Not.Nullable();
            Map(x => x.Name).Column("NAME").Nullable();
            Map(x => x.ApplicationType).Column("APPLICATION_TYPE").CustomType<ApplicationTypes>().Nullable();
            Map(x => x.Active).Column("ACTIVE").Nullable(); //.Default(Guid.NewGuid().ToString("D"));
            Map(x => x.RefreshTokenLifeTime).Column("REFRESH_TOKEN_LIFE_TIME").Nullable();
            Map(x => x.AllowedOrigin).Column("ALLOWED_ORIGIN").Not.Nullable();
        }
    }
}
