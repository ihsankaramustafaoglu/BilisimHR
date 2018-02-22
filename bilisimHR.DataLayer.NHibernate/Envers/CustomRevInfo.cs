using FluentNHibernate.Mapping;
using NHibernate.Envers;

namespace bilisimHR.DataLayer.NHibernate.Envers
{
    public class CustomRevInfo : DefaultRevisionEntity
    {
        public virtual int UserId { get; set; }
    }

    public class CustomRevInfoMap : ClassMap<CustomRevInfo>
    {
        //Constructor
        public CustomRevInfoMap()
        {
            Table("REVINFO");
            LazyLoad();
            Id(x => x.Id).Column("REV").GeneratedBy.Increment();
            Map(x => x.RevisionDate).Column("REVTSTMP").Not.Nullable();
            Map(x => x.UserId).Column("USER_ID").Nullable();
        }
    }
}
