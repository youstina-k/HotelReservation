
using HotelReservation.Business_Logic;
using HotelReservation.Models;
using HotelReservation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace HotelReservation.Pages
{
    public class BookingModel : PageModel
    {

        private ReservationService reservationService;
        private MealServices meals;
        private RoomServices rooms;
        private DataServices saveNewReservations;
        [BindProperty]
        public Reservation NewReservation { get; set; }
        public List<RoomType> AvailableRoomTypes { get; set; } = [];
        public List<MealPlan> AvailableMealPlans { get; set; } = [];
        public decimal TotalPrice = 0;

        public BookingModel(ReservationService ReservationService,
            MealServices Meals,RoomServices Rooms, DataServices SaveNewReservations)
        {
            reservationService = ReservationService;
            rooms = Rooms;
            meals = Meals;
            saveNewReservations = SaveNewReservations;
        }
        public async Task OnGet()
        {
           
            AvailableRoomTypes = await rooms.GetAvailableRoomTypes();
            AvailableMealPlans = await meals.GetAvailableMealPlans();
        }
        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                try
                {

                    TotalPrice = await reservationService.GetReservationTotalPrice(NewReservation.CheckIn,
                    NewReservation.CheckOut,
                    NewReservation.Adults,
                    NewReservation.Children,
                    NewReservation.RoomTypeId,
                    NewReservation.MealPlanId);
                NewReservation.TotalPrice = TotalPrice;

                await saveNewReservations.SaveNewReservation(NewReservation);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong while processing your reservation. Please try again.");
                }
            }

            AvailableRoomTypes = await rooms.GetAvailableRoomTypes();
            AvailableMealPlans = await meals.GetAvailableMealPlans();
            return Page();
        }

    }
}
