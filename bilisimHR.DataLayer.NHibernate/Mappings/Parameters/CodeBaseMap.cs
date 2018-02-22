using bilisimHR.DataLayer.Core.Entities.Parameters;
using FluentNHibernate.Mapping;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Parameters
{
    public class CodeBaseMap : ClassMap<CodeBase>
    {
        //Constructor
        public CodeBaseMap()
        {
            Table("CODE_BASE");
            LazyLoad();
            Id(x => x.Id).Column("ID").GeneratedBy.Increment();
            Map(x => x.TableName).Column("TABLE_NAME").Not.Nullable();
            Map(x => x.CreatedBy).Column("CREATED_BY").Not.Nullable();
            Map(x => x.CreatedDate).Column("CREATED_DATE").Not.Nullable();
            Map(x => x.UpdatedBy).Column("UPDATED_BY").Not.Nullable();
            Map(x => x.UpdatedDate).Column("UPDATED_DATE").Not.Nullable();
        }
    }
}
