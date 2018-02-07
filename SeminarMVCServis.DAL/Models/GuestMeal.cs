using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class GuestMeal
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Guest")]
        public int GuestId { get; set; }
        [ForeignKey("Meal")]
        public int MealId { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
