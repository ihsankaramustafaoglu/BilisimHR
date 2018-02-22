using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.Common.Helper.ServiceLocator
{
    public class HttpRequestMessageService
    {
        private Dictionary<string, HttpRequestMessage> _httpRequestMessageDict;

        public HttpRequestMessageService()
        {
            _httpRequestMessageDict = new Dictionary<string, HttpRequestMessage>();
        }
        
        public Dictionary<string, HttpRequestMessage> HttpRequestMessageDict
        {
            get { return _httpRequestMessageDict; }
            set { _httpRequestMessageDict = value; }
        }
    }
}
