using System;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model
{
    public class BaseModel<TPrimaryKey>
    {
        [Key]
        public TPrimaryKey Id { get; set; }

        public int InsertedBy { get; set; }

        public DateTime InsertedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
