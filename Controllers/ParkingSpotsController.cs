using Microsoft.AspNetCore.Mvc;
using ParkingServiceApi.Entities;
using ParkingServiceApi.DTOs;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using ParkingServiceApi.Data;
using Microsoft.EntityFrameworkCore;
using ParkingServiceApi.Data.Models;

namespace ParkingServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingSpotsController : ControllerBase
    {
        private ParkingServiceDbContext context;
        public ParkingSpotsController(ParkingServiceDbContext _context) {
            context = _context;
        }    

        //private static List<ParkingSpotDto> _parkingSpots = new List<ParkingSpotDto>();
        [HttpGet("getAllSpots")]
        public async Task<ActionResult<List<ParkingSpotDto>>> GetAllSpots()
        {
            var allSpots = await context.ParkingSpots
            .AsNoTracking()
            .Select(x => new ParkingSpotDto
            {
                ParkingSpotId = x.ParkingSpotId,
                ParkingSlotId = x.ParkingLotId,
                Number = x.Number,
                IsOccupied = x.IsOccupied
            })
            .ToListAsync();

            return Ok(allSpots);
        }

        [HttpPost("create")]
        public async Task<ActionResult<ParkingSpotDto>> Create([FromBody] CreateParkingSpotDto spot)
        {
            //var newSpot = new ParkingSpotDto
            //{
            //    Id = "1",
            //    Number = spot.Number,
            //    IsOccupied = spot.IsOccupied
            //};

            var newSpot = new ParkingSpot()
            {
                ParkingLotId = spot.ParkingLotId,
                Number = spot.Number,
                IsOccupied = spot.IsOccupied
            };

            context.ParkingSpots.Add(newSpot);

            await context.SaveChangesAsync();

            var dto = new ParkingSpotDto()
            {
                ParkingSpotId = newSpot.ParkingSpotId,
                ParkingLotId = newSpot.ParkingLotId,
                Number = newSpot.Number,
                IsOccupied = newSpot.IsOccupied,
            };

            return CreatedAtAction(nameof(GetSpotById), new { Id = dto.ParkingSpotId }, dto);
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
