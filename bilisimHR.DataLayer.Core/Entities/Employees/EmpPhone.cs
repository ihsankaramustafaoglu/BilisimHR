using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Employees
{
    public class EmpPhone : Entity<int>
    {

        public virtual int FirmId { get; set; }

        public virtual int PhoneTypeId { get; set; }

        public virtual string PhoneCode { get; set; }

        public virtual int PhoneNumber { get; set; }

		public virtual EmpEmployeePk EmpEmployeePk { get; set; }
    }
}
