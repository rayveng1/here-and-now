using Newtonsoft.Json;

namespace HereAndNow.Models;

public class Geolocation
{
    [JsonProperty("query")]
    public string? Query;

    [JsonProperty("status")]
    public string? Status;

    [JsonProperty("country")]
    public string? Country;

    [JsonProperty("countryCode")]
    public string? CountryCode;

    [JsonProperty("region")]
    public string? Region;

    [JsonProperty("regionName")]
    public string? RegionName;

    [JsonProperty("city")]
    public string? City;

    [JsonProperty("zip")]
    public string? Zip;

    [JsonProperty("lat")]
    public double? Lat;

    [JsonProperty("lon")]
    public double? Lon;

    [JsonProperty("timezone")]
    public string? Timezone;

    [JsonProperty("isp")]
    public string? Isp;

    [JsonProperty("org")]
    public string? Org;

    [JsonProperty("as")]
    public string? As;

    public static Geolocation FromJson(string json)
    {
        var geolocation = JsonConvert.DeserializeObject<Geolocation>(json);

        if (geolocation is not null)
        {
            return geolocation;
        }
        throw new InvalidOperationException("Failed to deserialize json");
    }
}