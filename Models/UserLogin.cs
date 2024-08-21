using System.ComponentModel.DataAnnotations;

namespace HereAndNow.Models;

public class UserLogin
{
    
    [Required(ErrorMessage = "Please enter your email.")]
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Invalid Email format.")]
    public string Email {get; set;}
    
    [Required(ErrorMessage = "Please enter your password.")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password {get; set;}  
}