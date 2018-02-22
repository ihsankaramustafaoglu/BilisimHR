using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Employees
{
    public class EmpPhoneModel : BaseModel<uint>
    {
        
		[Required]
        public decimal FirmId { get; set; }
        
        public decimal EmployeePkId { get; set; }
        
        public decimal PhoneTypeId { get; set; }
        
        public string PhoneCode { get; set; }
        
        public decimal PhoneNumber { get; set; }

		public EmpEmployeePkModel EmpEmployeePk { get; }
    }
}
