using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Employees;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Employees
{
    public class EmpPhoneMap: EntityBaseMap<EmpPhone>
    {
        public EmpPhoneMap()
        {
            Table("EMP_PHONE");
            LazyLoad();
            
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
            Map(x => x.PhoneTypeId).Column("PHONE_TYPE_ID").Nullable();
            Map(x => x.PhoneCode).Column("PHONE_CODE").Nullable();
            Map(x => x.PhoneNumber).Column("PHONE_NUMBER").Nullable();
			References(x => x.EmpEmployeePk).Column("EMPLOYEE_PK_ID");
        }
    }
}
