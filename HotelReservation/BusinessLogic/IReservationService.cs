namespace HotelReservation.Business_Logic
{
    public interface IReservationService
    {
        Task<decimal> GetReservationTotalPrice(
            DateOnly checkIn,
            DateOnly checkOut,
            int? adults,
            int? children,
            int? roomTypeId,
            int? mealPlanId);
    }
}
