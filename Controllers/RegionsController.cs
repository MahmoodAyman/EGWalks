using AutoMapper;
using EGWalks.API.Data;
using EGWalks.API.Models.Domain;
using EGWalks.API.Models.DTO;
using EGWalks.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGWalks.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper) {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        // GET: /api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions() {
            var regions = await _regionRepository.GetAllRegions();

            var regionsDto = _mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionsDto);
        }

        // GET: /api/regions/id
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetRegionById(Guid id) {
            var region = await _regionRepository.GetRegionById(id);
            if (region == null) {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDTO>(region);


            return Ok(regionDto);
        }

        // Create Region POST /api/regions
        [HttpPost]
        public async Task<IActionResult> CreateNewRegion([FromBody] AddRegionRequestDTO regionRequestDto) {


            var region = _mapper.Map<Region>(regionRequestDto);
            region = await _regionRepository.CreateNewRegion(region);
            var regionDto = _mapper.Map<RegionDTO>(region);
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }

        // Update Region PUT /api/regions/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateRegionById([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO regionUpdateRequestDto) {


            var region = _mapper.Map<Region>(regionUpdateRequestDto);
            region = await _regionRepository.UpdateRegionById(id, region);
            if (region == null) return NotFound();

            region.Code = regionUpdateRequestDto.Code;
            region.Name = regionUpdateRequestDto.Name;
            region.RegionImageUrl = regionUpdateRequestDto.RegionImageUrl;

            var RegionDto = _mapper.Map<RegionDTO>(region);

            return Ok(RegionDto);
        }

        // Delete Region DELETE /api/regions/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteRegionById([FromRoute] Guid id) {
            var region = await _regionRepository.DeleteRegionById(id);
            if (region == null) {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDTO>(region); 
            return Ok(regionDto);
        }
    }
}


