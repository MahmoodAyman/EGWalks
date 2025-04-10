using AutoMapper;
using EGWalks.API.Models.Domain;
using EGWalks.API.Models.DTO;

namespace EGWalks.API.Mappings {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles() {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
        }
    }
}
