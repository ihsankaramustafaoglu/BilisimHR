using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Employees;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Employees
{
    public class EmpEducationMap: EntityBaseMap<EmpEducation>
    {
        public EmpEducationMap()
        {
            Table("EMP_EDUCATION");
            LazyLoad();
            
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
            Map(x => x.EducationStatusId).Column("EDUCATION_STATUS_ID").Nullable();
            Map(x => x.Sdate).Column("SDATE").Nullable();
            Map(x => x.Edate).Column("EDATE").Nullable();
            Map(x => x.InstitutionName).Column("INSTITUTION_NAME").Nullable();
            Map(x => x.FacultyName).Column("FACULTY_NAME").Nullable();
            Map(x => x.StudyFieldName).Column("STUDY_FIELD_NAME").Nullable();
            Map(x => x.InstitutionId).Column("INSTITUTION_ID").Nullable();
            Map(x => x.FacultyId).Column("FACULTY_ID").Nullable();
            Map(x => x.StudyFieldId).Column("STUDY_FIELD_ID").Nullable();
            Map(x => x.DurationYear).Column("DURATION_YEAR").Nullable();
            Map(x => x.GraduateStatusId).Column("GRADUATE_STATUS_ID").Nullable();
			References(x => x.EmpEmployeePk).Column("EMPLOYEE_PK_ID");
        }
    }
}
