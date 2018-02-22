using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Organization
{
    public class OrgDepartmentDetailSegmentModel : BaseModel<uint>
    {
        
		[Required]
        public decimal FirmId { get; set; }
        
        public string Definition { get; set; }
        
        public decimal CompanyId { get; set; }
        
        public decimal DepartmentDetailId { get; set; }
        
        public decimal OrderNo { get; set; }
    }
}
