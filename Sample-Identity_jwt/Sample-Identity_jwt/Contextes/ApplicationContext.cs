using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sample_Identity_jwt.Models;

namespace Sample_Identity_jwt.Contextes
{
    public class ApplicationContext: IdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions options): base(options)
        {

        }
    }
}
