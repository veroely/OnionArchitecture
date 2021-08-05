using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Core.Domain.Entities;
using OnionArchitecture.Core.Domain.Entities.Auth;

namespace OnionArchitecture.Infrastructure.Repositories
{
    public class OnionArchitectureContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<User> Users { get; set; }
        public OnionArchitectureContext() : base() { }
        public OnionArchitectureContext(DbContextOptions<OnionArchitectureContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(@"Server=.;Database=OnionDB;Trusted_Connection=True;");
        }
    }
}
