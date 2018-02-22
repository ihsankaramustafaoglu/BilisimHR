using System;
using System.Net;

namespace bilisimHR.Common.Core.ErrorHandling
{
    public class WebException : Exception, IWebException
    {
        public int HataKodu { get; set; }

        public string HataAciklama { get; set; }

        public string HataDetayAciklama { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

    }
}
