using System.Net;
using System.Net.Sockets;
using HereAndNow.Models;

namespace HereAndNow.Services;


public interface IIPGeolocationAPIService
{
    Task<Geolocation> GetGeolocationAsync(string endpoint, string ipAddress);
}

public class IPGeolocationAPIService : IIPGeolocationAPIService
{
    private readonly HttpClient _client;
    
    public IPGeolocationAPIService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Geolocation> GetGeolocationAsync(string endpoint, string ipAddress)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{endpoint}/{ipAddress}");
        var response = await _client.SendAsync(request);

        var json = await response.Content.ReadAsStringAsync();

        // Print Status Error Message
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {response.StatusCode}");
            Console.WriteLine($"Response content: {errorContent}");
        }
        Console.WriteLine($"Response JSON: {json}");

        return  Geolocation.FromJson(json);
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            Console.WriteLine($"#{ip} - {ip.AddressFamily}");
            
            if (ip.AddressFamily == AddressFamily.InterNetworkV6 && ip.ToString().Substring(0, 4) != "fe80")
            {

                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}