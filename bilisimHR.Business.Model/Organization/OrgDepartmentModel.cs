using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Organization
{
    public class OrgDepartmentModel : BaseModel<uint>
    {
        
		[Required]
        public decimal FirmId { get; set; }
        
        public string Company { get; set; }
        
        public DateTime Sdate { get; set; }
        
        public DateTime Edate { get; set; }
        
        public string Definition { get; set; }
        
        public string OtherDefinition { get; set; }
        
        public decimal CostCenterId { get; set; }
        
        public decimal Isactive { get; set; }
        
        public decimal LocationId { get; set; }
    }
}
