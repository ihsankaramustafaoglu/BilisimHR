using bilisimHR.Common.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.DataLayer.Core.Entities.Parameters
{
    public class Codes : Entity<int>
    {
        public virtual CodesTable CodesTable { get; set; }

        public virtual string Code { get; set; }

        public virtual string Definition { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual int CompanyId { get; set; }

        public virtual int OrderNo { get; set; }

        public virtual bool ShowOnSS { get; set; }

        public virtual int FirmId { get; set; }

        public virtual Codes CodeGroup { get; set; }
    }
}
