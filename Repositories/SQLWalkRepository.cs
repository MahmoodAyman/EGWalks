using EGWalks.API.Data;
using EGWalks.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGWalks.API.Repositories {
    public class SQLWalkRepository : IWalkRepository {
        private readonly EGWalksDbContext _context;

        public SQLWalkRepository(EGWalksDbContext context) {
            _context = context;

        }
        public async Task<List<Walk>> GetAllWalks(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000) {
            var walks = _context.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering:
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery)) {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) {
                    walks = walks.Where(w => w.Name.Contains(filterQuery));
                }
            }

            // Sorting: 
            if (!string.IsNullOrWhiteSpace(sortBy)) {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase)) {
                    walks = isAscending ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
                } else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase)) {
                    walks = isAscending ? walks.OrderBy(w => w.LengthInKm) : walks.OrderByDescending(w => w.LengthInKm);
                }
            }

            // Pagination 
            var skippedResults = (pageNumber - 1) * pageSize;
            walks = walks.Skip(skippedResults).Take(pageSize);

            return await walks.ToListAsync();
        }
        public async Task<Walk> CreateWalk(Walk walk) {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> GetWalkById(Guid id) {
            return await _context.Walks.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk?> DeleteWalkById([FromRoute] Guid id) {
            var walk = await _context.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if (walk == null) return null;
            _context.Walks.Remove(walk);
            await _context.SaveChangesAsync();
            return walk;
        }
    }
}
