using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Organization
{
    public class OrgDepartmentDetailSegment : Entity<int>
    {

        public virtual int FirmId { get; set; }

        public virtual string Definition { get; set; }

        public virtual int CompanyId { get; set; }

        public virtual int DepartmentDetailId { get; set; }

        public virtual int OrderNo { get; set; }
    }
}
