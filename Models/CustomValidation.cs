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
        return new ValidationResult("Date must be in the future!");
      }
      return ValidationResult.Success;
    }
  }
}