using System;
using System.ComponentModel.DataAnnotations;

namespace api.Config.Filters;

[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public class ValidateDateTense : ValidationAttribute
{
    public bool? AllowFuture { get; set; } = true;

    protected override ValidationResult IsValid(object? value, ValidationContext context)
    {
        if (AllowFuture!.Value && (DateTime?)value < DateTime.Now.Date)
        {
            return new ValidationResult("Must be in future");
        }

        return ValidationResult.Success!;
    }
}
