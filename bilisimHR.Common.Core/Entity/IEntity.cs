
using System;

namespace bilisimHR.Common.Core.Entity
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }

        int InsertedBy { get; set; }

        DateTime InsertedDate { get; set; }

        int UpdatedBy { get; set; }

        DateTime UpdatedDate { get; set; }
    }
}
