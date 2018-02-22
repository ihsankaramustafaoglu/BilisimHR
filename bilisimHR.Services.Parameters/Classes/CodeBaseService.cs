using System;
using System.Collections.Generic;
using bilisimHR.Business.Model.Parameters;
using bilisimHR.Services.Parameters.Interfaces;
using bilisimHR.DataLayer.Core.Repositories.Parameters;
using bilisimHR.DataLayer.Core.Entities.Parameters;

namespace bilisimHR.Services.Parameters.Classes
{
    public class CodeBaseService : ICodesTableService
    {
        private readonly ICodesTableRepository _codeBaseRepository;

        public CodeBaseService(ICodesTableRepository codeBaseRepository)
        {
            _codeBaseRepository = codeBaseRepository;
        }

        public void Insert(CodeTableModel codeBase)
        {
            if (codeBase == null)
                throw new ArgumentNullException("CodeBase");

            _codeBaseRepository.Insert(new CodesTable
            {
                TableName = codeBase.TableName,
                Definition = codeBase.Definition,
                IsActive = codeBase.IsActive,
                FirmId = codeBase.FirmId,
                InsertedBy = codeBase.InsertedBy,
                InsertedDate = codeBase.InsertedDate,
                UpdatedBy = codeBase.UpdatedBy,
                UpdatedDate = codeBase.UpdatedDate
            });
        }

        public void Update(CodeTableModel codeBase)
        {
            if (codeBase == null)
                throw new ArgumentNullException("CodeBase");

            throw new NotImplementedException("Update CodeBase");
        }

        public void Delete(int id)
        {
            _codeBaseRepository.Delete(id);
        }

        public IList<CodeTableModel> GetAll()

        {
            var dal = _codeBaseRepository.GetAll();

            if (dal == null)
                return new List<CodeTableModel>();
            else
            {
                List<CodeTableModel> list = new List<CodeTableModel>();
                foreach (CodesTable obj in dal)
                {
                    list.Add(new CodeTableModel
                    {
                        Id = obj.Id,
                        TableName = obj.TableName,
                        Definition = obj.Definition,
                        IsActive = obj.IsActive,
                        FirmId = obj.FirmId,
                        InsertedBy = obj.InsertedBy,
                        InsertedDate = obj.InsertedDate,
                        UpdatedBy = obj.UpdatedBy,
                        UpdatedDate = obj.UpdatedDate
                    });
                }

                return list;
            }
        }

        public CodeTableModel Get(int id)
        {
            var dal = _codeBaseRepository.Get(id);

            if (dal == null)
                return null;
            else
                return new CodeTableModel()
                {
                    Id = dal.Id,
                    TableName = dal.TableName,
                    Definition = dal.Definition,
                    IsActive = dal.IsActive,
                    FirmId = dal.FirmId,
                    InsertedBy = dal.InsertedBy,
                    InsertedDate = dal.InsertedDate,
                    UpdatedBy = dal.UpdatedBy,
                    UpdatedDate = dal.UpdatedDate
                };
        }

        public CodeTableModel GetByTableName(string tableName)
        {
            var dal = _codeBaseRepository.GetByTableName(tableName);

            if (dal == null)
                return null;
            else
                return new CodeTableModel()
                {
                    Id = dal.Id,
                    TableName = dal.TableName,
                    Definition = dal.Definition,
                    IsActive = dal.IsActive,
                    FirmId = dal.FirmId,
                    InsertedBy = dal.InsertedBy,
                    InsertedDate = dal.InsertedDate,
                    UpdatedBy = dal.UpdatedBy,
                    UpdatedDate = dal.UpdatedDate
                };
        }
    }
}
