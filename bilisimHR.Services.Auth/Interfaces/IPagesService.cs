using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bilisimHR.Services.Auth.Interfaces
{
    public interface IPagesService : IService
    {
        Task<object> InsertAsync(PagesModel model);

        Task UpdateAsync(PagesModel model);

        Task DeleteAsync(int id);

        Task<IList<PagesModel>> GetAllAsync();

        Task<IList<PagesModelLite>> GetAllLiteAsync();

        Task<PagesModel> GetAsync(int id);

        Task<PagesModel> GetBy(Dictionary<string, string> whereParameters);

        IQueryable<PagesModel> SelectBy(Expression<Func<PagesModel, bool>> expression);
        
        Task InsertControllerActionsAsync(int id, IList<int> controllerAcitons);

        Task DeleteControllerActionsAsync(int id, IList<int> controllerAcitons);
    }
}
