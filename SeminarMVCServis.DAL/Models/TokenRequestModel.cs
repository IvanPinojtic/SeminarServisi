using System;
using System.Collections.Generic;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class TokenRequestModel
    {
        public string UserName { get; set; }
        public int UserPIN { get; set; }
        public string UserStringId { get; set; }
    }
}
