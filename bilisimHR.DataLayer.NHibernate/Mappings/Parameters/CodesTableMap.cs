using bilisimHR.DataLayer.Core.Entities.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Parameters
{
    public class CodesTableMap : EntityBaseMap<CodesTable>
    {
        //Constructor
        public CodesTableMap()
        {
            Table("P_CODE_TABLE");
            LazyLoad();
            Map(x => x.TableName).Column("TABLE_NAME").Not.Nullable();
            Map(x => x.Definition).Column("DEFITINION").Not.Nullable();
            Map(x => x.IsActive).Column("IS_ACTIVE").Not.Nullable();
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
        }
    }
}
