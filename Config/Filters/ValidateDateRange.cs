using System.ComponentModel.DataAnnotations;

namespace api.Config.Filters;

public class ValidateDateRange : ValidationAttribute
{
    private readonly string _startDateProperty;

    public ValidateDateRange(string startDateProperty)
    {
        _startDateProperty = startDateProperty;
        ErrorMessage = "End date must be after the start date.";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        // Get the start date property value from the model
        var startDateProperty = validationContext.ObjectType.GetProperty(_startDateProperty);
        if (startDateProperty == null)
        {
            return new ValidationResult($"Unknown property: {_startDateProperty}");
        }

        var startDateValue =
            startDateProperty.GetValue(validationContext.ObjectInstance, null) as DateTime?;
        var endDateValue = value as DateTime?;

        if (!startDateValue.HasValue || !endDateValue.HasValue)
            return ValidationResult.Success!;

        // Validate that end date is greater than or equal to start date
        if (endDateValue < startDateValue)
        {
            return new ValidationResult(ErrorMessage);
        }

        int houres = (int)endDateValue.Value.Subtract(startDateValue.Value).TotalHours;

        // var houres = (endDateValue - startDateValue).Value.Hours;
        if (houres < 12)
        {
            return new ValidationResult("Must be difference 12 hours");
        }

        return ValidationResult.Success!;
    }
}
