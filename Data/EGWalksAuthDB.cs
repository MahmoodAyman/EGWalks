using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EGWalks.API.Data {
    public class EGWalksAuthDB : IdentityDbContext {

        public EGWalksAuthDB(DbContextOptions<EGWalksAuthDB> options) : base(options) {
        }

    }
}
