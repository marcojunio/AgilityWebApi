using AgilityWeb.Infra.Base;
using AgilityWeb.Infra.Model.User;
using Microsoft.EntityFrameworkCore;

namespace AgilityWeb.Infra.Model.Authentication
{
    public class AuthEntity : EntityBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int? CodeVerification { get; set; }
        public string IdUser { get; set; }
        public UserEntity UserEntity { get; set; }
    }

    public class AuthMapping
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<AuthEntity>();

            entity.ToTable("AUTH_INFOS");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Login).HasColumnName("LOGIN");
            entity.Property(x => x.Password).HasColumnName("PASSWORD");
            entity.Property(x => x.CodeVerification).HasColumnName("CODE_VERIFICATION");
            entity.Property(x => x.DateInsert).HasColumnName("DATE_INSERT");
            entity.Property(x => x.DateEdition).HasColumnName("DATE_EDITION");

            entity.HasOne(x => x.UserEntity)
                .WithOne(x => x.AuthEntity)
                .HasForeignKey<AuthEntity>(x => x.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}