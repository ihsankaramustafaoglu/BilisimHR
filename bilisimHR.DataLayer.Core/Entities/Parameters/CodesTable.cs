using bilisimHR.Common.Core.Entity;

namespace bilisimHR.DataLayer.Core.Entities.Parameters
{
    public class CodesTable : Entity<int>
    {
        public virtual string TableName { get; set; }

        public virtual string Definition { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual int FirmId { get; set; }
    }
}
