using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class IngredientMeal
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Meal")]
        public int MealId { get; set; }
        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }
        public double Quantity { get; set; }
        public virtual Meal Meal { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
