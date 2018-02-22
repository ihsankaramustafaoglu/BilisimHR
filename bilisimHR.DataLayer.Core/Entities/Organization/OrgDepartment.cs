using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Organization
{
    public class OrgDepartment : Entity<int>
    {

        public virtual int FirmId { get; set; }

        public virtual string Company { get; set; }

        public virtual DateTime Sdate { get; set; }

        public virtual DateTime Edate { get; set; }

        public virtual string Definition { get; set; }

        public virtual string OtherDefinition { get; set; }

        public virtual int CostCenterId { get; set; }

        public virtual int Isactive { get; set; }

        public virtual int LocationId { get; set; }
    }
}
