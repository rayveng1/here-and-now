using System.Net.Http.Headers;
using HereAndNow.Models;

namespace HereAndNow.Services;

public interface IGooglePlacesAPIService
{
    Task<Place> GetPlaceAsync(string endpoint);
}

public class GooglePlacesAPIService : IGooglePlacesAPIService
{
    private readonly HttpClient _client;
    public GooglePlacesAPIService(HttpClient client)
    {
        _client = client;
    }

    async public Task<Place> GetPlaceAsync(string endpoint)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return Place.FromJson(json);
    }
}