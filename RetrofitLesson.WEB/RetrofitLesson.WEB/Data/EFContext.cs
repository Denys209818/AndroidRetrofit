
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetrofitLesson.WEB.Data.Identity;
using RetrofitLesson.WEB.Data.Identity.Configuration;

namespace RetrofitLesson.WEB.Data
{
    public class EFContext : IdentityDbContext<AppUser, AppRole, long, IdentityUserClaim<long>, AppUserRole, 
        IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public EFContext(DbContextOptions opts) : base(opts)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, 
                new[] { DbLoggerCategory.Database.Name }, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new IdentityConfiguration());
        }
    }
}
