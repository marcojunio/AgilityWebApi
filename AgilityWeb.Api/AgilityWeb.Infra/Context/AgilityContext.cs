
using AgilityWeb.Infra.Model;
using Microsoft.EntityFrameworkCore;

namespace AgilityWeb.Infra.Context
{
    public class AgilityContext : DbContext
    {
        public AgilityContext(DbContextOptions<AgilityContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserMapping.Map(modelBuilder);
        }

        private DbSet<UserEntity> Users { get; set; }
    }
}
