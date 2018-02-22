using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Auth
{
    public class RoleInPagesModel : BaseModel<int>
    {
        [Required]
        public RolesModelLite Role { get; set; }

        [Required]
        public PagesModelLite Page { get; set; }

        [Required]
        public bool Create { get; set; }

        [Required]
        public bool Read { get; set; }

        [Required]
        public bool Update { get; set; }

        [Required]
        public bool Delete { get; set; }
    }    
}
