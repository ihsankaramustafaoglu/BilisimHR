using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Employees;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Employees
{
    public class EmpAdressMap: EntityBaseMap<EmpAdress>
    {
        public EmpAdressMap()
        {
            Table("EMP_ADRESS");
            LazyLoad();
            
            Map(x => x.AdressTypeId).Column("ADRESS_TYPE_ID").Nullable();
            Map(x => x.Adress1).Column("ADRESS1").Nullable();
            Map(x => x.Adress2).Column("ADRESS2").Nullable();
            Map(x => x.Locality).Column("LOCALITY").Nullable();
            Map(x => x.PostCode).Column("POST_CODE").Nullable();
            Map(x => x.CityId).Column("CITY_ID").Nullable();
            Map(x => x.CountyId).Column("COUNTY_ID").Nullable();
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
            Map(x => x.Sdate).Column("SDATE").Nullable();
            Map(x => x.Edate).Column("EDATE").Nullable();
			References(x => x.EmpEmployeePk).Column("EMPLOYEE_PK_ID");
        }
    }
}
