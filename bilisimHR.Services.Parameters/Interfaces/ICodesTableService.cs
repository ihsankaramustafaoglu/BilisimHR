using bilisimHR.Business.Model.Parameters;
using bilisimHR.Common.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bilisimHR.Services.Parameters.Interfaces
{
    public interface ICodesTableService : IService
    {
        void Insert(CodeTableModel codeBase);

        void Update(CodeTableModel codeBase);

        void Delete(int id);

        IList<CodeTableModel> GetAll();
        
        CodeTableModel Get(int id);

        CodeTableModel GetByTableName(string tableName);
    }
}
