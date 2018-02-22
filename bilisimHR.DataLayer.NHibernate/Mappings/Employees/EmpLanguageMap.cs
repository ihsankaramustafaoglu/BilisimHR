using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Employees;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Employees
{
    public class EmpLanguageMap: EntityBaseMap<EmpLanguage>
    {
        public EmpLanguageMap()
        {
            Table("EMP_LANGUAGE");
            LazyLoad();
            
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
            Map(x => x.LanguageId).Column("LANGUAGE_ID").Nullable();
            Map(x => x.ReadingId).Column("READING_ID").Nullable();
            Map(x => x.WritingId).Column("WRITING_ID").Nullable();
            Map(x => x.SpeakingId).Column("SPEAKING_ID").Nullable();
            Map(x => x.UnderstandingId).Column("UNDERSTANDING_ID").Nullable();
            Map(x => x.LangLevelId).Column("LANG_LEVEL_ID").Nullable();
            Map(x => x.LearningPlaceId).Column("LEARNING_PLACE_ID").Nullable();
            Map(x => x.Sdate).Column("SDATE").Nullable();
            Map(x => x.Edate).Column("EDATE").Nullable();
			References(x => x.EmpEmployeePk).Column("EMPLOYEE_PK_ID");
        }
    }
}
