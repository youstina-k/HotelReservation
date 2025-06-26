using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Validation
{
    public class RequiredDate : ValidationAttribute
    {
       
        public RequiredDate()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var currentValue = (DateOnly?)value;
            if (currentValue < DateOnly.FromDateTime(DateTime.Today)) { return new ValidationResult("Please enter a Valid date"); }
            if (currentValue.Value == default) { return new ValidationResult("Please enter a check In date"); }
            

            return ValidationResult.Success;
        }
    }
}
    

