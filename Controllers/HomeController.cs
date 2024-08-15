using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HereAndNow.Models;
using HereAndNow.Services;

namespace HereAndNow.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGooglePlacesAPIService _placeService;
    private readonly string _endpoint = "https://places.googleapis.com/v1/places:searchNearby";

    public HomeController(ILogger<HomeController> logger, IGooglePlacesAPIService placeService)
    {
        _logger = logger;
        _placeService = placeService;
    }

    [HttpGet("")]
    async public Task<IActionResult> Index()
    {
        var place = await _placeService.GetPlaceAsync(_endpoint);
        return View();
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
