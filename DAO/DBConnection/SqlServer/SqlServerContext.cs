using Microsoft.EntityFrameworkCore;
using Models.Customer.Entities;

namespace DAO.DBConnection.SqlServer
{
    public class SqlServerContext : DbContext
    {
        private readonly string _conn;

        public SqlServerContext(string conn) => _conn = conn;

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(_conn);

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("LastUpdate") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("LastUpdate").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("LastUpdate").CurrentValue = DateTime.Now;
            }

            try
            {
                return base.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
