using AgilityWeb.Infra.Model.Authentication;
using AgilityWeb.Infra.Model.User;
using Microsoft.EntityFrameworkCore;

namespace AgilityWeb.Infra.Context
{
    public class AgilityContext : DbContext
    {
        public AgilityContext(DbContextOptions<AgilityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserMapping.Map(modelBuilder);
            AuthMapping.Map(modelBuilder);
        }

        public DbSet<AuthEntity> Auths { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}