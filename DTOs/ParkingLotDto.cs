namespace ParkingServiceApi.DTOs
{
    public class ParkingLotDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TotalSpots { get; set; }
    }
}
