using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class Restoran
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Adresa")]
        public int AdresaId { get; set; }
        public string Naziv { get; set; }
        public virtual Adresa Adresa { get; set; }
        public virtual ICollection<Gost> Gosti { get; set; }
        public virtual ICollection<Jelo> Menu { get; set; }
    }
}
