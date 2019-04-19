using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DotNetBelt.Models
{
  public class FutureDateAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (DateTime.Parse(value.ToString()) < DateTime.Now )
        {
            return new ValidationResult("Please enter a future date.");
        }
        return ValidationResult.Success;
    }
  }
}