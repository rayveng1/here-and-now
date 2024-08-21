using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HereAndNow.Models;

public class Place
{

    [Key]
    public int Id {get; set;}


    [JsonProperty("types")]
    public List<string>? Types {get; set;}
    
    [JsonProperty("formattedAddress")]
    public string? FormattedAddress;

    [JsonProperty("rating")]
    public double? Rating {get; set;}
    

    [JsonProperty("googleMapsUri")]
    public string? GoogleMapsUri {get; set;}
    

    [JsonProperty("priceLevel")]
    public string? PriceLevel {get; set;}
    

    [JsonProperty("displayName")]
    public DisplayName? DisplayName {get; set;}
    public int CreatorId {get; set;}
    public List<Association> AssociatedUsers {get; set;} = new();
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;


    public static Place FromJson(string json)
    {
        var place = JsonConvert.DeserializeObject<Place>(json);

        if (place is not null)
        {
            return place;
        }
        throw new InvalidOperationException("Failed to deserialize json");
    }

}


public class PlacesAPIResponse
{
    [JsonProperty("places")]
    public List<Place>? Places { get; set; }
    public static PlacesAPIResponse FromJson(string json)
    {
        var placesAPIResponse = JsonConvert.DeserializeObject<PlacesAPIResponse>(json);
        

        if (placesAPIResponse is not null)
        {
            return placesAPIResponse;
        }
        throw new InvalidOperationException("Failed to deserialize json");
    }

        public static string? GetTypesString(string json)
    {
        // var str = "{\"types\":[\"american_restaurant\",\"fast_food_restaurant\",\"restaurant\",\"food\",\"point_of_interest\",\"establishment\"],\"formattedAddress\":\"9785 Ferguson Rd, Dallas, TX 75228, USA\",\"rating\":";
        int j = 0, k = 1;
        for (var i = 0; i < json.Length; i++)
        {
            if (json[i] == 't')
            {
                if (json.Substring(i, 5) == "types")
                {
                    j = i + 8;
                    k = 1;
                    Console.WriteLine($"json[{j}]: {json[j]}");
                    while (json[j + k] != ']')
                    {
                        k++;
                    }
                    Console.WriteLine($"json[{j + k}]: {json[j + k]}");
                }
            }
        }
        var typesSubstring = json.Substring(j, k);
        Console.WriteLine($"Type Substring: {typesSubstring}");
        foreach (var type in typesSubstring.Split(","))
        {
            Console.WriteLine(type);

        }
        Console.WriteLine($"Type Substring: {json.Substring(j, k).Split(",")}");
        return typesSubstring;

    }
}

public class DisplayName
{

    [Key]
    public int Id {get; set;}

    [JsonProperty("text")]
    public string? Text { get; set; }

    public int PlaceId {get; set;}
}

public static class Types
{
public const string American = "american_restaurant";
public const string Bakery = "bakery";
public const string Bar = "bar";
public const string Breakfast = "breakfast_restaurant";
public const string Cafe = "cafe";
public const string Chinese = "chinese_restaurant";
public const string FastFood = "fast_food_restaurant";
public const string IceCream = "ice_cream_shop";
public const string Italian = "italian_restaurant";
public const string Japanese = "japanese_restaurant";
public const string Korean = "korean_restaurant";
public const string Library = "library";
public const string Mediterranean = "mediterranean_restaurant";
public const string Mexican = "mexican_restaurant";
public const string Park = "park";
public const string Pizza = "pizza_restaurant";
public const string Playground = "playground";
public const string Ramen = "ramen_restaurant";
public const string Restaurant = "restaurant";
public const string Seafood = "seafood_restaurant";
public const string Steak = "steak_house";
public const string TouristAttraction = "tourist_attraction";
}