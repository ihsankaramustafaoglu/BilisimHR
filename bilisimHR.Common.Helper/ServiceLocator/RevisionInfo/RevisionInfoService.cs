
namespace bilisimHR.Common.Helper.ServiceLocator.RevisionInfo
{
    public class RevisionInfoService : IRevisionInfoService
    {
        private int _userId;
        
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
    }
}
