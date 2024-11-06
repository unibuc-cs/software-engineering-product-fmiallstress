using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using test_binance_api.Models;

namespace test_binance_api.Data
{
    public class BinanceContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<History> Histories { get; set; }

        public BinanceContext(DbContextOptions<BinanceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //one to one
            modelBuilder.Entity<User>()
                .HasOne(h => h.History)
                .WithOne(u => u.User)
                .HasForeignKey<History>(fk => fk.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(w => w.Wallet)
                .WithOne(u => u.User)
                .HasForeignKey<Wallet>(fk => fk.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            //one to many
            modelBuilder.Entity<History>()
                .HasMany(c => c.Transactions)
                .WithOne(h => h.History)
                .HasForeignKey(a => a.IdHistory)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
