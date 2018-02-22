using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Employees
{
    public class EmpEducation : Entity<int>
    {

        public virtual int FirmId { get; set; }

        public virtual int EducationStatusId { get; set; }

        public virtual DateTime Sdate { get; set; }

        public virtual DateTime Edate { get; set; }

        public virtual string InstitutionName { get; set; }

        public virtual string FacultyName { get; set; }

        public virtual string StudyFieldName { get; set; }

        public virtual int InstitutionId { get; set; }

        public virtual int FacultyId { get; set; }

        public virtual int StudyFieldId { get; set; }

        public virtual int DurationYear { get; set; }

        public virtual int GraduateStatusId { get; set; }

		public virtual EmpEmployeePk EmpEmployeePk { get; set; }
    }
}
