using bilisimHR.Business.Model.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Employees
{
    public class EmpEmployeePkModel : BaseModel<uint>
    {
        
		[Required]
        public decimal FirmId { get; set; }

		public virtual IList<UsersModel> AuthUsers { get; set; }

		public virtual IList<EmpPositionDetailModel> EmpPositionDetail { get; set; }

		public virtual IList<EmpPhoneModel> EmpPhone { get; set; }

		public virtual IList<EmpLanguageModel> EmpLanguage { get; set; }

		public virtual IList<EmpIdentityModel> EmpIdentity { get; set; }

		public virtual IList<EmpEducationModel> EmpEducation { get; set; }

		public virtual IList<EmpAdressModel> EmpAdress { get; set; }

		public virtual IList<EmpEmployeeModel> EmpEmployee { get; set; }
    }
}
