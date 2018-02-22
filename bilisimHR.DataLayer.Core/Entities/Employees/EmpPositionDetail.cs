using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Employees
{
    public class EmpPositionDetail : Entity<int>
    {

        public virtual int DepartmentId { get; set; }

        public virtual DateTime Sdate { get; set; }

        public virtual DateTime Edate { get; set; }

        public virtual int PositionChangeTypeId { get; set; }

        public virtual int FirmId { get; set; }

        public virtual int PositionId { get; set; }

        public virtual int TitleId { get; set; }

        public virtual int WorkplaceId { get; set; }

        public virtual int CostCenterId { get; set; }

        public virtual int CompanyId { get; set; }

		public virtual EmpEmployeePk EmpEmployeePk { get; set; }
    }
}
