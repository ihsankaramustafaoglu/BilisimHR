using bilisimHR.DataLayer.Core.Entities.Auth;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Auth
{
    public class RolesMap : EntityBaseMap<Roles>
    {
        //Constructor
        public RolesMap()
        {
            Table("ROLES");
            LazyLoad();
            Map(x => x.Name).Column("NAME").Not.Nullable();
            HasManyToMany(x => x.Users).Table("USER_IN_ROLES").Cascade.None();
            // HasManyToMany(x => x.Pages).Table("ROLE_IN_PAGES").Cascade.None();
            HasMany(x => x.RoleInPages).KeyColumn("ROLE_ID").Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
