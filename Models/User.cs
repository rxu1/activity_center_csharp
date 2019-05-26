using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBelt.Models
{
  public class User
  {
    [Key]
    public int UserId {get;set;}

    [Required]
    [MinLength(2, ErrorMessage="First Name must be longer than 3 characters!")]
    [Display(Name = "First Name:")]
    public string FirstName {get;set;}

    [Required]
    [MinLength(2, ErrorMessage="Last Name must be longer than 2 characters!")]
    [Display(Name = "Last Name:")]
    public string LastName {get;set;}

    [Required]
    [EmailAddress]
    [Display(Name = "Email:")]
    public string Email {get;set;}

    [Required]
    [DataType(DataType.Password)]
    // [MinLength(8, ErrorMessage="Password must be longer than 8 characters!")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one number and one special character! I know, this gets me every time!")]
    [Display(Name = "Password:")]
    public string Password {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password:")]
    public string Confirm {get;set;}

  }
}