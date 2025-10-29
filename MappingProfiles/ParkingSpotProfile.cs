using AutoMapper;
using ParkingServiceApi.Data.Models;
using ParkingServiceApi.DTO;

namespace ParkingServiceApi.MappingProfiles
{
    public class ParkingSpotProfile : Profile
    {
        public ParkingSpotProfile()
        {
            CreateMap<ParkingSpot, ParkingSpotDto>();

            //CreateMap<ParkingSpotDto, ParkingSpot>();

            CreateMap<CreateParkingSpotDto, ParkingSpot>();
        }
    }
}
