using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.Common.Core.ErrorHandling
{
    public interface IWebException
    {
        int HataKodu { get; set; }

        string HataAciklama { get; set; }

        string HataDetayAciklama { get; set; }

        HttpStatusCode HttpStatusCode { get; set; }
    }
}
