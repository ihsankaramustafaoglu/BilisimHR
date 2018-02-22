using bilisimHR.DataLayer.Core.Entities.Auth;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Auth
{
    public class PagesMap : EntityBaseMap<Pages>
    {
        //Constructor
        public PagesMap()
        {
            Table("PAGES");
            LazyLoad();
            Map(x => x.Name).Column("NAME").Nullable();
            // HasManyToMany(x => x.Roles).Table("ROLE_IN_PAGES").Cascade.None();
            HasMany(x => x.RoleInPages).KeyColumn("PAGE_ID").Inverse().Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.ControllerActions).Table("PAGE_IN_ACTIONS").Cascade.None();
        }
    }
}
