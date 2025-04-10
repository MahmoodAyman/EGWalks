using AutoMapper;
using EGWalks.API.Models.Domain;
using EGWalks.API.Models.DTO;
using EGWalks.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EGWalks.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalksController : ControllerBase {

        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper) {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000) {
            var walks = await _walkRepository.GetAllWalks(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            var walksDto = _mapper.Map<List<WalkDTO>>(walks);
            return Ok(walksDto);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id) {
            var walk = await _walkRepository.GetWalkById(id);
            if (walk == null) {
                return NotFound();
            }

            var WalkDto = _mapper.Map<WalkDTO>(walk);
            return Ok(WalkDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto walkDto) {
            var walkDomainModel = _mapper.Map<Walk>(walkDto);
            walkDomainModel = await _walkRepository.CreateWalk(walkDomainModel);
            var walkDTO = _mapper.Map<AddWalkRequestDto>(walkDomainModel);
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDomainModel.Id }, walkDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkById([FromRoute] Guid id) {
            var walk = await _walkRepository.DeleteWalkById(id);
            if (walk == null) {
                return NotFound();
            }

            var walkDto = _mapper.Map<WalkDTO>(walk);
            return Ok(walkDto);
        }
    }
}
