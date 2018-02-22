using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.Business.Model.Auth
{
    public class SmtpModel : BaseModel<uint>
    {
        [Required]
        public string SmtpHost { get; set; }

        public string UserMail { get; set; }

        [Required]
        public int SmtpPort { get; set; }

        [Required]
        public string UserPw { get; set; }

        [Required]
        public string DefaultTo { get; set; }


    }
}
