using System;
using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Validation
{
    public class DateRange : ValidationAttribute
    {
        private readonly string comparisonProperty;

        public DateRange(string ComparisonProperty)
        {
            comparisonProperty = ComparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var currentValue = (DateOnly?)value;
            if (currentValue < DateOnly.FromDateTime(DateTime.Today) ) { return new ValidationResult("Please enter a Valid date"); }
            var property = validationContext.ObjectType.GetProperty(comparisonProperty);

            var comparisonValue = (DateOnly?)property.GetValue(validationContext.ObjectInstance);
           
            
            if (currentValue.HasValue && comparisonValue.HasValue)
            {
                if (currentValue.Value <= comparisonValue.Value)
                {
                    return new ValidationResult("Your check out date must be later than your check in date");
                }
            }

            return ValidationResult.Success;
        }
    }
}