using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission8IS413.Models;

namespace Mission8IS413.Controllers;

public class HomeController : Controller
{
    public IActionResult Quadrants()
    {
        return View();
    }

    
}