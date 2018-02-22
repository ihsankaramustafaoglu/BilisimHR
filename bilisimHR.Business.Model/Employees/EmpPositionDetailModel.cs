using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Employees
{
    public class EmpPositionDetailModel : BaseModel<uint>
    {
        
        public decimal EmployeePkId { get; set; }
        
        public decimal DepartmentId { get; set; }
        
        public DateTime Sdate { get; set; }
        
        public DateTime Edate { get; set; }
        
        public decimal PositionChangeTypeId { get; set; }
        
		[Required]
        public decimal FirmId { get; set; }
        
        public decimal PositionId { get; set; }
        
        public decimal TitleId { get; set; }
        
        public decimal WorkplaceId { get; set; }
        
        public decimal CostCenterId { get; set; }
        
        public decimal CompanyId { get; set; }

		public EmpEmployeePkModel EmpEmployeePk { get; }
    }
}
