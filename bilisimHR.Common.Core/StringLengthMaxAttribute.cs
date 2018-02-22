using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.Common.Core
{
    public class StringLengthMaxAttribute : StringLengthAttribute
    {
        public StringLengthMaxAttribute() 
            : base(10000)
        {
            // 10000 is an arbitrary number large enough to be in the nvarchar(max) range 
        }
    }
}
