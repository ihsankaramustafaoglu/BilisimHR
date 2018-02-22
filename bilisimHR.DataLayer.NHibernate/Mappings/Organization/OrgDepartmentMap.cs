using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Organization;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Organization
{
    public class OrgDepartmentMap: EntityBaseMap<OrgDepartment>
    {
        public OrgDepartmentMap()
        {
            Table("ORG_DEPARTMENT");
            LazyLoad();
            
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
            Map(x => x.Company).Column("COMPANY").Nullable();
            Map(x => x.Sdate).Column("SDATE").Nullable();
            Map(x => x.Edate).Column("EDATE").Nullable();
            Map(x => x.Definition).Column("DEFINITION").Nullable();
            Map(x => x.OtherDefinition).Column("OTHER_DEFINITION").Nullable();
            Map(x => x.CostCenterId).Column("COST_CENTER_ID").Nullable();
            Map(x => x.Isactive).Column("ISACTIVE").Nullable();
            Map(x => x.LocationId).Column("LOCATION_ID").Nullable();
        }
    }
}
