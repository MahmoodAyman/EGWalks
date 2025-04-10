using EGWalks.API.Data;
using EGWalks.API.Models.Domain;
using EGWalks.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EGWalks.API.Repositories {
    public interface IRegionRepository {

        Task<List<Region>> GetAllRegions();
        Task<Region?> GetRegionById(Guid id);
        Task<Region> CreateNewRegion(Region region);

        Task<Region?> UpdateRegionById([FromRoute] Guid id, [FromRoute] Region region);

        Task<Region?> DeleteRegionById([FromRoute] Guid id);

    }
}
