using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
