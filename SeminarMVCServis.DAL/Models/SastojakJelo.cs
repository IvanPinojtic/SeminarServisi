using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class SastojakJelo
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Jelo")]
        public int JeloId { get; set; }
        [ForeignKey("Sastojak")]
        public int SastojakId { get; set; }
        public float Kolicina { get; set; }
        public virtual Jelo Jelo { get; set; }
        public virtual Sastojak Sastojak { get; set; }
    }
}
