using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Employees
{
    public class EmpIdentity : Entity<int>
    {

        public virtual int IdentificationNumber { get; set; }

        public virtual string SerialNumber { get; set; }

        public virtual string WalletNumber { get; set; }

        public virtual DateTime BirthDate { get; set; }

        public virtual string BirthLocation { get; set; }

        public virtual int BirthCountryId { get; set; }

        public virtual string FatherName { get; set; }

        public virtual string MotherName { get; set; }

        public virtual int MaritalStatusId { get; set; }

        public virtual int NationalityId { get; set; }

        public virtual int BloodGroupId { get; set; }

        public virtual string Province { get; set; }

        public virtual string District { get; set; }

        public virtual string Neighborhood { get; set; }

        public virtual string BindingNumber { get; set; }

        public virtual string PageNumber { get; set; }

        public virtual string SequenceNumber { get; set; }

        public virtual DateTime DeliveryDate { get; set; }

        public virtual int DeliveryReasonId { get; set; }

        public virtual string DeliveryPopulationManagement { get; set; }

        public virtual string RecordingNumber { get; set; }

        public virtual int FirmId { get; set; }

		public virtual EmpEmployeePk EmpEmployeePk { get; set; }
    }
}
