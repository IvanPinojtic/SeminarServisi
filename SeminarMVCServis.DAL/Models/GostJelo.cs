using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class GostJelo
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Gost")]
        public int GostId { get; set; }
        [ForeignKey("Jelo")]
        public int JeloId { get; set; }
        public virtual Gost Gost { get; set; }
        public virtual Jelo Jelo { get; set; }
    }
}
