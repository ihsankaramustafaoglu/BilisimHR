using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.Business.Model.Email
{
    public class EmailModel 
    {

        public List<string> Recipient { get; set; }
        //public string Cc { get; set; }
        //public string ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        //public List<byte[]> FileContent { get; set; }
        //public List<string> FileName { get; set; }
        public string DefaultTo { get; set; }
        public Dictionary<string, byte[]> dictionaryMail { get; set; }
    }
}
