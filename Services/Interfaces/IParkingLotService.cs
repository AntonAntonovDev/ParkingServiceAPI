using Microsoft.AspNetCore.Mvc;
using ParkingServiceApi.Data.Models;
using ParkingServiceApi.DTO;

namespace ParkingServiceApi.Services.Interfaces
{
    public interface IParkingLotService
    {
        Task<List<ParkingLotDto>> GetAllAsync();
        Task<ParkingLotDto?> GetByIdAsync(int id);
        Task<ParkingLotDto?> CreateAsync(CreateParkingLotDto lot);
        Task<ParkingLotDto?> UpdateAsync(int id, UpdateParkingLotDto lot);
        Task<bool> DeleteAsync(int id);
    }
}
