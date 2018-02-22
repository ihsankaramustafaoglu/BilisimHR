using bilisimHR.Common.Helper.ServiceLocator;
using bilisimHR.Common.Helper.ServiceLocator.RevisionInfo;
using NHibernate.Envers;

namespace bilisimHR.DataLayer.NHibernate.Envers
{
    public class CustomRevInfoListener : IRevisionListener
    {
        private int _userId = 0;
        
        public void NewRevision(object revisionEntity)
        {
            _userId = ServiceLocator.GetService<IRevisionInfoService>().UserId;
            ((CustomRevInfo)revisionEntity).UserId = _userId;
        }
    }
}
