using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Auth
{
    public class PagesModel : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        // public IList<RolesModel> Roles { get; protected set; }

        public IList<RoleInPagesModel> RoleInPages { get; protected set; }

        public IList<ControllerActionsModel> ControllerActions { get; protected set; }
    }

    public class PagesModelLite
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    
    public class NewPagesModel
    {
        [Required]
        public int PageId { get; set; }

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
