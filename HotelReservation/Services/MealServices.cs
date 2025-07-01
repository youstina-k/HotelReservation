using HotelReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Services
{
    public class MealServices
    {

        private HotelReservationDbContext hotelContext;

        public MealServices(HotelReservationDbContext HotelContext)
        {
            hotelContext = HotelContext;
        }

        public async Task<List<MealPlan>> GetAvailableMealPlans()
        {
            try
            {
                return await hotelContext.MealPlans.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving meal plans.", ex);
            }
        }
        public async Task<List<MealPlanRate>> GetAvailableMealPlanRates(int mealPlanId)
        {
            try
            {
                return await hotelContext.MealPlanRates.Where(r => r.MealPlanId == mealPlanId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving meal plan rates.");
            }
        }
    }
}
