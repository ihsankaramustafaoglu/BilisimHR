using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.DataLayer.NHibernate.Repositories.Auth
{
    public class ControllerActionsRepository : RepositoryBase<ControllerActions, int>, IControllerActionsRepository
    {
        public ControllerActionsRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }

        public ControllerActions GetByControllerAndActionAsync(string controller, string action)
        {
            return Session.QueryOver<ControllerActions>().Where(k => k.Controller == controller && k.Action == action).SingleOrDefault();
        }
    }
}
