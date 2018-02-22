using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Organization
{
    public class OrgDepartmentDetailModel : BaseModel<uint>
    {
        
		[Required]
        public decimal FirmId { get; set; }
        
        public decimal DepartmentDetailSegmentId { get; set; }
        
        public DateTime Sdate { get; set; }
        
        public DateTime Edate { get; set; }
        
        public decimal DepartmentPkId { get; set; }
    }
}
