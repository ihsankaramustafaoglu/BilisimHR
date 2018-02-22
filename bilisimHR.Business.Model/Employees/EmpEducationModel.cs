using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Employees
{
    public class EmpEducationModel : BaseModel<uint>
    {
        
		[Required]
        public decimal FirmId { get; set; }
        
        public decimal EmployeePkId { get; set; }
        
        public decimal EducationStatusId { get; set; }
        
        public DateTime Sdate { get; set; }
        
        public DateTime Edate { get; set; }
        
        public string InstitutionName { get; set; }
        
        public string FacultyName { get; set; }
        
        public string StudyFieldName { get; set; }
        
        public decimal InstitutionId { get; set; }
        
        public decimal FacultyId { get; set; }
        
        public decimal StudyFieldId { get; set; }
        
        public decimal DurationYear { get; set; }
        
        public decimal GraduateStatusId { get; set; }

		public EmpEmployeePkModel EmpEmployeePk { get; }
    }
}
