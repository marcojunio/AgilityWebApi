using Microsoft.EntityFrameworkCore;

namespace AgilityWeb.Infra.Model
{
    public class UserEntity : EntityBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
    }

    public class UserMapping
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UserEntity>();

            entity.ToTable("USER");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Document).HasColumnName("DOCUMENT");
            entity.Property(x => x.Login).HasColumnName("LOGIN");
            entity.Property(x => x.Password).HasColumnName("PASSWORD");
            entity.Property(x => x.Email).HasColumnName("EMAIL");
        }
    }
}
