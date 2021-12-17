using AgilityWeb.Infra.Base;
using AgilityWeb.Infra.Model.Authentication;
using Microsoft.EntityFrameworkCore;

namespace AgilityWeb.Infra.Model.User
{
    public class UserEntity : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public AuthEntity AuthEntity { get; set; }
    }

    public class UserMapping
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UserEntity>();

            entity.ToTable("USER");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Document).HasColumnName("DOCUMENT");
            entity.Property(x => x.FirstName).HasColumnName("FIRST_NAME");
            entity.Property(x => x.LastName).HasColumnName("LAST_NAME");
            entity.Property(x => x.DateInsert).HasColumnName("DATE_INSERT");
            entity.Property(x => x.DateEdition).HasColumnName("DATE_EDITION");
            entity.Property(x => x.Email).HasColumnName("EMAIL");
        }
    }
}