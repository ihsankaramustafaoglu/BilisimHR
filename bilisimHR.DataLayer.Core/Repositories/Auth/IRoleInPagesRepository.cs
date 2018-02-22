using bilisimHR.Common.Core;
using bilisimHR.DataLayer.Core.Entities.Auth;
using System.Linq;

namespace bilisimHR.DataLayer.Core.Repositories.Auth
{
    public interface IRoleInPagesRepository : IRepository<RoleInPages, int>
    {
        IQueryable<RoleInPages> GetByRoleIdAsync(int roleId);
    }
}
