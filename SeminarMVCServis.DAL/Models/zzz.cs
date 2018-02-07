using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<RestaurantsContext>
    {
        public RestaurantsContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<RestaurantsContext>();

            var connectionString = "Data Source=DESKTOP-V4F7KIQ;Initial Catalog=SeminarDb2;Integrated Security=True";

            builder.UseSqlServer(connectionString);

            // Stop client query evaluation
            builder.ConfigureWarnings(w =>
                w.Throw(RelationalEventId.QueryClientEvaluationWarning));

            return new RestaurantsContext(builder.Options);
        }
    }
}
