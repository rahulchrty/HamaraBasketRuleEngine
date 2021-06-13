using HamaraBasketRuleEngine.Domain;
using Microsoft.EntityFrameworkCore;

namespace HamaraBasketRuleEngine.Repository
{
    public class HamaraBasketRuleEngineContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<RuleByItem> RuleByItems { get; set; }
        public DbSet<RuleByNumberOfDaysLeft> RuleByNumberOfDaysLefts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=HamaraBasketDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Item>()
                .HasOne(a => a.RuleByItem)
                .WithOne(b => b.Item)
                .HasForeignKey<RuleByItem>(b => b.ItemId)
                .HasConstraintName("FK_RuleByItem_ItemId")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RuleByNumberOfDaysLeft>()
                .HasOne(x => x.RuleByItem)
                .WithMany(y => y.RuleByNumberOfDaysLeft)
                .HasForeignKey(z => z.RuleByItemId)
                .HasConstraintName("FK_RuleByNumberOfDaysLeft_RuleByItemId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
