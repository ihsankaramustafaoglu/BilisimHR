using System;
using bilisimHR.Common.Core.Entity;
using System.Collections.Generic;

namespace bilisimHR.DataLayer.Core.Entities.Employees
{
    public class EmpAdress : Entity<int>
    {

        public virtual int AdressTypeId { get; set; }

        public virtual string Adress1 { get; set; }

        public virtual string Adress2 { get; set; }

        public virtual string Locality { get; set; }

        public virtual string PostCode { get; set; }

        public virtual int CityId { get; set; }

        public virtual int CountyId { get; set; }

        public virtual int FirmId { get; set; }

        public virtual DateTime Sdate { get; set; }

        public virtual DateTime Edate { get; set; }

		public virtual EmpEmployeePk EmpEmployeePk { get; set; }
    }
}
