
using System.Collections.Generic;

namespace bilisimHR.Business.Model.Auth
{
    public class RolesModel : BaseModel<int>
    {
        public string Name { get; set; }

        public IList<UsersModel> Users { get; set; }

        // public IList<PagesModel> Pages { get; set; }

        public IList<RoleInPagesModel> RoleInPages { get; protected set; }
    }

    public class RolesModelLite
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
