namespace HotelReservation.Business_Logic
{
    using HotelReservation.Services;



    public class ReservationService
    {
       
        private MealServices meals;
        private RoomServices rooms;

        public ReservationService( MealServices Meals, RoomServices Rooms)
        {
           
            rooms = Rooms;
            meals = Meals;
        }

        public async Task<decimal> GetReservationTotalPrice(
            DateOnly checkIn,
            DateOnly checkOut,
            int adults,
            int children,
            int roomTypeId,
            int mealPlanId)
        {
            int totalGuests = adults + children;
            var roomRates = await rooms.GetAvailableRoomRates(roomTypeId);

            var mealRates = await meals.GetAvailableMealPlanRates(mealPlanId);

            var roomType = await rooms.GetRoomTypeById(roomTypeId);

            decimal numberOfAdultsPerRoom = roomType.MaxAdults;
            decimal numberOfChildrenPerRoom = roomType.MaxChildren;

            int roomsForAdults = (int)Math.Ceiling(adults / numberOfAdultsPerRoom);
            int roomsForChildren = (int)Math.Ceiling(children / numberOfChildrenPerRoom);
            int numberOfRooms = Math.Max(roomsForAdults, roomsForChildren);

            decimal total = 0;

            for (var currentDate = checkIn; currentDate < checkOut; currentDate = currentDate.AddDays(1))
            {

                var roomRate = roomRates
                    .First(r => r.FromDate <= currentDate && r.ToDate >= currentDate);

                var mealRate = mealRates
                    .First(m => m.FromDate <= currentDate && m.ToDate >= currentDate);

                decimal dailyTotal = (roomRate.RatePerNight * numberOfRooms)
                                    + (mealRate.RatePerPersonPerNight * totalGuests);

                total += dailyTotal;
            }
            return total;
        }
    }
}
