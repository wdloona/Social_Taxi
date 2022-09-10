using Social_Taxi.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Social_Taxi.EntityFramework
{
    public class TaxiDbContext : DbContext
    {
        public static readonly ILoggerFactory factory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=goldspa.beget.tech;port=3306;database=goldspa_taxi;" +
                "user=goldspa_taxi;password=2aXvA%Zj[eqrf;Persist Security Info=False;Connect Timeout=1500",
                MySqlServerVersion.LatestSupportedServerVersion);

            optionsBuilder.UseLoggerFactory(factory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(TaxiDbContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
