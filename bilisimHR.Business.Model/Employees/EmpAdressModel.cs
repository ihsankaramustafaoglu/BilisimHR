using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Employees
{
    public class EmpAdressModel : BaseModel<uint>
    {
        
        public decimal AdressTypeId { get; set; }
        
        public string Adress1 { get; set; }
        
        public string Adress2 { get; set; }
        
        public string Locality { get; set; }
        
        public string PostCode { get; set; }
        
        public decimal CityId { get; set; }
        
        public decimal CountyId { get; set; }
        
		[Required]
        public decimal FirmId { get; set; }
        
        public decimal EmployeePkId { get; set; }
        
        public DateTime Sdate { get; set; }
        
        public DateTime Edate { get; set; }

		public EmpEmployeePkModel EmpEmployeePk { get; }
    }
}
