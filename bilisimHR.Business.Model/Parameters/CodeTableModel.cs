using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Parameters
{
    public class CodeTableModel : BaseModel<int>
    {
        [Required]
        public string TableName { get; set; }

        [Required]
        public string Definition { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int FirmId { get; set; }
    }
}
