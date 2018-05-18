using System;
using System.Collections.Generic;
using BandApp;
using MySql.Data.MySqlClient;

namespace Band.Models
{
  public class Band
  {
    //private variables
    private int _id;
    private string _bandName;



    public Band (string bandName, int id = 0)
    {
      _id =id;
      _bandName = bandName;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetBandName()
    {
      return _bandName;
    }
    public void SetBandName(string newBandName)
    {
      _bandName = newbandName;
    }

    //overrides for testing
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public override bool Equals(System.Object otherBook)
    {
      if (!(otherBook is Book))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool nameEquality = this.GetBandName() == newBand.GetBandName();
        bool isbnEquality = this.GetIsbn() == newBook.GetIsbn();
      }
    }

    //create Band instance in database table 'bands'
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands (bandName) VALUES (@BandName);";

      MySqlParameter bookName = new MySqlParameter();
      bookName.ParameterName = "@BandName";
      bookName.Value = this._bookName;
      cmd.Parameters.Add(bookName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    //create join table instance, linking venues(s) to a Band instance
    public void AddVenueToBand(Venue newVenue)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues(bands_id, venues_id) VALUES (@BandId, @VenueId);";

      MySqlParameter band_id = new MySqlParameter();
      band_id.ParameterName = "@BandId";
      band_id.Value = _id;
      cmd.Parameters.Add(band_id);

      MySqlParameter venue_id = new MySqlParameter();
      venue_id.ParameterName = "@VenueId";
      venue_id.Value = newVenue.GetId();
      cmd.Parameters.Add(venue_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    //get all single Book instances in Book class
    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from bands;";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);

        Band newBand = new Band(name, id);
        allBands.Add(newBand);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allBands;
    }

    //get all Book-linked customer instances from join table
    public List<Venue> GetVenue()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT venues.* FROM bands
      JOIN bands_venues ON (bands.id = bands_venues.bands_id)
      JOIN venues ON (bands_venues.venues_id = venues_id) WHERE bands.id = @BandId;";

      MySqlParameter BandId = new MySqlParameter();
      BandkId.ParameterName = "@BandId";
      BandId.Value = _id;
      cmd.Parameters.Add(BandId);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Venue> Venues = new List<Venue> {};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Venue newVenue = new Venue(name, id);
        Venues.Add(newVenue);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return Venues;
    }

    //find single Book instance in table 'books'
    public static Band Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int bandId = 0;
      string bandName = "";
      while(rdr.Read())
      {
        bookId = rdr.GetInt32(0);
        bandName = rdr.GetString(1);
      }

      Band foundBand = new Band (bandName, id);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundBand;
    }

    //update single Book instance in table 'books'
    public void UpdateBand(string bandName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE bands SET bandName = @BandName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@BandName";
      name.Value = bookName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _bandName = bandName;

      if(conn != null)
      {
        conn.Dispose();
      }

    }

    //delete single Book instance in table 'bands'
    public void DeleteBand()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands WHERE id = @BandId
      DELETE FROM books_customers WHERE books_id = @BookId;";

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@BandId";
      id.Value = this.GetId();
      cmd.Parameters.Add(id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    //delete all Band instances
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE from bands;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    //search for books
    // public static List<Band> SearchBands(string band)
    // {
    //  List<Band> MyBooks = new List<Book> {};
    //  MySqlConnection conn = DB.Connection();
    //  conn.Open();
    //
    //  MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    //  cmd.CommandText = @"SELECT * FROM bands WHERE bandName LIKE '%" + band "';";
    //  MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    //  while(rdr.Read())
    //  {
    //    int id = rdr.GetInt32(0);
    //    string bandName = rdr.GetString(1);
    //    Band newBand = new Band(bandName, id);
    //    MyBands.Add(newBand);
    //  }
    //
    //  conn.Close();
    //  if(conn != null)
    //  {
    //    conn.Dispose();
    //  }
    //  return MyBands;
    // }
  }
}
