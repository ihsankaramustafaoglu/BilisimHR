using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Organization
{
    public class OrgDepartmentPkModel : BaseModel<uint>
    {
        
		[Required]
        public decimal FirmId { get; set; }
    }
}
