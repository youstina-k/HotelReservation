using HotelReservation.Models;

namespace HotelReservation.Services
{
    using Microsoft.EntityFrameworkCore;
    public class DataServices
    {

        private HotelReservationDbContext hotelContext;
        public DataServices(HotelReservationDbContext HotelContext)
        {
            hotelContext = HotelContext;
        }

        public async Task SaveNewReservation(Reservation NewReservation)
        {
            try
            {
                await hotelContext.Reservations.AddAsync(NewReservation);
                await hotelContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database update failed while saving the reservation.", ex);
            }
        }

    }
}