using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Organization;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Organization
{
    public class OrgDepartmentDetailMap: EntityBaseMap<OrgDepartmentDetail>
    {
        public OrgDepartmentDetailMap()
        {
            Table("ORG_DEPARTMENT_DETAIL");
            LazyLoad();
            
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
            Map(x => x.DepartmentDetailSegmentId).Column("DEPARTMENT_DETAIL_SEGMENT_ID").Nullable();
            Map(x => x.Sdate).Column("SDATE").Nullable();
            Map(x => x.Edate).Column("EDATE").Nullable();
            Map(x => x.DepartmentPkId).Column("DEPARTMENT_PK_ID").Nullable();
        }
    }
}
