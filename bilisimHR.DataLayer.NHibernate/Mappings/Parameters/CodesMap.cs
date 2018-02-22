using bilisimHR.DataLayer.Core.Entities.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Parameters
{
    public class CodesMap : EntityBaseMap<Codes>
    {
        //Constructor
        public CodesMap()
        {
            Table("P_CODES");
            LazyLoad();
            References(x => x.CodesTable).Column("CODE_TABLE_ID").Not.Nullable();
            Map(x => x.Code).Column("CODE").Not.Nullable();
            Map(x => x.Definition).Column("DEFINITION").Not.Nullable();
            References(x => x.CodeGroup).Column("CODE_GROUP").Not.Nullable();
            Map(x => x.IsActive).Column("IS_ACTIVE").Not.Nullable();
            Map(x => x.CompanyId).Column("COMPANY_ID").Not.Nullable();
            Map(x => x.OrderNo).Column("ORDER_NO").Nullable();
            Map(x => x.ShowOnSS).Column("SHOW_ON_SS").Not.Nullable();
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
        }
    }
}
