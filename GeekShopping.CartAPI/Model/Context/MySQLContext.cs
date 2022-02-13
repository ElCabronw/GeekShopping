using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Model.Context
{
    public class MySQLContext : DbContext
	{
		public MySQLContext() {}

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<CartHeader> CartHeaders { get; set; }
		public DbSet<CartDetail> CartDetails { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CartHeader>().Property(m => m.CouponCode).IsRequired(false);
			base.OnModelCreating(modelBuilder);
		}

	}
}

