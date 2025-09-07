using AutoMapper;
using ParkingServiceApi.Data.Models;
using ParkingServiceApi.DTOs;

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
