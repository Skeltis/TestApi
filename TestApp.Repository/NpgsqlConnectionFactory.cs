using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace TestApp.Data
{
    public class NpgsqlConnectionFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public NpgsqlConnectionFactory()
        {

        }

        public MainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Userid=postgres;Password=123;Pooling=true;MinPoolSize=1;MaxPoolSize=10;ConnectionLifeTime=15;");

            return new MainDbContext(optionsBuilder.Options);
        }
    }
}
