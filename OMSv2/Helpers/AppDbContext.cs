using Microsoft.EntityFrameworkCore;
using OMSv2.Service.Entity;

namespace OMSv2.Service.Helpers
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users"); // Set the table name

                // Define primary key
                entity.HasKey(e => e.UserId);

                // Define other properties
                entity.Property(e => e.UserId).HasColumnName("UserId").ValueGeneratedOnAdd();
                entity.Property(e => e.Username).HasColumnName("Username").HasMaxLength(100).IsRequired();
                entity.Property(e => e.PasswordHash).HasColumnName("PasswordHash").HasMaxLength(64).IsRequired();
                entity.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired();
                entity.Property(e => e.SessionToken).HasColumnName("SessionToken");
                //entity.Property(e => e.LastLogin).HasColumnName("LastLogin").IsRequired();
            });
        }
    }
}
