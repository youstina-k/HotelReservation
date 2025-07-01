using HotelReservation.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace HotelReservation.Services
{
    
    public class RoomServices
    {
        private HotelReservationDbContext hotelContext;

        public RoomServices(HotelReservationDbContext HotelContext)
        {
            hotelContext = HotelContext;
        }
        public async Task<List<RoomType>> GetAvailableRoomTypes()
        {
            try
            {
                return await hotelContext.RoomTypes.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving room types.");
            }
        }
        public async Task<RoomType> GetRoomTypeById(int roomTypeId)
        {
            try
            {
                return await hotelContext.RoomTypes.FirstAsync(r => r.RoomTypeId == roomTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving room type.");
            }
        }
        public async Task<List<RoomRate>> GetAvailableRoomRates(int roomTypeId)
        {
            try
            {
                return await hotelContext.RoomRates.Where(r => r.RoomTypeId == roomTypeId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving room type rates.");
            }
        }


    }
}
