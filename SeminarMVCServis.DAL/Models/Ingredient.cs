using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}
