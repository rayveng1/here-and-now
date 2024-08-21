using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HereAndNow.Authentication;
using HereAndNow.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // Import this for JObject

namespace HereAndNow.Services
{
    public interface IGooglePlacesAPIService
    {
        Task<PlacesAPIResponse> GetPlaceAsync(string endpoint, Geolocation geolocation);
        Task<PlacesAPIResponse> GetPlaceAsync(string endpoint, Geolocation geolocation, double radius);
        Task<PlacesAPIResponse> GetPlaceAsync(string endpoint, Geolocation geolocation, double radius, List<string> types);
        
    }

    public class GooglePlacesAPIService : IGooglePlacesAPIService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public GooglePlacesAPIService(HttpClient client, IOptions<AuthenticationSettings> authSettings)
        {
            _client = client;
            _apiKey = authSettings.Value.ApiKey;
        }

        public async Task<PlacesAPIResponse> GetPlaceAsync(string endpoint, Geolocation geolocation)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                var body = new
                {
                    includedTypes = new List<string>{"mexican_restaurant"},
                    maxResultCount = 1,
                    locationRestriction = new
                    {
                        circle = new 
                        {
                            center = new
                            {
                                latitude = geolocation.Lat,
                                longitude = geolocation.Lon
                            },
                            radius = 900.0
                        }
                    }       
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add(AuthConstants.ApiKeyHeaderName, _apiKey);
                request.Headers.Add(AuthConstants.FieldMaskHeaderName, "places.displayName.text,places.formattedAddress,places.types,places.rating,places.googleMapsUri,places.priceLevel");

                // Serialize body to JSON
                var jsonBody = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                // Print Status Error Message
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine($"Response content: {errorContent}");
                }
                // Console.WriteLine($"Response JSON: {json}");

                return PlacesAPIResponse.FromJson(json);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
        }

        public async Task<PlacesAPIResponse> GetPlaceAsync(string endpoint, Geolocation geolocation, double radius)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                var body = new
                {
                    includedTypes = new List<string>{"mexican_restaurant"},
                    maxResultCount = 1,
                    locationRestriction = new
                    {
                        circle = new 
                        {
                            center = new
                            {
                                latitude = geolocation.Lat,
                                longitude = geolocation.Lon
                            },
                            radius
                        }
                    }       
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add(AuthConstants.ApiKeyHeaderName, _apiKey);
                request.Headers.Add(AuthConstants.FieldMaskHeaderName, "places.displayName.text,places.formattedAddress,places.types,places.rating,places.googleMapsUri,places.priceLevel");

                // Serialize body to JSON
                var jsonBody = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                // Print Status Error Message
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine($"Response content: {errorContent}");
                }
                // Console.WriteLine($"Response JSON: {json}");

                return PlacesAPIResponse.FromJson(json);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
        }


        public async Task<PlacesAPIResponse> GetPlaceAsync(string endpoint, Geolocation geolocation, double radius, List<string> types)
        {
            Random rand = new();
            string rankPref = "POPULARITY";
            if(rand.Next(2) == 1)
            {
                rankPref = "DISTANCE";
            }
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                var body = new
                {
                    rankPreference = rankPref,
                    includedTypes = types,
                    locationRestriction = new
                    {
                        circle = new 
                        {
                            center = new
                            {
                                latitude = geolocation.Lat,
                                longitude = geolocation.Lon
                            },
                            radius
                        }
                    }       
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add(AuthConstants.ApiKeyHeaderName, _apiKey);
                request.Headers.Add(AuthConstants.FieldMaskHeaderName, "places.displayName.text,places.formattedAddress,places.types,places.rating,places.googleMapsUri,places.priceLevel");

                // Serialize body to JSON
                var jsonBody = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                // Console.WriteLine(json);
                

                // Print Status Error Message
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine($"Response content: {errorContent}");
                }
                // Console.WriteLine($"Response JSON: {json}");

                return PlacesAPIResponse.FromJson(json);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
        }  
    }
}
