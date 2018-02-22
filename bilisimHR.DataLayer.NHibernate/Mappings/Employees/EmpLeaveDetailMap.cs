using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Employees;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Employees
{
    public class EmpLeaveDetailMap: EntityBaseMap<EmpLeaveDetail>
    {
        public EmpLeaveDetailMap()
        {
            Table("EMP_LEAVE_DETAIL");
            LazyLoad();
            
            Map(x => x.FirmId).Column("FIRM_ID").Not.Nullable();
            Map(x => x.EmployeePkId).Column("EMPLOYEE_PK_ID").Nullable();
            Map(x => x.PhoneTypeId).Column("PHONE_TYPE_ID").Nullable();
            Map(x => x.PhoneCode).Column("PHONE_CODE").Nullable();
            Map(x => x.PhoneNumber).Column("PHONE_NUMBER").Nullable();
            Map(x => x.LeavingReasonId).Column("LEAVING_REASON_ID").Nullable();
            Map(x => x.LeavingReasonSpecialId).Column("LEAVING_REASON_SPECIAL_ID").Nullable();
            Map(x => x.ResignationId).Column("RESIGNATION_ID").Nullable();
            Map(x => x.LeavingRequestById).Column("LEAVING_REQUEST_BY_ID").Nullable();
            Map(x => x.LeavingOfferId).Column("LEAVING_OFFER_ID").Nullable();
            Map(x => x.LeavingModelId).Column("LEAVING_MODEL_ID").Nullable();
            Map(x => x.LeaveToParticipationId).Column("LEAVE_TO_PARTICIPATION_ID").Nullable();
            Map(x => x.Explanation).Column("EXPLANATİON").Nullable();
        }
    }
}
