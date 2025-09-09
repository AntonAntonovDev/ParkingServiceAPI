using System.ComponentModel.DataAnnotations;

namespace ParkingServiceApi.DTOs
{
    public class CreateParkingLotDto
    {
        [Required]
        public string Name { get; set; }

        public int TotalSpots { get; set; }
    }
}
