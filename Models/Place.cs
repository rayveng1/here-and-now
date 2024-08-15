using Newtonsoft.Json;

namespace HereAndNow.Models;

public class Place
{

    public int Id {get; set;}

    [JsonProperty("status")]
    public int Status { get; set; }
    
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