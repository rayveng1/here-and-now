using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HereAndNow.Models;
using HereAndNow.Services;
using HereAndNow.Context;
using Microsoft.AspNetCore.Identity;

namespace HereAndNow.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _context;
    private readonly IConfiguration _config;
    private readonly IGooglePlacesAPIService _placeService;
    private readonly IIPGeolocationAPIService _geolocationService;
    private readonly string _googlePlacesEndpoint = "https://places.googleapis.com/v1/places:searchNearby";
    private readonly string _ipEndpoint = "http://ip-api.com/json";
    
    
    public HomeController(ILogger<HomeController> logger, IConfiguration config, IGooglePlacesAPIService placeService, IIPGeolocationAPIService geolocationService, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
        _config = config;
        _placeService = placeService;
        _geolocationService = geolocationService;
    }

    [HttpGet("getpoi")]
    async public Task<IActionResult> GetPOI(List<string> filters, int radius)
    {
        var ipAddress = IPGeolocationAPIService.GetLocalIPAddress();        
        var geolocation = await _geolocationService.GetGeolocationAsync(_ipEndpoint, ipAddress);

        List<Place> places;
        if (filters.Count == 0)
        {
            var placesAPIResponse = await _placeService.GetPlaceAsync(_googlePlacesEndpoint, geolocation, radius * 1608, [Types.FastFood]);
            places = Randomize(placesAPIResponse, 1);
        } else 
        {        
            var placesAPIResponse = await _placeService.GetPlaceAsync(_googlePlacesEndpoint, geolocation, radius * 1608, filters);
            places = Randomize(placesAPIResponse, 1);
        }
        bool showModal = places == null || !places.Any(); // Adjust condition as needed
        ViewBag.ShowModal = showModal;
    
        ViewBag.UserId = HttpContext.Session.GetInt32("userId");
        return View("Index", places[0]);
    }

    [HttpGet("")]
    public IActionResult Index()
    {        
        ViewBag.UserId = HttpContext.Session.GetInt32("userId");
        return View();
    }

    [HttpGet("LoginRegister")]
    public IActionResult LoginRegister(string? message)
    {        
        ViewBag.Error = message;
        return View();
    }
        
    [HttpPost("users/create")]
    public IActionResult CreateUser(User user)
    {
        if(!ModelState.IsValid)
        {
            return View("LoginRegister");
        }
        var hash = new PasswordHasher<User>();
        user.Password = hash.HashPassword(user, user.Password);
        
        _context.Users.Add(user);
        _context.SaveChanges();
        var foundUser = _context.Users.FirstOrDefault(currentUser => currentUser.Email == user.Email);
        if(foundUser is null)
        {
            return NotFound();
        }
        HttpContext.Session.SetInt32("userId", foundUser.UserId);
        return RedirectToAction("Index", "Home");
    }
        
    [HttpPost("login")]
    public IActionResult Login(UserLogin user)
    {        
        if(!ModelState.IsValid)
        {
            return View("Index");
        }

        var foundUser = _context.Users.FirstOrDefault(contextUser => contextUser.Email == user.Email);
        if(foundUser is null)
        {
            return RedirectToAction("LoginRegister", new {message = "Invalid Credentials."});
        }

        var hash = new PasswordHasher<User>();
        var result = hash.VerifyHashedPassword(foundUser, foundUser.Password, user.Password);

        if (result == 0)
        {   
            return RedirectToAction("LoginRegister", new {message = "Invalid Credentials."});
        }
        HttpContext.Session.SetInt32("userId", foundUser.UserId);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
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

    public static List<Place> Randomize(PlacesAPIResponse placesAPIResponse, int numberOfResults)
    {
        Random rand = new();
        List<int> lottery = [];
        for(var i = 0; i < numberOfResults; i++)
        {            
            var num = rand.Next(placesAPIResponse.Places.Count);
            while (lottery.Contains(num))
            {
                num = rand.Next(placesAPIResponse.Places.Count);
            }
            lottery.Add(num);
        }
        List<Place> retList = new();
        for (var i = 0; i < lottery.Count; i++)
        {
            retList.Add(placesAPIResponse.Places[lottery[i]]);
        }

        return retList;
    }
}
