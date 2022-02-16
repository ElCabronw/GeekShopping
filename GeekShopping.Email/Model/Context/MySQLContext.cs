using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Model.Context
{
    public class MySQLContext : DbContext
	{
		public MySQLContext() {}

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

		public DbSet<EmailLog> EmailLogs { get; set; }
		



	}
}

