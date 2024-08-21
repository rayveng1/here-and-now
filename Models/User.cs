using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HereAndNow.Attributes;

namespace HereAndNow.Models;

public class User
{
    [Key]
    public int UserId {get; set;}

    [Required(ErrorMessage = "Please enter your username.")]
    [Display(Name = "Username")]
    public string Username {get; set;}
    
    [Required(ErrorMessage = "Please enter your email.")]
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Invalid Email format.")]
    [UniqueEmail]
    public string Email {get; set;}
    
    [Required(ErrorMessage = "Please enter your password.")]    
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least eight characters.")]
    public string Password {get; set;}
    
    [NotMapped]
    [Required(ErrorMessage = "Please retype your password.")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword {get; set;}

    public List<Place> AssociatedPlaces {get; set;} = new();


    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
}