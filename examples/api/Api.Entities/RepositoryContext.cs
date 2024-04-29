using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Api.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Person>? Persons { get; set; }

        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] { new NLogLoggerProvider() });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }
    }
}
