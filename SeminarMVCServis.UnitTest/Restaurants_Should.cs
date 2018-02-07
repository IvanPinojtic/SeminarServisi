using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarMVCServis.DAL.Models;
using SeminarMVCServis.GoogleAuth.Controllers;
using System;
using Xunit;

namespace SeminarMVCServis.UnitTest
{
    public class Restaurants_Should
    {
        DbContextOptions<RestaurantsContext> _dbContextOptions;

        public Restaurants_Should()
        {
            _dbContextOptions = new DbContextOptionsBuilder<RestaurantsContext>()
                            .UseInMemoryDatabase(databaseName: "Test_database")
                            .Options;
        }

        [Fact]
        public async void PostRestaurant()
        {
            // Koristenje InMemory baze podataka (context)
            using (var context = new RestaurantsContext(_dbContextOptions))
            {
                var restaurantsAPI = new RestaurantsController(context);
                for (int i = 0; i < 10; ++i)
                {
                    var tmpRestaurant = new Restaurant();
                    tmpRestaurant.Name = $"Ime { i + 1 }";
                    tmpRestaurant.Address = new Address
                    {
                        City = "Zagreb",
                        Street = "Ilica",
                        Country = "Hrvatska",
                        Number = $"{i + 1}",
                    };
                    var result = await restaurantsAPI.PostRestaurant(tmpRestaurant);
                    var badRequest = result as BadRequestObjectResult;

                    Assert.Null(badRequest);    // Ako API ne vraca BadRequest, to znaci da je poziv uspjesan
                }
            }
        }

        [Fact]
        public async void GetRestaurant()
        {
            using (var context = new RestaurantsContext(_dbContextOptions))
            {
                var restaurantsAPI = new RestaurantsController(context);
                for (int i = 0; i < 10; ++i)
                {
                    var tmpRestaurant = new Restaurant();
                    tmpRestaurant.Name = $"Ime { i + 1 }";
                    tmpRestaurant.Address = new Address
                    {
                        City = "Zagreb",
                        Street = "Ilica",
                        Country = "Hrvatska",
                        Number = $"{i + 1}",
                    };
                    restaurantsAPI.PostRestaurant(tmpRestaurant).Wait();
                }
            }

            using (var context = new RestaurantsContext(_dbContextOptions))
            {
                var restaurantsAPI = new RestaurantsController(context);
                var result = await restaurantsAPI.GetRestaurant(5);
                var okResult = result as OkObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);

                var restaurant = okResult.Value as Restaurant;
                Assert.NotNull(restaurant);
                Assert.Equal("Ime 5", restaurant.Name);
            }
        }
    }
}