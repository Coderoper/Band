using System;
using System.Collections.Generic;
using Band.Models;
using Microsoft.AspNetCore.Mvc;

namespace Band.Controllers
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
