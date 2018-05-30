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
      List<Venue> allVenues = Venue.GetAll();
      model.Add("allVenues", allVenues);
      model.Add("allBands", allBands);

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
        List<Venue> allVenues = Venue.GetAll();
        List<Venue> bandVenues = selectedBand.GetVenues();
        model.Add("selectedBand", selectedBand);
        model.Add("bandVenues", bandVenues);
        model.Add("allVenues", allVenues);
        return View("Details", model);
      }
      [HttpPost("/bands/{id}")]
      public ActionResult PostNewVenue(int id)
      {
        Band selectedBand = Band.Find(id);
        Venue selectedVenue=Venue.Find(int.Parse(Request.Form["venue_id"]));
        selectedBand.AddVenueToBand(selectedVenue);
        return RedirectToAction("Index", selectedVenue);
      }

      [HttpGet("/bands/delete")]
      public ActionResult DeleteBand(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band selectedBand = Band.Find(id);
        selectedBand.DeleteBand();
        return View("Index", model);
      }
      [HttpPost("/bands/delete")]
      public ActionResult DeletePost()
      {
        int id=int.Parse(Request.Form["deleteId"]);
        Band selectedBand = Band.Find(id);
        selectedBand.DeleteBand();
        return RedirectToAction("Index");
      }


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
      [HttpGet("/bands/deleteall")]
      public ActionResult DeleteAll()
      {
        Band.DeleteAll();
        return View("Index");
      }
  }
}
