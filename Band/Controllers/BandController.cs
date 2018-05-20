using System;
using System.Collections.Generic;
using BandApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BandApp.Controllers
{
  public class BandController : Controller
  {
      [HttpGet("/bands")]
      public ActionResult Index()
      {
      List<Band> allBands = Band.GetAll();
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Venue> newVenue = Venue.GetAll();
      model.Add("Venues", newVenue);
      model.Add("Bands", allBands);

      return View(model);
      }
      [HttpGet("/bands/new")]
      public ActionResult CreateForm()
      {
        return View("Form");
      }
      [HttpPost("/bands")]
      public ActionResult Create()
      {
        Band newBand = new Band(Request.Form["band_name"]);
        newBand.Save();

        // List<Band> allBands = Band.GetAll();
        return RedirectToAction("Index");
      }
      [HttpGet("/bands/{id}")]
      public ActionResult Details(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band selectedBand = Band.Find(id);
        List<Venue> bandVenues = selectedBand.GetVenues();
        model.Add("selectedBand", selectedBand);
        model.Add("bandVenues", bandVenues);
        return View("Details", model);
      }
      [HttpGet("/bands/{id}/delete")]
      public ActionResult DeleteBand(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band selectedBand = Band.Find(id);
        selectedBand.DeleteBand();
        return View("Index", model);
      }

      // [HttpPost("/search")]
      // public ActionResult Search()
      // {
      //   List<Band> searchBand = Band.SearchBands(Request.Form["searchdate"]);
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   List<Venue> newVenue = Venue.GetAll();
      //   model.Add("Venues", newVenue);
      //   model.Add("Bands", searchBand);
      //   return View("Index", model);
      // }

      [HttpGet("/bands/{id}/update")]
      public ActionResult UpdateForm(int id)
      {
        Band selectedBand = Band.Find(id);
        return View("BandUpdate", selectedBand);
        // return RedirectToAction("Index")
      }

      [HttpPost("/bands/{id}/update")]
      public ActionResult PostUpdate(int id)
      {
        Band selectedBand = Band.Find(id);
        selectedBand.UpdateBand(Request.Form["band_name"]);
        return RedirectToAction("Index");
      }
  }
}
