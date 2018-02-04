using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class Sastojak
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Jedinica { get; set; }
    }
}
