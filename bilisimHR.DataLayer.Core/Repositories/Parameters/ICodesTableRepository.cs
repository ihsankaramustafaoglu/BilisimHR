using bilisimHR.Common.Core;
using bilisimHR.DataLayer.Core.Entities.Parameters;

namespace bilisimHR.DataLayer.Core.Repositories.Parameters
{
    public interface ICodesTableRepository : IRepository<CodesTable, int>
    {
        CodesTable GetByTableName(string tableName);
    }
}
