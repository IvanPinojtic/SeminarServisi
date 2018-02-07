using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
