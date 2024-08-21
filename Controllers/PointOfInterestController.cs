using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HereAndNow.Models;
using HereAndNow.Services;
using HereAndNow.Context;
using System.Runtime.CompilerServices;

namespace HereAndNow.Controllers;

public class PointOfInterestController : Controller
{
    private readonly ILogger<PointOfInterestController> _logger;
    private readonly ApplicationContext _context;
    
    
    public PointOfInterestController(ILogger<PointOfInterestController> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("pointofinterest")]
    async public Task<IActionResult> PointOfInterest(string place)
    {        
        // var ipAddress = IPGeolocationAPIService.GetLocalIPAddress();        
        // var geolocation = await _geolocationService.GetGeolocationAsync(_ipEndpoint, ipAddress);
        // var placesAPIResponse = await _placeService.GetPlaceAsync(_googlePlacesEndpoint, geolocation, radius:3500, [Types.FastFood]);
        // List<Place> places = Randomize(placesAPIResponse, 1);

        // List<Place> places = new List<Place>(){place};
        // Console.WriteLine($"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ {_context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"))} @@@@");
        //         Console.WriteLine($"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ {_context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"))} @@@@");
        // Console.WriteLine($"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ {_context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"))} @@@@");
        // Console.WriteLine($"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ {_context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"))} @@@@");
        // Console.WriteLine($"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ {_context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"))} @@@@");

        var placeNew = Place.FromJson(place);
        POIViewModel viewModel = new()
        {
            Place = placeNew,
            Reviews = "hi",
            User = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"))
        };
        return View("PointOfInterest", viewModel);
    }   


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
