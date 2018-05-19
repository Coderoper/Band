using System;
using System.Collections.Generic;
using BandApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BandApp.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

  }
}
