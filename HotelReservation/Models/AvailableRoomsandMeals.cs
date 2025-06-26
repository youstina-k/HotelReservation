using Microsoft.EntityFrameworkCore;
namespace HotelReservation.Models
{
    
    public class AvailableRoomsAndMeals
    {
        private HotelReservationDbContext hotelContext;

        public AvailableRoomsAndMeals(HotelReservationDbContext HotelContext)
        {
            hotelContext = HotelContext;
        }
        public async Task<List<RoomType>> GetAvailableRoomTypes()
        {
            return await hotelContext.RoomTypes.ToListAsync();
        }
        public async Task<List<MealPlan>> GetAvailableMealPlans()
        {
            return await hotelContext.MealPlans.ToListAsync();
        }
    }
}
