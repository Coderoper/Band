using System;
using System.Collections.Generic;
using BandApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;

namespace BandApp.Tests
{
  [TestClass]
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();

    }
    //test whether two identical bands are recognized as identical instances
    [TestMethod]
    public void Saves_TwoBands_AsSame()
    {
      //Arrange
      Band firstBand = new Band ("Modest Mouse");
      firstBand.Save();
      Band secondBand = new Band ("Modest Mouse");
      secondBand.Save();

      Console.WriteLine("firstBand: " +firstBand.GetId());
      Console.WriteLine("secondBand: " +secondBand.GetId());
      //Assert
      Assert.AreEqual(firstBand, secondBand);
    }
    // //tests whether Save method works on the Band class
    // [TestMethod]
    // public void Save_InstanceToDatabase_BandsTable()
    // {
    //   //Arrange
    //   Band testBand = new Band ("Nirvana");
    //   testBand.Save();
    //   //Act
    //   List<Band> result = Band.GetAll();
    //   List<Band> testList = new List<Band>{testBand};
    //   //Arrange
    //   CollectionAssert.AreEqual(testList, result);
    // }
    // //tests whether customer instances are being linked to band instances via the join table
    // [TestMethod]
    // public void AddVenue_ToBandObject()
    // {
    //   //Arrange
    //   Band testBand = new Band ();
    //   testBand.Save();
    //
    //   Venue testVenue = new Venue ("Seattle");
    //   testVenue.Save();
    //   //Act
    //   testBand.AddVenueToBand(testVenue);
    //   List<Venue> result = testBand.GetVenues();
    //   int resultOne = result[0].GetId();
    //   int resultTwo = testVenue.GetId();
    //   //Assert
    //   Assert.AreEqual(resultOne, resultTwo);
    // }
    // //tests whether find method works on instances of Band class
    // [TestMethod]
    // public void Find_AllBandObjects_InDatabase()
    // {
    //   //Arrange
    //   Band testBand = new Band ("Jimi Hendrix");
    //   testBand.Save();
    //   //Act
    //   Band foundBand = Band.Find(testBand.GetId());
    //   //Assert
    //   Assert.AreEqual(testBand, foundBand);
    // }
    // //tests whether update method works on instances of Band class
    // [TestMethod]
    // public void Update_BandObject_BandsTable()
    // {
    //   //Arrange
    //   Band testBand = new Band ("Gem Ham");
    //   testBand.Save();
    //
    //   string updatedName = "Pearl Jam";
    //   //Act
    //   testBand.UpdateBand(updatedImage, updatedAuthor, updatedName, updatedIsbn, updatedPublisher, updatedPrice, updatedQuantity);
    //   string result = Band.Find(testBand.GetId()).GetName();
    //   //Assert
    //   Assert.AreEqual(updatedName, result);
    // }
    //
    // [TestMethod]
    // public void TestSearchBand()
    // {
    //   Band newBand = new Band("Pink Floyd");
    //   newBand.Save();
    //   List<Band> MyBands = Band.SearchBands("This is a test");
    //   Assert.AreEqual("This is a test",MyBands[0].GetName());
    // }
    //
    //
    //
    // [TestMethod]
    // public void TestUpdateQuantity()
    // {
    //   Band MyBand = new Band("img", "James Harrison", "IPython Notebands", "21231233", "Version", 21, 10);
    //   MyBand.Save();
    //   MyBand.UpdateQuantity();
    //   Assert.AreEqual(9,MyBand.GetQuantity());
    // }
  }
}
