using bilisimHR.Common.Core;
using bilisimHR.DataLayer.Core.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.DataLayer.Core.Repositories.Auth
{
    public interface IControllerActionsRepository : IRepository<ControllerActions, int>
    {
        ControllerActions GetByControllerAndActionAsync(string controller, string action);
    }
}
