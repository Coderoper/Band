using System;
using System.Collections.Generic;
using BandApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BandApp.Controllers
{
  public class VenueController : Controller
  {
    [HttpGet("/venues")]
    public ActionResult Index()
    {
      List<Venue> allVenue = Venue.GetAll();
      return View(allVenue);
    }
    [HttpGet("/venues/new")]
    public ActionResult CreateForm()
    {
      return View("Form");
    }
    [HttpPost("/venues")]
    public ActionResult Create()
    {
      Venue newVenue = new Venue(Request.Form["venue_name"]);
      newVenue.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/venues/{id}/")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();
      Venue selectedVenue = Venue.Find(id);
      List<Band> venueBands = selectedVenue.GetBands();
      List<Band> allBands = Band.GetAll();
      model.Add("selectedVenue", selectedVenue);
      model.Add("venueBands", venueBands);
      model.Add("allBands", allBands);
      return View("Details", model);
    }

    [HttpGet("/venues/{id}/update")]
    public ActionResult UpdateVenue(int id)
    {
      Venue selectedVenue = Venue.Find(id);
      return View("VenueUpdate",selectedVenue);
    }

    [HttpPost("/venues/{id}/updated")]
    public ActionResult PostUpdate(int id)
    {
      Venue foundVenue = Venue.Find(id);
      foundVenue.UpdateVenue(Request.Form["venue_name"]);
      return RedirectToAction("Index", foundVenue);
    }
    [HttpPost("/venues/{id}/")]
    public ActionResult PostNewBand(int id)
    {
      Venue selectedVenue = Venue.Find(id);
      Band selectedBand= Band.Find(int.Parse(Request.Form["band_id"]));
      selectedVenue.AddBandtoVenue(selectedBand);
      return RedirectToAction("Index",selectedVenue);
    }



    [HttpGet("/venues/{id}/delete")]
    public ActionResult DeleteVenue(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Venue selectedVenue = Venue.Find(id);
      selectedVenue.DeleteVenue();
      return RedirectToAction("Index", model);
    }

    [HttpGet("/venues/deleteall")]
    public ActionResult DeleteAll()
    {
      Venue.DeleteAll();
      return View("Index");
    }
  }
}
