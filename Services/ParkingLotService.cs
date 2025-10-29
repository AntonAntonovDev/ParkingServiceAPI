using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ParkingServiceApi.Data;
using ParkingServiceApi.Data.Models;
using ParkingServiceApi.DTO;
using ParkingServiceApi.Services.Interfaces;

namespace ParkingServiceApi.Services
{
    public class ParkingLotService : IParkingLotService
    {
        private readonly ParkingServiceDbContext context;
        public ParkingLotService(ParkingServiceDbContext _context)
        {
            context = _context;
        }
        async Task<ParkingLotDto?> IParkingLotService.CreateAsync(CreateParkingLotDto lot)
        {
            var createdLot = new ParkingLot
            {
                Name = lot.Name,
                TotalSpots = lot.TotalSpots
            };

            context.ParkingLots.Add(createdLot);
            await context.SaveChangesAsync();

            return new ParkingLotDto
            {
                Id = createdLot.ParkingLotId,
                Name = createdLot.Name,
                TotalSpots = createdLot.TotalSpots
            };

        }

        async Task<bool> IParkingLotService.DeleteAsync(int id)
        {
            var deletedLot = await context.ParkingLots.FirstOrDefaultAsync(x => x.ParkingLotId == id);

            if (deletedLot == null)
            {
                return false;
            }

            context.ParkingLots.Remove(deletedLot);

            await context.SaveChangesAsync();

            return true;
        }

        async Task<List<ParkingLotDto>> IParkingLotService.GetAllAsync()
        {
            return await context.ParkingLots
                .AsNoTracking()
                .Select(x => new ParkingLotDto
                {
                    Id = x.ParkingLotId,
                    Name = x.Name,
                    TotalSpots = x.TotalSpots
                }
                )
                .ToListAsync();
        }

        async Task<ParkingLotDto?> IParkingLotService.GetByIdAsync(int id)
        {
            return await context.ParkingLots
                .Select(x => new ParkingLotDto
                {
                    Id = x.ParkingLotId,
                    Name = x.Name,
                    TotalSpots = x.TotalSpots
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        async Task<ParkingLotDto?> IParkingLotService.UpdateAsync(int id, UpdateParkingLotDto lot)
        {
            var updatedlot = await context.ParkingLots.FirstOrDefaultAsync(x => x.ParkingLotId == id);

            if (updatedlot != null)
            {
                updatedlot.Name = lot.Name;
                updatedlot.TotalSpots = lot.TotalSpots;

                //context.ParkingLots.Add(updatedlot);
                await context.SaveChangesAsync();

                return new ParkingLotDto
                {
                    Id = updatedlot.ParkingLotId,
                    Name = updatedlot.Name,
                    TotalSpots = updatedlot.TotalSpots
                };
            }

            return new ParkingLotDto();  
        }
    }
}
