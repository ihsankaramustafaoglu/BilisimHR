using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Employees;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Employees
{
    public class EmpEmployeeMap: EntityBaseMap<EmpEmployee>
    {
        public EmpEmployeeMap()
        {
            Table("EMP_EMPLOYEE");
            LazyLoad();
            
            Map(x => x.DrivingLicenceNumber).Column("DRIVING_LICENCE_NUMBER").Nullable();
            Map(x => x.DrivingLicenceTypeId).Column("DRIVING_LICENCE_TYPE_ID").Nullable();
            Map(x => x.SgkPositionId).Column("SGK_POSITION_ID").Nullable();
            Map(x => x.TaxNumber).Column("TAX_NUMBER").Nullable();
            Map(x => x.TaxOffice).Column("TAX_OFFICE").Nullable();
            Map(x => x.AgiStatus).Column("AGI_STATUS").Nullable();
            Map(x => x.CandidateEdate).Column("CANDIDATE_EDATE").Nullable();
            Map(x => x.CompanyId).Column("COMPANY_ID").Nullable();
            Map(x => x.CompanySdate).Column("COMPANY_SDATE").Nullable();
            Map(x => x.ContractEdate).Column("CONTRACT_EDATE").Nullable();
            Map(x => x.ContractSdate).Column("CONTRACT_SDATE").Nullable();
            Map(x => x.ContractStatusId).Column("CONTRACT_STATUS_ID").Nullable();
            Map(x => x.CostCenterId).Column("COST_CENTER_ID").Nullable();
            Map(x => x.DepartmentId).Column("DEPARTMENT_ID").Nullable();
            Map(x => x.DisabilityDegreeId).Column("DISABILITY_DEGREE_ID").Nullable();
            Map(x => x.Edate).Column("EDATE").Nullable();
            Map(x => x.EducationStatusId).Column("EDUCATION_STATUS_ID").Nullable();
            Map(x => x.EmailOfficial).Column("EMAIL_OFFICIAL").Nullable();
            Map(x => x.EmailPersonal).Column("EMAIL_PERSONAL").Nullable();
            Map(x => x.EmpGroupId).Column("EMP_GROUP_ID").Nullable();
            Map(x => x.EmpKindId).Column("EMP_KIND_ID").Nullable();
            Map(x => x.EmpTypeId).Column("EMP_TYPE_ID").Nullable();
            Map(x => x.Empno).Column("EMPNO").Nullable();
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
            Map(x => x.GenderId).Column("GENDER_ID").Nullable();
            Map(x => x.InsteadId).Column("INSTEAD_ID").Nullable();
            Map(x => x.InsuranceDate).Column("INSURANCE_DATE").Nullable();
            Map(x => x.JobchangeEdate).Column("JOBCHANGE_EDATE").Nullable();
            Map(x => x.Name).Column("NAME").Nullable();
            Map(x => x.OldEmpno).Column("OLD_EMPNO").Nullable();
            Map(x => x.OldSurname).Column("OLD_SURNAME").Nullable();
            Map(x => x.PositionId).Column("POSITION_ID").Nullable();
            Map(x => x.RecruitmentEdate).Column("RECRUITMENT_EDATE").Nullable();
            Map(x => x.RetirementDate).Column("RETIREMENT_DATE").Nullable();
            Map(x => x.Sdate).Column("SDATE").Nullable();
            Map(x => x.SeniorityRefdate).Column("SENIORITY_REFDATE").Nullable();
            Map(x => x.SeniorityRefdateCompPress).Column("SENIORITY_REFDATE_COMP_PRESS").Nullable();
            Map(x => x.SeniorityRefdatePress).Column("SENIORITY_REFDATE_PRESS").Nullable();
            Map(x => x.SgkDepartureReasonId).Column("SGK_DEPARTURE_REASON_ID").Nullable();
            Map(x => x.SgkJobId).Column("SGK_JOB_ID").Nullable();
            Map(x => x.SgkRefdate).Column("SGK_REFDATE").Nullable();
            Map(x => x.StatusId).Column("STATUS_ID").Nullable();
            Map(x => x.Surname).Column("SURNAME").Nullable();
            Map(x => x.SyndicateId).Column("SYNDICATE_ID").Nullable();
            Map(x => x.SyndicateStatusId).Column("SYNDICATE_STATUS_ID").Nullable();
            Map(x => x.TitleId).Column("TITLE_ID").Nullable();
            Map(x => x.VacationGroupId).Column("VACATION_GROUP_ID").Nullable();
            Map(x => x.VacationRefdate).Column("VACATION_REFDATE").Nullable();
            Map(x => x.WageBandId).Column("WAGE_BAND_ID").Nullable();
            Map(x => x.WorkEdate).Column("WORK_EDATE").Nullable();
            Map(x => x.WorkSdate).Column("WORK_SDATE").Nullable();
            Map(x => x.WorkingGroupId).Column("WORKING_GROUP_ID").Nullable();
            Map(x => x.WorkingStatusId).Column("WORKING_STATUS_ID").Nullable();
            Map(x => x.WorkplaceId).Column("WORKPLACE_ID").Nullable();
			References(x => x.EmpEmployeePk).Column("EMPLOYEE_PK_ID");
        }
    }
}
