using Microsoft.EntityFrameworkCore;
using UserManagementApp.Domain.Entities;

namespace UserManagementApp.Infrastructure.DatabaseContext
{
    public class UserManagementAppDbContext : DbContext
    {
        public UserManagementAppDbContext(DbContextOptions<UserManagementAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementAppDbContext).Assembly);
        }

        public DbSet<User> Users { get; private set; }
    }
}