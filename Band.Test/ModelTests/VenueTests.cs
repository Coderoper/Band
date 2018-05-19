using System;
using System.Collections.Generic;
using BandApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;

namespace BandApp.Tests
{
  [TestClass]
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }
    public void Dispose()
    {
       Venue.DeleteAll();
       Band.DeleteAll();
    }
    // [TestMethod]
    // public void Save_InstanceToDatabase_VenueTable()
    // {
    //   //Arrange
    //   Venue testVenue = new Venue ("Kansas City");
    //   testVenue.Save();
    //   //Act
    //   List<Venue> result = Venue.GetAll();
    //   List<Venue> testList = new List<Venue>{testVenue};
    //   //Arrange
    //   Console.WriteLine(testList.Count + "()" + result.Count);
    //   CollectionAssert.AreEqual(testList, result);
    // }
    // [TestMethod]
    // public void test_GetAll()
    // {
    //   //Arrange
    //   Venue testVenue = new Venue ("Okc");
    //   testVenue.Save();
    //   testVenue.Save();
    //   //Act
    //   List<Venue> result = Venue.GetAll();
    //
    //   //Arrange
    //   Assert.AreEqual(2, result.Count);
    // }
    // [TestMethod]
    // public void test_Update()
    // {
    //   Venue testVenue = new Venue ("Phoenix");
    //   testVenue.Save();
    //   testVenue.UpdateVenue("Portland");
    //   Venue newVenue = new Venue("Portland");
    //   List<Venue> testList = new List<Venue> {newVenue};
    //   List<Venue> allVenue = Venue.GetAll();
    //   Console.WriteLine(allVenue[0].GetVenueName() + ">>>" + testList[0].GetVenueName());
    //   CollectionAssert.AreEqual(testList, allVenue);
    // }
    // [TestMethod]
    // public void find_in_database()
    // {
    //   //Arrange
    //   Venue testItem = new Venue("Boise");
    //   testItem.Save();
    //
    //   //Act
    //   Venue result = Venue.Find(testItem.GetId());
    //   Console.WriteLine("testItem Id: " +testItem.GetId());
    //   Console.WriteLine("result Id: " +result.GetId());
    //   //Assert
    //   Assert.AreEqual(testItem, result);
    // }
    // [TestMethod]
    // public void test_join_get()
    // {
    //   //Arrange
    //   Venue testVenue = new Venue("Boise");
    //   testVenue.Save();
    //
    //   Band testBand = new Band ("Slipknot");
    //   testBand.Save();
    //   // Act
    //   testBand.AddVenueToBand(testVenue);
    //
    //   List<Band> result = testVenue.GetBands();
    //
    //   List<Band> testList = new List<Band>{testBand};
    //
    //   //Assert
    //   CollectionAssert.AreEqual(testList, result);
    // }
  }
}
