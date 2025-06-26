namespace HotelReservation.Models
{
    using Microsoft.EntityFrameworkCore;
    public class SaveToDataBase
    {

        private HotelReservationDbContext? hotelContext;
        public SaveToDataBase(HotelReservationDbContext HotelContext)
        {
            hotelContext = HotelContext;
        }

        public async Task SaveNewReservation(Reservation NewReservation)
        {
             await hotelContext.Reservations.AddAsync(NewReservation);
             await hotelContext.SaveChangesAsync();
        }

    }
}