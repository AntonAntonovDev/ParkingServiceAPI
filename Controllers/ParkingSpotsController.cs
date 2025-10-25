using Microsoft.AspNetCore.Mvc;
//using ParkingServiceApi.Entities;
using ParkingServiceApi.DTOs;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using ParkingServiceApi.Data;
using Microsoft.EntityFrameworkCore;
using ParkingServiceApi.Data.Models;
using AutoMapper;

namespace ParkingServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingSpotsController : ControllerBase
    {
        private ParkingServiceDbContext context;
        private IMapper mapper;
        private readonly ILogger<ParkingSpotsController> logger;

        public ParkingSpotsController(ParkingServiceDbContext _context, IMapper _mapper, ILogger<ParkingSpotsController> _logger) {
            context = _context;
            mapper = _mapper;
            logger = _logger;
        }    

        //private static List<ParkingSpotDto> _parkingSpots = new List<ParkingSpotDto>();
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 30)]
        public async Task<ActionResult<List<ParkingSpotDto>>> GetAllSpots()
        {
            logger.LogInformation("Получен запрос на кол-во парковочных мест");
            var allSpots = await context.ParkingSpots
            .AsNoTracking()
            .Select(x => new ParkingSpotDto
            {
                ParkingSpotId = x.ParkingSpotId,
                ParkingLotId = x.ParkingLotId,
                Number = x.Number,
                IsOccupied = x.IsOccupied
            })
            .ToListAsync();

            return Ok(allSpots);
        }

        [HttpPost]
        public async Task<ActionResult<ParkingSpotDto>> Create([FromBody] CreateParkingSpotDto spot)
        {
            var newSpot = mapper.Map<ParkingSpot>(spot);

            context.ParkingSpots.Add(newSpot);

            await context.SaveChangesAsync();

            var dto = mapper.Map<ParkingSpotDto>(newSpot);

            return CreatedAtAction(nameof(GetSpotById), new { Id = dto.ParkingSpotId }, dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpotDto>> GetSpotById(int id)
        {
            //var spot = _parkingSpots.FirstOrDefault(x => x.Id == id);

            var spot = await context.ParkingSpots.AsNoTracking().FirstOrDefaultAsync(x => x.ParkingSpotId == id);

            if (spot == null)
            {
                return NotFound();
            }
            else
            {
                var dto = mapper.Map<ParkingSpotDto>(spot);
                return Ok(dto);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ParkingSpotDto>> Update(int id, [FromBody] CreateParkingSpotDto spot)
        {
            var updatedSpot = await context.ParkingSpots
            .FirstOrDefaultAsync(x => x.ParkingSpotId == id);

            if (updatedSpot != null)
            {
                updatedSpot.Number = spot.Number;
                updatedSpot.IsOccupied = spot.IsOccupied;

                await context.SaveChangesAsync();
                
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletedSpot = await context.ParkingSpots.FirstOrDefaultAsync(x => x.ParkingSpotId == id);

            if (deletedSpot != null)
            {
                context.ParkingSpots.Remove(deletedSpot);
                await context.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
