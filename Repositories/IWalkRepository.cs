using EGWalks.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EGWalks.API.Repositories {
    public interface IWalkRepository {
        Task<Walk> CreateWalk(Walk walk);
        Task<Walk?> GetWalkById(Guid id);
        Task<List<Walk>> GetAllWalks(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

        Task<Walk> DeleteWalkById([FromRoute] Guid id);
    }
}
