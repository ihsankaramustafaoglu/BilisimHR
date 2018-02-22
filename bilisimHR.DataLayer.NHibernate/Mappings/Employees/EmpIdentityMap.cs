using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Employees;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Employees
{
    public class EmpIdentityMap: EntityBaseMap<EmpIdentity>
    {
        public EmpIdentityMap()
        {
            Table("EMP_IDENTITY");
            LazyLoad();
            
            Map(x => x.IdentificationNumber).Column("IDENTIFICATION_NUMBER").Nullable();
            Map(x => x.SerialNumber).Column("SERIAL_NUMBER").Nullable();
            Map(x => x.WalletNumber).Column("WALLET_NUMBER").Nullable();
            Map(x => x.BirthDate).Column("BIRTH_DATE").Nullable();
            Map(x => x.BirthLocation).Column("BIRTH_LOCATION").Nullable();
            Map(x => x.BirthCountryId).Column("BIRTH_COUNTRY_ID").Nullable();
            Map(x => x.FatherName).Column("FATHER_NAME").Nullable();
            Map(x => x.MotherName).Column("MOTHER_NAME").Nullable();
            Map(x => x.MaritalStatusId).Column("MARITAL_STATUS_ID").Nullable();
            Map(x => x.NationalityId).Column("NATIONALITY_ID").Nullable();
            Map(x => x.BloodGroupId).Column("BLOOD_GROUP_ID").Nullable();
            Map(x => x.Province).Column("PROVINCE").Nullable();
            Map(x => x.District).Column("DISTRICT").Nullable();
            Map(x => x.Neighborhood).Column("NEIGHBORHOOD").Nullable();
            Map(x => x.BindingNumber).Column("BINDING_NUMBER").Nullable();
            Map(x => x.PageNumber).Column("PAGE_NUMBER").Nullable();
            Map(x => x.SequenceNumber).Column("SEQUENCE_NUMBER").Nullable();
            Map(x => x.DeliveryDate).Column("DELIVERY_DATE").Nullable();
            Map(x => x.DeliveryReasonId).Column("DELIVERY_REASON_ID").Nullable();
            Map(x => x.DeliveryPopulationManagement).Column("DELIVERY_POPULATION_MANAGEMENT").Nullable();
            Map(x => x.RecordingNumber).Column("RECORDING_NUMBER").Nullable();
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
			References(x => x.EmpEmployeePk).Column("EMPLOYEE_PK_ID");
        }
    }
}
