using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class Adresa
    {
        [Key]
        public int Id { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string Mjesto { get; set; }
        public string Drzava { get; set; }
    }
}
