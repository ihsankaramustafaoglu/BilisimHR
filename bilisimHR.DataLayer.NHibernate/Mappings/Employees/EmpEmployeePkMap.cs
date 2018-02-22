using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Employees;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Employees
{
    public class EmpEmployeePkMap : EntityBaseMap<EmpEmployeePk>
    {
        public EmpEmployeePkMap()
        {
            Table("EMP_EMPLOYEE_PK");
            LazyLoad();

            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();

            //HasMany(x => x.AuthUsers).KeyColumn("EMPLOYEE_PK_ID").Inverse().Cascade.None().ForeignKeyConstraintName("FK_USERS_ID");

            HasMany(x => x.EmpPositionDetail).KeyColumn("EMPLOYEE_PK_ID").Inverse().Cascade.None().ForeignKeyConstraintName("FK_EMP_POSITION_DETAIL_ID");

            HasMany(x => x.EmpPhone).KeyColumn("EMPLOYEE_PK_ID").Inverse().Cascade.None().ForeignKeyConstraintName("FK_EMP_PHONE_ID");

            HasMany(x => x.EmpLanguage).KeyColumn("EMPLOYEE_PK_ID").Inverse().Cascade.None().ForeignKeyConstraintName("FK_EMP_LANGUAGE_ID");

            HasMany(x => x.EmpIdentity).KeyColumn("EMPLOYEE_PK_ID").Inverse().Cascade.None().ForeignKeyConstraintName("FK_EMP_IDENTITY_ID");

            HasMany(x => x.EmpEducation).KeyColumn("EMPLOYEE_PK_ID").Inverse().Cascade.None().ForeignKeyConstraintName("FK_EMP_EDUCATION_ID");

            HasMany(x => x.EmpAdress).KeyColumn("EMPLOYEE_PK_ID").Inverse().Cascade.None().ForeignKeyConstraintName("FK_EMP_ADRESS_ID");

            HasMany(x => x.EmpEmployee).KeyColumn("EMPLOYEE_PK_ID").Inverse().Cascade.None().ForeignKeyConstraintName("FK_EMPLOYEE_ID");
        }
    }
}
