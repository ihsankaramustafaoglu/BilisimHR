
using bilisimHR.Common.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Auth
{
    public class ControllerActionsModel : BaseModel<int>
    {
        [Required]
        public string Controller { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string Description { get; set; }

        public IList<PagesModel> Pages { get; protected set; }

        public OperationType OperationType { get; set; }
    }
}
