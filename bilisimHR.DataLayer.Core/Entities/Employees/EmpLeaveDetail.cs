using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Employees
{
    public class EmpLeaveDetail : Entity<int>
    {

        public virtual int FirmId { get; set; }

        public virtual int EmployeePkId { get; set; }

        public virtual int PhoneTypeId { get; set; }

        public virtual string PhoneCode { get; set; }

        public virtual int PhoneNumber { get; set; }

        public virtual int LeavingReasonId { get; set; }

        public virtual int LeavingReasonSpecialId { get; set; }

        public virtual int ResignationId { get; set; }

        public virtual int LeavingRequestById { get; set; }

        public virtual int LeavingOfferId { get; set; }

        public virtual int LeavingModelId { get; set; }

        public virtual int LeaveToParticipationId { get; set; }

        public virtual string Explanation { get; set; }
    }
}
