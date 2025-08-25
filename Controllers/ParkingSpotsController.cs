using Microsoft.AspNetCore.Mvc;
using ParkingServiceApi.Entities;
using ParkingServiceApi.DTOs;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace ParkingServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingSpotsController : ControllerBase
    {
        private static List<ParkingSpotDto> _parkingSpots = new List<ParkingSpotDto>();
        [HttpGet("getAllSpots")]
        public async Task<ActionResult<List<ParkingSpotDto>>> GetAllSpots()
        {
            if (_parkingSpots.Count == 0)
            {
                return Ok(new List<ParkingSpotDto>());
            }
            else
            {
                return Ok(_parkingSpots);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<ParkingSpotDto>> Create([FromBody] CreateParkingSpotDto spot)
        {
            var newSpot = new ParkingSpotDto
            {
                Id = "1",
                Number = spot.Number,
                IsOccupied = spot.IsOccupied
            };

            _parkingSpots.Add(newSpot);

            return CreatedAtAction(nameof(GetSpotById), new { Id = newSpot.Id }, newSpot);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpotDto>> GetSpotById(string id)
        {
            var spot = _parkingSpots.FirstOrDefault(x => x.Id == id);

            if (spot == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(spot);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ParkingSpotDto>> Update(string id, [FromBody] CreateParkingSpotDto spot)
        {
            var updatedSpot = _parkingSpots.FirstOrDefault(x => x.Id == id);
            if (updatedSpot != null)
            {
                updatedSpot.Number = spot.Number;
                updatedSpot.IsOccupied = spot.IsOccupied;

                return Ok(updatedSpot);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var deletedSpot = _parkingSpots.FirstOrDefault(x => x.Id == id);

            if (deletedSpot != null)
            {
                _parkingSpots.Remove(deletedSpot);
                return Ok(new { message = "Parking spot deleted successfully." });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
