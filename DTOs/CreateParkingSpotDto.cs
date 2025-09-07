using System.ComponentModel.DataAnnotations;

namespace ParkingServiceApi.DTOs
{
    public class CreateParkingSpotDto
    {
        [Required]
        public int ParkingLotId { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public bool IsOccupied { get; set; }
    }
}
