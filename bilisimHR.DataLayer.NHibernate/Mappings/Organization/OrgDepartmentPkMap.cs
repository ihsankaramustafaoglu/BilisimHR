using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Organization;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Organization
{
    public class OrgDepartmentPkMap: EntityBaseMap<OrgDepartmentPk>
    {
        public OrgDepartmentPkMap()
        {
            Table("ORG_DEPARTMENT_PK");
            LazyLoad();
            
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
        }
    }
}
