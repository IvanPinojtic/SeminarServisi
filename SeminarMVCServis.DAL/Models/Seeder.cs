using System;
using System.Collections.Generic;
using System.Linq;

namespace SeminarMVCServis.DAL.Models
{
    public static class Seeder
    {
        public static void SeedInitial(this RestaurantsContext restaurantsContext)
        {
            if (!restaurantsContext.Users.Any())
            {
                SeedUsers(restaurantsContext);

                SeedRestaurants(restaurantsContext);

                SeedSastojci(restaurantsContext);

                SeedMeal(restaurantsContext);
            }
        }

        private static void SeedMeal(RestaurantsContext restaurantsContext)
        {
            var g1 = restaurantsContext.Guests.FirstOrDefault(g => g.FirstName.Equals("Joža"));
            var g2 = restaurantsContext.Guests.FirstOrDefault(g => g.FirstName.Equals("Stipe"));
            var g3 = restaurantsContext.Guests.FirstOrDefault(g => g.FirstName.Equals("Bila"));

            var g4 = restaurantsContext.Guests.FirstOrDefault(g => g.FirstName.Equals("Ima"));
            var g5 = restaurantsContext.Guests.FirstOrDefault(g => g.FirstName.Equals("Mehmet"));

            var riba = restaurantsContext.Meals.FirstOrDefault(j => j.Name.Equals("Riba"));
            var grah = restaurantsContext.Meals.FirstOrDefault(j => j.Name.Equals("Grah"));
            var juhaPonistra = restaurantsContext.Meals.FirstOrDefault(j => j.Name.Equals("Juha od ponistre"));
            var burek = restaurantsContext.Meals.FirstOrDefault(j => j.Name.Equals("Burek"));
            var juhaMrkva = restaurantsContext.Meals.FirstOrDefault(j => j.Name.Equals("Juha od mrkve"));

            restaurantsContext.GuestMeals.AddRange(new List<GuestMeal>
            {
                new GuestMeal
                {
                    Guest=g1,
                    Meal=grah
                },
                new GuestMeal
                {
                    Guest=g2,
                    Meal=grah
                },
                new GuestMeal
                {
                    Guest=g2,
                    Meal=burek
                },
                new GuestMeal
                {
                    Guest=g3,
                    Meal=juhaMrkva
                },
                new GuestMeal
                {
                    Guest=g4,
                    Meal=riba
                },
                new GuestMeal
                {
                    Guest=g5,
                    Meal=juhaPonistra
                }
            });

            restaurantsContext.SaveChanges();
        }

        private static void SeedRestaurants(RestaurantsContext restaurantsContext)
        {
            var Restaurant1 = new Restaurant
            {
                Address = new Address
                {
                    City = "Zagreb",
                    Street = "Ilica",
                    Number = "23",
                    Country = "Hrvatska"
                },
                Name = "Kod Bozanića",
                Guests = new List<Guest>
                {
                    new Guest
                    {
                        FirstName="Joža",
                        LastName="Koža"
                    },
                    new Guest
                    {
                        FirstName="Stipe",
                        LastName="Klipe"
                    },
                    new Guest
                    {
                        FirstName="Bila",
                        LastName="Nebila"
                    }
                },
                Meals = new List<Meal>
                {
                    new Meal
                    {
                        Name="Juha od mrkve",
                        Price=66
                    },
                    new Meal
                    {
                        Name="Grah",
                        Price=13
                    },
                    new Meal
                    {
                        Name="Burek",
                        Price=7
                    },
                }
            };

            var Restaurant2 = new Restaurant
            {
                Address = new Address
                {
                    City = "Split",
                    Street = "Zelena",
                    Number = "14",
                    Country = "Hrvatska"
                },
                Name = "Ae2",
                Guests = new List<Guest>
                {
                    new Guest
                    {
                        FirstName="Ima",
                        LastName="Nema"
                    },
                    new Guest
                    {
                        FirstName="Mehmet",
                        LastName="Mujkić"
                    }
                },
                Meals = new List<Meal>
                {
                    new Meal
                    {
                        Name="Juha od ponistre",
                        Price=12
                    },
                    new Meal
                    {
                        Name="Riba",
                        Price=500
                    }
                }
            };

            restaurantsContext.Restaurants.Add(Restaurant1);
            restaurantsContext.Restaurants.Add(Restaurant2);

            restaurantsContext.SaveChanges();

        }

        private static void SeedSastojci(RestaurantsContext restaurantsContext)
        {
            var riba = restaurantsContext.Meals.FirstOrDefault(j => j.Name.Equals("Riba"));
            var grah = restaurantsContext.Meals.FirstOrDefault(j => j.Name.Equals("Grah"));
            var juhaPonistra = restaurantsContext.Meals.FirstOrDefault(j => j.Name.Equals("Juha od ponistre"));
            var burek= restaurantsContext.Meals.FirstOrDefault(j => j.Name.Equals("Burek"));
            var juhaMrkva= restaurantsContext.Meals.FirstOrDefault(j => j.Name.Equals("Juha od mrkve"));

            var sol = new Ingredient
            {
                Name = "Sol",
                Unit = "mg"
            };
            var srdela = new Ingredient
            {
                Name = "Srdela",
                Unit = "kg"
            };
            var voda = new Ingredient
            {
                Name = "Voda",
                Unit = "L"
            };
            var ponistra = new Ingredient
            {
                Name = "Ponistra",
                Unit = "prstohvat"
            };
            var mahune = new Ingredient
            {
                Name = "Mahune",
                Unit = "komad"
            };
            var sir = new Ingredient
            {
                Name = "Sir",
                Unit = "kg"
            };
            var mrkva = new Ingredient
            {
                Name = "Mrkva",
                Unit = "kg"
            };

            /*    RestaurantsContext.Sastojak.Add(sol);
                RestaurantsContext.Sastojak.Add(srdela);
                RestaurantsContext.Sastojak.Add(voda);

                RestaurantsContext.SaveChanges();*/

            restaurantsContext.IngredientMeals.AddRange(new List<IngredientMeal>
            {
                new IngredientMeal
                {
                    Meal=riba,
                    Ingredient=sol,
                    Quantity=20
                },
                new IngredientMeal
                {
                    Meal=riba,
                    Ingredient=srdela,
                    Quantity=1
                },
                new IngredientMeal
                {
                    Meal=riba,
                    Ingredient=voda,
                    Quantity=60
                }
                ,new IngredientMeal
                {
                    Meal=grah,
                    Ingredient=sol,
                    Quantity=7
                },
                new IngredientMeal
                {
                    Meal=grah,
                    Ingredient=mahune,
                    Quantity=2
                },
                new IngredientMeal
                {
                    Meal=grah,
                    Ingredient=voda,
                    Quantity=1
                },
                new IngredientMeal
                {
                    Meal=juhaPonistra,
                    Ingredient=ponistra,
                    Quantity=12
                },
                new IngredientMeal
                {
                    Meal=burek,
                    Ingredient=sir,
                    Quantity=50
                },
                new IngredientMeal
                {
                    Meal=juhaMrkva,
                    Ingredient=mrkva,
                    Quantity=88
                },
                new IngredientMeal
                {
                    Meal=juhaMrkva,
                    Ingredient=voda,
                    Quantity=2
                }
            });

            restaurantsContext.SaveChanges();
        }

        private static void SeedUsers(RestaurantsContext restaurantsContext)
        {
            var users = new List<User>
            {
                new User
                {
                    UserName = "user1",
                    UserPIN = 1111,
                    ApsUserId = "jedan"
                },
                new User
                {
                    UserName = "user2",
                    UserPIN = 2222,
                    ApsUserId = "dva"
                },
                new User
                {
                    UserName = "user3",
                    UserPIN = 3333,
                    ApsUserId = "tri"
                },
                new User
                {
                    UserName = "user4",
                    UserPIN = 4444,
                    ApsUserId = "cetiri"
                }
            };

            restaurantsContext.Users.AddRange(users);

            restaurantsContext.SaveChanges();
        }
    }
}