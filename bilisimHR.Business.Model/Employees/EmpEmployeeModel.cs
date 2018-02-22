using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Employees
{
    public class EmpEmployeeModel : BaseModel<uint>
    {
        
        public string DrivingLicenceNumber { get; set; }
        
        public decimal DrivingLicenceTypeId { get; set; }
        
        public decimal SgkPositionId { get; set; }
        
        public string TaxNumber { get; set; }
        
        public string TaxOffice { get; set; }
        
        public decimal EmployeePkId { get; set; }
        
        public decimal AgiStatus { get; set; }
        
        public DateTime CandidateEdate { get; set; }
        
        public decimal CompanyId { get; set; }
        
        public DateTime CompanySdate { get; set; }
        
        public DateTime ContractEdate { get; set; }
        
        public DateTime ContractSdate { get; set; }
        
        public decimal ContractStatusId { get; set; }
        
        public decimal CostCenterId { get; set; }
        
        public decimal DepartmentId { get; set; }
        
        public decimal DisabilityDegreeId { get; set; }
        
        public DateTime Edate { get; set; }
        
        public decimal EducationStatusId { get; set; }
        
        public string EmailOfficial { get; set; }
        
        public string EmailPersonal { get; set; }
        
        public decimal EmpGroupId { get; set; }
        
        public decimal EmpKindId { get; set; }
        
        public decimal EmpTypeId { get; set; }
        
        public string Empno { get; set; }
        
		[Required]
        public decimal FirmId { get; set; }
        
        public decimal GenderId { get; set; }
        
        public decimal InsteadId { get; set; }
        
        public DateTime InsuranceDate { get; set; }
        
        public DateTime JobchangeEdate { get; set; }
        
        public string Name { get; set; }
        
        public string OldEmpno { get; set; }
        
        public string OldSurname { get; set; }
        
        public decimal PositionId { get; set; }
        
        public DateTime RecruitmentEdate { get; set; }
        
        public DateTime RetirementDate { get; set; }
        
        public DateTime Sdate { get; set; }
        
        public DateTime SeniorityRefdate { get; set; }
        
        public DateTime SeniorityRefdateCompPress { get; set; }
        
        public DateTime SeniorityRefdatePress { get; set; }
        
        public decimal SgkDepartureReasonId { get; set; }
        
        public decimal SgkJobId { get; set; }
        
        public DateTime SgkRefdate { get; set; }
        
        public decimal StatusId { get; set; }
        
        public string Surname { get; set; }
        
        public decimal SyndicateId { get; set; }
        
        public decimal SyndicateStatusId { get; set; }
        
        public decimal TitleId { get; set; }
        
        public decimal VacationGroupId { get; set; }
        
        public DateTime VacationRefdate { get; set; }
        
        public decimal WageBandId { get; set; }
        
        public DateTime WorkEdate { get; set; }
        
        public DateTime WorkSdate { get; set; }
        
        public decimal WorkingGroupId { get; set; }
        
        public decimal WorkingStatusId { get; set; }
        
        public decimal WorkplaceId { get; set; }

		public EmpEmployeePkModel EmpEmployeePk { get; }
    }
}
