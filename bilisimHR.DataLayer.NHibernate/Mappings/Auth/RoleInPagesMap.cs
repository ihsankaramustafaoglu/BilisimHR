using bilisimHR.DataLayer.Core.Entities.Auth;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Auth
{
    public class RoleInPagesMap : EntityBaseMap<RoleInPages>
    {
        //Constructor
        public RoleInPagesMap()
        {
            Table("ROLE_IN_PAGES");
            LazyLoad();
            // HasManyToMany(x => x.Users).Table("USER_IN_ROLES").Cascade.None();
            // HasManyToMany(x => x.Pages).Table("ROLE_IN_PAGES").Cascade.None();
            References(x => x.Role).Column("ROLE_ID").Not.Nullable().UniqueKey("RoleInPagesUnique");
            References(x => x.Page).Column("PAGE_ID").Not.Nullable().UniqueKey("RoleInPagesUnique");
            Map(x => x.Create).Column("OP_CREATE").Not.Nullable();
            Map(x => x.Read).Column("OP_READ").Not.Nullable();
            Map(x => x.Update).Column("OP_UPDATE").Not.Nullable();
            Map(x => x.Delete).Column("OP_DELETE").Not.Nullable();
        }
    }
}
