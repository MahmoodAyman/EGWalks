using EGWalks.API.Data;
using EGWalks.API.Models.Domain;
using EGWalks.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGWalks.API.Repositories {
    public class SQLRegionRepository : IRegionRepository {
        private readonly EGWalksDbContext _context;
        public SQLRegionRepository(EGWalksDbContext context) {
            _context = context;
        }

        public async Task<List<Region>> GetAllRegions() {

            return await _context.Regions.ToListAsync();
        }
        public async Task<Region?> GetRegionById(Guid id) {
            return await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> CreateNewRegion(Region region) {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateRegionById([FromRoute] Guid id,
            [FromRoute] Region region) {
            var _region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (_region == null) return null;
            _region.Code = region.Code;
            _region.Name = region.Name;
            _region.RegionImageUrl = region.RegionImageUrl;
            await _context.SaveChangesAsync();
            return _region;
        }

        public async Task<Region?> DeleteRegionById([FromRoute] Guid id) {
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null) return null;
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }
    }
}
