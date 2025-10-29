using System.ComponentModel.DataAnnotations;

namespace ParkingServiceApi.DTO
{
    public class CreateParkingLotDto
    {
        [Required]
        public string Name { get; set; }

        public int TotalSpots { get; set; }
    }
}
