using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Employees
{
    public class EmpLanguage : Entity<int>
    {

        public virtual int FirmId { get; set; }

        public virtual int LanguageId { get; set; }

        public virtual int ReadingId { get; set; }

        public virtual int WritingId { get; set; }

        public virtual int SpeakingId { get; set; }

        public virtual int UnderstandingId { get; set; }

        public virtual int LangLevelId { get; set; }

        public virtual int LearningPlaceId { get; set; }

        public virtual DateTime Sdate { get; set; }

        public virtual DateTime Edate { get; set; }

		public virtual EmpEmployeePk EmpEmployeePk { get; set; }
    }
}
