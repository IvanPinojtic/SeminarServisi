using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class Jelo
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public float Cijena { get; set; }
    }
}
