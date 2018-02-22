using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Employees
{
    public class EmpLeaveDetailModel : BaseModel<uint>
    {
        
		[Required]
        public decimal FirmId { get; set; }
        
        public decimal EmployeePkId { get; set; }
        
        public decimal PhoneTypeId { get; set; }
        
        public string PhoneCode { get; set; }
        
        public decimal PhoneNumber { get; set; }
        
        public decimal LeavingReasonId { get; set; }
        
        public decimal LeavingReasonSpecialId { get; set; }
        
        public decimal ResignationId { get; set; }
        
        public decimal LeavingRequestById { get; set; }
        
        public decimal LeavingOfferId { get; set; }
        
        public decimal LeavingModelId { get; set; }
        
        public decimal LeaveToParticipationId { get; set; }
        
        public string Explanation { get; set; }
    }
}
