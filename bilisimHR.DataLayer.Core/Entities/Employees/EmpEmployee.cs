using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Employees
{
    public class EmpEmployee : Entity<int>
    {

        public virtual string DrivingLicenceNumber { get; set; }

        public virtual int DrivingLicenceTypeId { get; set; }

        public virtual int SgkPositionId { get; set; }

        public virtual string TaxNumber { get; set; }

        public virtual string TaxOffice { get; set; }
        
        public virtual int AgiStatus { get; set; }

        public virtual DateTime CandidateEdate { get; set; }

        public virtual int CompanyId { get; set; }

        public virtual DateTime CompanySdate { get; set; }

        public virtual DateTime ContractEdate { get; set; }

        public virtual DateTime ContractSdate { get; set; }

        public virtual int ContractStatusId { get; set; }

        public virtual int CostCenterId { get; set; }

        public virtual int DepartmentId { get; set; }

        public virtual int DisabilityDegreeId { get; set; }

        public virtual DateTime Edate { get; set; }

        public virtual int EducationStatusId { get; set; }

        public virtual string EmailOfficial { get; set; }

        public virtual string EmailPersonal { get; set; }

        public virtual int EmpGroupId { get; set; }

        public virtual int EmpKindId { get; set; }

        public virtual int EmpTypeId { get; set; }

        public virtual string Empno { get; set; }

        public virtual int FirmId { get; set; }

        public virtual int GenderId { get; set; }

        public virtual int InsteadId { get; set; }

        public virtual DateTime InsuranceDate { get; set; }

        public virtual DateTime JobchangeEdate { get; set; }

        public virtual string Name { get; set; }

        public virtual string OldEmpno { get; set; }

        public virtual string OldSurname { get; set; }

        public virtual int PositionId { get; set; }

        public virtual DateTime RecruitmentEdate { get; set; }

        public virtual DateTime RetirementDate { get; set; }

        public virtual DateTime Sdate { get; set; }

        public virtual DateTime SeniorityRefdate { get; set; }

        public virtual DateTime SeniorityRefdateCompPress { get; set; }

        public virtual DateTime SeniorityRefdatePress { get; set; }

        public virtual int SgkDepartureReasonId { get; set; }

        public virtual int SgkJobId { get; set; }

        public virtual DateTime SgkRefdate { get; set; }

        public virtual int StatusId { get; set; }

        public virtual string Surname { get; set; }

        public virtual int SyndicateId { get; set; }

        public virtual int SyndicateStatusId { get; set; }

        public virtual int TitleId { get; set; }

        public virtual int VacationGroupId { get; set; }

        public virtual DateTime VacationRefdate { get; set; }

        public virtual int WageBandId { get; set; }

        public virtual DateTime WorkEdate { get; set; }

        public virtual DateTime WorkSdate { get; set; }

        public virtual int WorkingGroupId { get; set; }

        public virtual int WorkingStatusId { get; set; }

        public virtual int WorkplaceId { get; set; }

		public virtual EmpEmployeePk EmpEmployeePk { get; set; }
    }
}
