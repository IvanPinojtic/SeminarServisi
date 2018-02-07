using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string ApsUserId { get; set; }
        public string UserName { get; set; }
        public int UserPIN { get; set; }
    }
}
