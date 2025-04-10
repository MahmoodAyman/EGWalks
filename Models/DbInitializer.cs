using EGWalks.API.Data;
using EGWalks.API.Models.Domain;

namespace EGWalks.API.Models {
    public static class DbInitializer {
        public static void Seed(EGWalksDbContext context) {
            if (context.Regions.Any()) {
                return;
            }
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "SIN",
                    Name = "Sinai",
                    RegionImageUrl = "https://example.com/images/sinai.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "CAI",
                    Name = "Cairo",
                    RegionImageUrl = "https://example.com/images/cairo.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "ALG",
                    Name = "Alexandria",
                    RegionImageUrl = "https://example.com/images/alexandria.jpg"
                }
            };
            context.Regions.AddRange(regions);
            var difficulties = new List<Difficulty>
            {
                new Difficulty
                {
                    Id = Guid.NewGuid(),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.NewGuid(),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.NewGuid(),
                    Name = "Hard"
                }
            };
            context.Difficulties.AddRange(difficulties);
            var walks = new List<Walk>
            {
                new Walk
                {
                    Id = Guid.NewGuid(),
                    Name = "Sinai Coastal Walk",
                    Description = "Enjoy breathtaking coastal views along Sinai's shores.",
                    LengthInKm = 12.5,
                    WalkImageUrl = "https://example.com/images/sinai_coastal.jpg",
                    // Associate with the Sinai region and a Medium difficulty
                    RegionId = regions.First(r => r.Code == "SIN").Id,
                    DifficultyId = difficulties.First(d => d.Name == "Medium").Id
                },
                new Walk
                {
                    Id = Guid.NewGuid(),
                    Name = "Cairo Historical Walk",
                    Description = "Explore Cairo's rich historical heritage.",
                    LengthInKm = 8.3,
                    WalkImageUrl = "https://example.com/images/cairo_history.jpg",
                    RegionId = regions.First(r => r.Code == "CAI").Id,
                    DifficultyId = difficulties.First(d => d.Name == "Easy").Id
                },
                new Walk
                {
                    Id = Guid.NewGuid(),
                    Name = "Alexandria Beach Walk",
                    Description = "Stroll along Alexandria's stunning seaside.",
                    LengthInKm = 15.0,
                    WalkImageUrl = "https://example.com/images/alexandria_beach.jpg",
                    RegionId = regions.First(r => r.Code == "ALG").Id,
                    DifficultyId = difficulties.First(d => d.Name == "Hard").Id
                }
            };
            context.Walks.AddRange(walks);

            context.SaveChanges();
        }
    }
}
