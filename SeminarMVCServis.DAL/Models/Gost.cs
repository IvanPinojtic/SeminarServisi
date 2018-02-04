using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class Gost
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Restoran")]
        public int RestoranId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public virtual Restoran Restoran { get; set; }
    }
}
