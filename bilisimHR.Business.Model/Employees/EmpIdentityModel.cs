using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Employees
{
    public class EmpIdentityModel : BaseModel<uint>
    {
        
        public decimal IdentificationNumber { get; set; }
        
        public string SerialNumber { get; set; }
        
        public string WalletNumber { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string BirthLocation { get; set; }
        
        public decimal BirthCountryId { get; set; }
        
        public string FatherName { get; set; }
        
        public string MotherName { get; set; }
        
        public decimal MaritalStatusId { get; set; }
        
        public decimal NationalityId { get; set; }
        
        public decimal BloodGroupId { get; set; }
        
        public string Province { get; set; }
        
        public string District { get; set; }
        
        public string Neighborhood { get; set; }
        
        public string BindingNumber { get; set; }
        
        public string PageNumber { get; set; }
        
        public string SequenceNumber { get; set; }
        
        public DateTime DeliveryDate { get; set; }
        
        public decimal DeliveryReasonId { get; set; }
        
        public string DeliveryPopulationManagement { get; set; }
        
        public string RecordingNumber { get; set; }
        
		[Required]
        public decimal FirmId { get; set; }
        
        public decimal EmployeePkId { get; set; }

		public EmpEmployeePkModel EmpEmployeePk { get; }
    }
}
