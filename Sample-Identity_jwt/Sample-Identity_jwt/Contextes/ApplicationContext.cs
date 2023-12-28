using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sample_Identity_jwt.Contextes.Configurations;
using Sample_Identity_jwt.Models;

namespace Sample_Identity_jwt.Contextes
{
    public class ApplicationContext: IdentityDbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public ApplicationContext(DbContextOptions options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new PermissionConfiguration().Configure(modelBuilder.Entity<Permission>());
            new RolePermissionConfiguration().Configure(modelBuilder.Entity<RolePermission>());
        }
    }
}
