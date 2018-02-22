using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Employees
{
    public class EmpLanguageModel : BaseModel<uint>
    {
        
		[Required]
        public decimal FirmId { get; set; }
        
        public decimal EmployeePkId { get; set; }
        
        public decimal LanguageId { get; set; }
        
        public decimal ReadingId { get; set; }
        
        public decimal WritingId { get; set; }
        
        public decimal SpeakingId { get; set; }
        
        public decimal UnderstandingId { get; set; }
        
        public decimal LangLevelId { get; set; }
        
        public decimal LearningPlaceId { get; set; }
        
        public DateTime Sdate { get; set; }
        
        public DateTime Edate { get; set; }

		public EmpEmployeePkModel EmpEmployeePk { get; }
    }
}
