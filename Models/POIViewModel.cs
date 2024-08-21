using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HereAndNow.Attributes;

namespace HereAndNow.Models;

public class POIViewModel
{
    public Place Place {get; set;}
    public string Reviews {get; set;}
    public User User {get; set;}
    
}