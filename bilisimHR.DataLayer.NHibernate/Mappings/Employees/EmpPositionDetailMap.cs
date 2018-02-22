using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Employees;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Employees
{
    public class EmpPositionDetailMap: EntityBaseMap<EmpPositionDetail>
    {
        public EmpPositionDetailMap()
        {
            Table("EMP_POSITION_DETAIL");
            LazyLoad();
            
            Map(x => x.DepartmentId).Column("DEPARTMENT_ID").Nullable();
            Map(x => x.Sdate).Column("SDATE").Nullable();
            Map(x => x.Edate).Column("EDATE").Nullable();
            Map(x => x.PositionChangeTypeId).Column("POSITION_CHANGE_TYPE_ID").Nullable();
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
            Map(x => x.PositionId).Column("POSITION_ID").Nullable();
            Map(x => x.TitleId).Column("TITLE_ID").Nullable();
            Map(x => x.WorkplaceId).Column("WORKPLACE_ID").Nullable();
            Map(x => x.CostCenterId).Column("COST_CENTER_ID").Nullable();
            Map(x => x.CompanyId).Column("COMPANY_ID").Nullable();
			References(x => x.EmpEmployeePk).Column("EMPLOYEE_PK_ID");
        }
    }
}
