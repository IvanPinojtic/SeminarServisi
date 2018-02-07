using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeminarMVCServis.GoogleAuth.Data;
using SeminarMVCServis.GoogleAuth.Models;
using SeminarMVCServis.GoogleAuth.Services;
using Newtonsoft.Json;
using SeminarMVCServis.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Routing.Constraints;

namespace SeminarMVCServis.GoogleAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = Configuration["Auth:ValidIssuer"],
                            ValidAudience = Configuration["Auth:ValidAudience"],
                            IssuerSigningKey =
        new SymmetricSecurityKey(
        Encoding.ASCII.GetBytes(Configuration["Auth:JwtSecurityKey"]))
                        };
                    });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;                       // Kod odgovora slati informaciju o kojoj verziji API-a se radi
                options.AssumeDefaultVersionWhenUnspecified = true;     // Ukoliko verzija nije ekplicitno odabrana, korisiti zadanu verziju
                options.DefaultApiVersion = new ApiVersion(1, 0);       // Postavljanje zadane verzije API-a. U ovom slucaju je odabrana zadnja verzija v1.0
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");   // Odabir verzije API-a pomocu header-a
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                })
                .AddJsonOptions(options => {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Auth:Google:client_id"];
                googleOptions.ClientSecret = Configuration["Auth:Google:client_secret"];
            });

            services.AddDbContext<RestaurantsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddScoped<DbContext, RestaurantsContext>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new Info { Title = "JWT API", Version = "v1.0" });   // Dodavanje swagger dokumenta za verziju 1.0
                options.SwaggerDoc("v1.1", new Info { Title = "JWT API", Version = "v1.1" });   // Dodavanje swagger dokumenta za verziju 1.0

                // Ovime se implementira logika za odlucivanje u koji dokument ce ici koja verzija servisa
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var versions = apiDesc.ControllerAttributes()
                                        .OfType<ApiVersionAttribute>()
                                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "JWT API v1.0");
                options.SwaggerEndpoint("/swagger/v1.1/swagger.json", "JWT API v1.1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
            routes.MapRoute(
                name: "IngredientMealsByMealId",
                template: "IngredientMeals/GetIngredientMealsByMealId/{n?}",
                defaults: new { controller = "IngredientMeals", action = "GetIngredientMealByMealId" },
                    constraints:new {n=new IntRouteConstraint()}
                    );

                routes.MapRoute(
                name: "IngredientMealsByIngredientId",
                template: "IngredientMeals/GetIngredientMealsByIngredientId/{n?}",
                defaults: new { controller = "IngredientMeals", action = "GetIngredientMealsByIngredientId" },
                    constraints: new { n = new IntRouteConstraint() }
                    );

                routes.MapRoute(
                    name: "GuestMealsByGuestId",
                    template: "GuestMeals/GetGuestMealsByGuestId/{n?}",
                    defaults: new { controller = "GuestMeals", action = "GetGuestMealByGuestId" },
                        constraints: new { n = new IntRouteConstraint() }
                        );
            });

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<RestaurantsContext>().SeedInitial();
            }
        }
    }
}
