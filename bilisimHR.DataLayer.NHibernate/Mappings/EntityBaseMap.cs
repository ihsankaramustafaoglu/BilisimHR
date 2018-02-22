using bilisimHR.Common.Core.Entity;
using FluentNHibernate.Mapping;

namespace bilisimHR.DataLayer.NHibernate.Mappings
{
    public abstract class EntityBaseMap<T> : ClassMap<T> where T : IEntity<int>
    {
        protected EntityBaseMap()
        {
            Id(x => x.Id).Column("ID").GeneratedBy.Increment();
            Map(x => x.InsertedBy).Column("INSERTED_BY");
            Map(x => x.InsertedDate).Column("INSERTED_DATE");
            Map(x => x.UpdatedBy).Column("UPDATED_BY");
            Map(x => x.UpdatedDate).Column("UPDATED_DATE");
        }
    }
}
