namespace HereAndNow.Models;

using System.ComponentModel.DataAnnotations;

public class Association
{
    [Key]
    public int AssociationId {get; set;}
    public int UserId {get; set;}
    public User? User {get; set;}
    public int PlaceId {get; set;}
    public Place? Wedding {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
}