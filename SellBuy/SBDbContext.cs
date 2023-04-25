using Microsoft.EntityFrameworkCore;
using SellBuy.Entities;

namespace SellBuy
{
    public class SBDbContext : DbContext
    {
        public SBDbContext(DbContextOptions<SBDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubSection> SubSections { get; set; }
        public DbSet<CustomizedSetting> CustomizedSettings { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {

                b
                   .HasIndex(p => new { p.Email })
                   .IsUnique();

                b
                    .HasData(new[]
                    {
                        new User
                        {
                            Id = 1,
                            Email = "it-admin@sellBuy.com",
                            PasswordHash = "Test123!",
                            RegisteredAt = DateTime.UtcNow,
                            Username = "it-admin",
                            Role = UserRole.Admin,
                            Status = UserStatus.Active
                        }
                    });
            });            
        }
    }
}
