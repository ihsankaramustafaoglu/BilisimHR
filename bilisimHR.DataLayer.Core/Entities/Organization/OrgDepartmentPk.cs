using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Organization
{
    public class OrgDepartmentPk : Entity<int>
    {

        public virtual int FirmId { get; set; }
    }
}
