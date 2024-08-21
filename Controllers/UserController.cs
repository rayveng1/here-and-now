using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HereAndNow.Models;
using HereAndNow.Services;
using HereAndNow.Context;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace HereAndNow.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly ApplicationContext _context;
    
    
    public UserController(ILogger<UserController> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("travel_log/{userId:int}")]
    public IActionResult TravelLog(int userId)
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
        var user = _context.Users.Include(u => u.AssociatedPlaces).FirstOrDefault(u => u.UserId == userId);
        return View("TravelLog", user);
    }

    [HttpGet("your_travel_log")]
    public IActionResult YourTravelLog()
    {
        var userId = HttpContext.Session.GetInt32("userId");
        var user = _context.Users.Include(u => u.AssociatedPlaces).FirstOrDefault(u => u.UserId == userId);
        // var places = _context.Users
        // .Include(u => u.AssociatedPlaces)
        // .FirstOrDefault(u => u.UserId == userId);
        var places = _context.Places.Where(p =>
                p.AssociatedUsers.Any(a => a.UserId == userId))
                .ToList();

        Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA@");
        Console.WriteLine($"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA@ {places.Count} @@@@");
        Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA@");
        

        return View("YourTravelLog", user);
    }

    [HttpPost("log/add")]
    public IActionResult LogPOI(Place place)
    {

        if(!ModelState.IsValid)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                Console.WriteLine(message);
            }
            return View("Index", "Home");
        }
        var userId = HttpContext.Session.GetInt32("userId") ?? 0;
        place.CreatorId = userId;
        // _context.
        _context.Places.Add(place);
        _context.SaveChanges();
        var user = _context.Users.Include(u => u.AssociatedPlaces).FirstOrDefault(u => u.UserId == userId);
        return RedirectToAction("YourTravelLog");
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
