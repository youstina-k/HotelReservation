    namespace HotelReservation.Business_Logic
{
    
    using HotelReservation.Models;
    using Microsoft.EntityFrameworkCore;
   

    public class ReservationService : IReservationService
    {
        private HotelReservationDbContext reservationDatabase;

        public ReservationService(HotelReservationDbContext context)
        {
            reservationDatabase = context;
        }

        public async Task<decimal> GetReservationTotalPrice(
            DateOnly checkIn,
            DateOnly checkOut,
            int? adults,
            int? children,
            int? roomTypeId,
            int? mealPlanId)
        {
            int? totalGuests = adults + children;
            var roomRates = await reservationDatabase.RoomRates
                      .Where(r => r.RoomTypeId == roomTypeId).ToListAsync();

            var mealRates = await reservationDatabase.MealPlanRates
                      .Where(m => m.MealPlanId == mealPlanId).ToListAsync();

            var roomType = await reservationDatabase.RoomTypes
             .FirstOrDefaultAsync(r => r.RoomTypeId == roomTypeId);

            decimal numberOfAdultsPerRoom = roomType.MaxAdults;
            decimal numberOfChildrenPerRoom = roomType.MaxChildren;

            int roomsForAdults = (int)Math.Ceiling(adults.Value / numberOfAdultsPerRoom);
            int roomsForChildren = (int)Math.Ceiling(children.Value / numberOfChildrenPerRoom);
            int numberOfRooms = Math.Max(roomsForAdults, roomsForChildren);

            decimal total = 0;

            for (var currentDate = checkIn; currentDate < checkOut; currentDate = currentDate.AddDays(1))
            {

                var roomRate = roomRates
                    .FirstOrDefault(r => r.FromDate <= currentDate && r.ToDate >= currentDate);

                var mealRate = mealRates
                    .FirstOrDefault(m => m.FromDate <= currentDate && m.ToDate >= currentDate);

                decimal dailyTotal = (roomRate.RatePerNight * numberOfRooms)
                                    + (mealRate.RatePerPersonPerNight * totalGuests.Value);

                total += dailyTotal;
            }


            return total;
        }
    }
}
