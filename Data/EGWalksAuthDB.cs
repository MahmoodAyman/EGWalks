using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EGWalks.API.Data {
    public class EGWalksAuthDB : IdentityDbContext {

        public EGWalksAuthDB(DbContextOptions<EGWalksAuthDB> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            var ReaderRoleId = "c8d63d18-15a1-4634-94ed-0fdd6f137f9c";
            var AdminRoleId = "dfed8544-61d5-4bd9-8d49-66130264afde";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id =ReaderRoleId,
                    ConcurrencyStamp = ReaderRoleId,
                    Name="Reader",
                    NormalizedName = "Reader".ToUpper()

                },
                new IdentityRole
                {
                    Id = AdminRoleId,
                    ConcurrencyStamp = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
