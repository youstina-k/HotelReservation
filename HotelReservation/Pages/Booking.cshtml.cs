using System.Threading.Tasks;
using HotelReservation.Business_Logic;
using HotelReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Pages
{
    public class BookingModel : PageModel
    {
       
        private IReservationService reservationService;
        private AvailableRoomsAndMeals roomsAndMeals;
        private SaveToDataBase saveNewReservations;
        [BindProperty]
        public Reservation NewReservation { get; set; }
        public List<RoomType> AvailableRoomTypes { get; set; } = [];
        public List<MealPlan> AvailableMealPlans { get; set; } = [];
        public decimal TotalPrice = 0;
        
        public BookingModel( IReservationService ReservationService,
            AvailableRoomsAndMeals RoomsAndMeals, SaveToDataBase SaveNewReservations)
        {
            reservationService = ReservationService;
            roomsAndMeals = RoomsAndMeals;
            saveNewReservations = SaveNewReservations;
        }
        public async Task OnGet()
        {
            AvailableRoomTypes = await roomsAndMeals.GetAvailableRoomTypes();
            AvailableMealPlans = await roomsAndMeals.GetAvailableMealPlans();
        }
        public async Task<IActionResult> OnPost()
        {
            
            if (ModelState.IsValid)
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

            AvailableRoomTypes = await roomsAndMeals.GetAvailableRoomTypes();
            AvailableMealPlans = await roomsAndMeals.GetAvailableMealPlans();
            return Page();
        }

    }
}
