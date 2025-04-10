using EGWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EGWalks.API.Data {
    public class EGWalksDbContext : DbContext {
        public EGWalksDbContext(DbContextOptions<EGWalksDbContext> options) : base(options) {

        }



        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
