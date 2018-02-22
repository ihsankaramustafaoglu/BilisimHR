using System;

namespace bilisimHR.Common.Core.Entity
{
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }

        public virtual int InsertedBy { get; set; }

        public virtual DateTime InsertedDate { get; set; }

        public virtual int UpdatedBy { get; set; }

        public virtual DateTime UpdatedDate { get; set; }
    }
}
