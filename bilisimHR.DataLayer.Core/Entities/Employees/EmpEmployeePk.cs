using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;
using bilisimHR.DataLayer.Core.Entities.Auth;

namespace bilisimHR.DataLayer.Core.Entities.Employees
{
    public class EmpEmployeePk : Entity<int>
    {

        public virtual int FirmId { get; set; }

		public virtual IList<Users> AuthUsersModel { get; set; }

		public virtual IList<EmpPositionDetail> EmpPositionDetail { get; set; }

		public virtual IList<EmpPhone> EmpPhone { get; set; }

		public virtual IList<EmpLanguage> EmpLanguage { get; set; }

		public virtual IList<EmpIdentity> EmpIdentity { get; set; }

		public virtual IList<EmpEducation> EmpEducation { get; set; }

		public virtual IList<EmpEmployee> EmpEmployee { get; set; }

		public virtual IList<EmpAdress> EmpAdress { get; set; }
    }
}
