using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Auth;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Auth
{
    public class ControllerActionsMap : EntityBaseMap<ControllerActions>
    {
        //Constructor
        public ControllerActionsMap()
        {
            Table("CONTROLLER_ACTIONS");
            LazyLoad();
            Map(x => x.Controller).Column("CONTROLLER").Nullable().UniqueKey("ControllerActionsUnique");
            Map(x => x.Action).Column("ACTION").Not.Nullable().UniqueKey("ControllerActionsUnique");
            Map(x => x.Description).Column("DESCRIPTION").Nullable();
            HasManyToMany(x => x.Pages).Table("PAGE_IN_ACTIONS").Cascade.None();
            Map(x => x.OperationType).Column("OPERATION_TYPE").CustomType<OperationType>().Not.Nullable();
        }
    }
}
