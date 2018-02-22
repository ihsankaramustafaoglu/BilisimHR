using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Organization
{
    public class OrgDepartmentDetail : Entity<int>
    {

        public virtual int FirmId { get; set; }

        public virtual int DepartmentDetailSegmentId { get; set; }

        public virtual DateTime Sdate { get; set; }

        public virtual DateTime Edate { get; set; }

        public virtual int DepartmentPkId { get; set; }
    }
}
