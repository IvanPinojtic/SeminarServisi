using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class RestaurantsContext:DbContext
    {
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<IngredientMeal> IngredientMeals { get; set; }
        public virtual DbSet<GuestMeal> GuestMeals { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public RestaurantsContext() { }
        public RestaurantsContext(DbContextOptions<RestaurantsContext> options) : base(options)
        {
        }
    }
}
