using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Organization;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Organization
{
    public class OrgDepartmentDetailSegmentMap: EntityBaseMap<OrgDepartmentDetailSegment>
    {
        public OrgDepartmentDetailSegmentMap()
        {
            Table("ORG_DEPARTMENT_DETAIL_SEGMENT");
            LazyLoad();
            
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
            Map(x => x.Definition).Column("DEFINITION").Nullable();
            Map(x => x.CompanyId).Column("COMPANY_ID").Nullable();
            Map(x => x.DepartmentDetailId).Column("DEPARTMENT_DETAIL_ID").Nullable();
            Map(x => x.OrderNo).Column("ORDER_NO").Nullable();
        }
    }
}
