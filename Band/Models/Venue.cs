using System;
using System.Collections.Generic;
using BandApp;
using MySql.Data.MySqlClient;

namespace BandApp.Models
{
  public class Venue
  {
    //private variables
    private int _id;
    private string _venueName;

    //constructor
    public Venue (string venueName, int id = 0)
    {
      _id = id;
      _venueName = venueName;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetVenueName()
    {
      return _venueName;
    }
    public void SetVenueName(string newVenueName)
    {
      _venueName = newVenueName;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venues (venue_name) VALUES (@Name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@Name";
      name.Value = this._venueName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
          conn.Dispose();
      }
    }
    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Venue newVenue = new Venue(name, id);
        allVenues.Add(newVenue);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allVenues;
    }
    public void UpdateVenue(string newVenueName)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

        cmd.CommandText = @"UPDATE venues SET venue_name = @NewName WHERE id = @searchId;";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);

        MySqlParameter venue_name = new MySqlParameter();
        venue_name.ParameterName = "@NewName";
        venue_name.Value = newVenueName;
        cmd.Parameters.Add(venue_name);

        cmd.ExecuteNonQuery();
        _venueName = newVenueName;
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }
    public static Venue Find(int id)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM venues WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        int venueId = 0;
        string name = "";

        while(rdr.Read())
        {
          venueId = rdr.GetInt32(0);
          name = rdr.GetString(1);
        }

        Venue newVenue = new Venue(name, id);
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }

        return newVenue;
    }
    public void DeleteVenue()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues WHERE id = @VenueId;
      DELETE FROM bands_venues WHERE venues_id = @VenueId;";

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@VenueId";
      id.Value = _id;
      cmd.Parameters.Add(id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static void DeleteAll()
    {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"DELETE FROM venues; DELETE FROM bands_venues";
       cmd.ExecuteNonQuery();
       conn.Close();
       if (conn != null)
       {
           conn.Dispose();
       }
    }
    public List<Band> GetBands()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT bands.* FROM venues
            JOIN bands_venues ON (venues.id = bands_venues.venues_id)
            JOIN bands ON (bands_venues.bands_id = bands.id)
            WHERE venues.id = @VenueId;";
        MySqlParameter categoryIdParameter = new MySqlParameter();
        categoryIdParameter.ParameterName = "@VenueId";
        categoryIdParameter.Value = _id;
        cmd.Parameters.Add(categoryIdParameter);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<Band> Bands = new List<Band> {};
        while(rdr.Read())
        {
          int id = rdr.GetInt32(0);
          string bandName = rdr.GetString(1);

          Band newBand = new Band (bandName,id);
          Bands.Add(newBand);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return Bands;
    }
        public override bool Equals(System.Object otherVenue)
        {
          if (!(otherVenue is Venue))
          {
            return false;
          }
          else
          {
             Venue newVenue = (Venue) otherVenue;
             bool nameEquality = this.GetVenueName() == newVenue.GetVenueName();
             bool idEquality =this.GetId() == newVenue.GetId();

             return (nameEquality && idEquality);
           }
        }

        public override int GetHashCode()
        {
             return this.GetId().GetHashCode();
        }

        //search for bands
        public static List<Venue> SearchVenue(string venue)
        {
         List<Venue> MyVenues = new List<Venue> {};
         MySqlConnection conn = DB.Connection();
         conn.Open();

         MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
         cmd.CommandText = @"SELECT * FROM venues WHERE name LIKE '%" + venue +"%\';";
        //  Console.WriteLine(cmd.CommandText);
         MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
         while(rdr.Read())
         {
           int id = rdr.GetInt32(0);
           string name = rdr.GetString(1);

           Venue newVenue = new Venue(name);
           MyVenues.Add(newVenue);
         }

         conn.Close();
         if(conn != null)
         {
           conn.Dispose();
         }
         return MyVenues;
        }
  }
}
