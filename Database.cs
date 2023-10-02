using System.Text.Json;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Android.Graphics;

/**
Name: Danny & Jordyn
Date: 9/26/2023
Description: Lab 2, but now with a remote database
Bugs: After clicking on Collect Statistics, Add, Edit and Delete no longer work
Reflection: I should've started this the day it was assigned!!!! I especially had problems
with Update(), and I should've learned how to use the debugger more
**/

namespace Lab2;

public class Database : IDatabase
{
    // Collection to store airports.
    public ObservableCollection<Airport> airports { get; set; } = new();

    //public ObservableCollection<Movie> SelectAllMovies() => Airport;

    //Declare the string connection
    //private static Random rng = new();
    private String connString;

    // File-related variables.
    string filename = "airports.db.txt";
    string mainDir = FileSystem.Current.AppDataDirectory;

    // JSON serialization options.
    private JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

    public Database()
    {
        // Constructor initializes and sets up the database.
        // It also ensures that the file exists or creates it if not.
        connString = GetConnectionString();
        CreateTable(connString);
        WriteAirports(); // This writes the initial empty collection to the file.
        filename = String.Format("{0}/{1}", mainDir, filename);
        
    }

    // Reads and deserializes airport data from the file.
    public ObservableCollection<Airport> SelectAllAirports()
    {
        airports.Clear();
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        // using() ==> disposable types are properly disposed of, even if there is an exception thrown
        using var cmd = new NpgsqlCommand("SELECT Id, City, DateVisited, Rating FROM airports", conn);
        using var reader = cmd.ExecuteReader(); // used for SELECT statement, returns a forward-only traversable object
        while (reader.Read()) // each time through we get another row in the table (i.e., another Movie)
        {
            String Id = reader.GetString(0);
            String City = reader.GetString(1);
            DateTime DateVisited = reader.GetDateTime(2);
            Int32 Rating = reader.GetInt32(3);
            Airport airportToAdd = new(Id, City, DateVisited, Rating);
            airports.Add(airportToAdd);
            Console.WriteLine(airportToAdd);
        }
        return airports;
        //string jsonAirports;

        //// Use the JSON deserializer to check if the file exists.
        //if (File.Exists(filename))
        //{
        //    jsonAirports = File.ReadAllText(filename);
        //    airports = JsonSerializer.Deserialize<ObservableCollection<Airport>>(jsonAirports);
        //}
        //else
        //{
        //    // If the file doesn't exist, create a new empty collection and save it.
        //    ObservableCollection<Airport> airports = new();
        //    jsonAirports = JsonSerializer.Serialize(airports, options);
        //    File.WriteAllText(filename, jsonAirports);
        //}

        //return airports;
    }

    // Writes the airport data to the JSON file.
    public void WriteAirports()
    {
        string jsonAirports;
        try
        {
            jsonAirports = JsonSerializer.Serialize(airports, options);
            File.WriteAllText(filename, jsonAirports);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving airports to file: {ex.Message}");
        }
    }

    // Returns an airport with a given ID, or null if not found.
    public Airport SelectAirport(String id)
    {
        return airports.FirstOrDefault(a => a.Id == id);
    }

    // Deletes an airport with a given ID from the collection.
    public bool DeleteAirport(String id) //Do we change this to Airport airports? check slide #31
    {
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        using var cmd = new NpgsqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "DELETE FROM airports WHERE Id = @Id";
        cmd.Parameters.AddWithValue("Id", id);
        int numDeleted = cmd.ExecuteNonQuery();
        if (numDeleted > 0)
        {
            SelectAllAirports(); // Question: why do we have to do this?
        }
        return numDeleted > 0;
        //// Check if the airport with the specified ID exists.
        //var airportToDelete = airports.FirstOrDefault(a => a.Id == id);

        //if (airportToDelete == null)
        //{
        //    return false; // Airport not found.
        //}

        //// Remove the airport from the collection.
        //airports.Remove(airportToDelete);
        //return true; // No errors.
    }

    // Inserts a new airport into the collection.
    public bool InsertAirport(Airport airport)
    {
        try
        {
            using var conn = new NpgsqlConnection(connString); // conn, short for connection, is a connection to the database
            conn.Open(); // open the connection ... now we are connected!
            var cmd = new NpgsqlCommand(); // create the sql command
            cmd.Connection = conn; // commands need a connection, an actual command to execute
            cmd.CommandText = "INSERT INTO airports (Id, city, DateVisited, Rating) VALUES (@Id, @city, @DateVisited, @Rating)";
            cmd.Parameters.AddWithValue("Id", airport.Id);
            cmd.Parameters.AddWithValue("City", airport.City);
            cmd.Parameters.AddWithValue("DateVisited", airport.DateVisited);
            cmd.Parameters.AddWithValue("Rating", airport.Rating);
            cmd.ExecuteNonQuery(); // used for INSERT, UPDATE & DELETE statements - returns # of affected rows
            SelectAllAirports();
        }
        catch (Npgsql.PostgresException pe)
        {
            Console.WriteLine("Insert failed, {0}", pe);
            return false;
        }
        return true;
        //try
        //{
        //    // Check if an airport with the same ID already exists.
        //    if (airports.Any(a => a.Id == airport.Id))
        //    {
        //        return false; // Airport with the same ID already exists.
        //    }

        //    // Add the airport to the collection.
        //    airports.Add(airport);
        //    return true; // Airport added successfully.
        //}
        //catch (Exception ex)
        //{
        //    // Handle any exceptions that may occur during insertion.
        //    Console.WriteLine($"Error while adding airport: {ex.Message}");
        //    return false; // Airport insertion failed due to an exception.
        //}
    }

    // Updates an existing airport's information.
    public bool UpdateAirport(Airport airportToUpdate, String city, DateTime DateVisited, int rating)
    {
        try
        {
            using var conn = new NpgsqlConnection(connString); // conn, short for connection, is a connection to the database
            conn.Open(); // open the connection ... now we are connected!
            var cmd = new NpgsqlCommand(); // create the sql commaned
            cmd.Connection = conn; // commands need a connection, an actual command to execute
            cmd.CommandText = "UPDATE airpots SET DateVisited = @DateVisited, City = @City, Rating = @Rating WHERE Id = @Id;";
            cmd.Parameters.AddWithValue("Id", airportToUpdate.Id);
            cmd.Parameters.AddWithValue("City", airportToUpdate.City);
            cmd.Parameters.AddWithValue("DateVisited", airportToUpdate.DateVisited);
            cmd.Parameters.AddWithValue("Rating", airportToUpdate.Rating); //I don't know if the city, date, and rating need to have airportToUpdate
            var numAffected = cmd.ExecuteNonQuery();
            SelectAllAirports();
        }
        catch (Npgsql.PostgresException pe)
        {
            Console.WriteLine("Update failed, {0}", pe);
            return false;
        }
        return true;
        //foreach (var existingAirport in airports)
        //{
        //    if (airport.Id == existingAirport.Id)
        //    {
        //        // Update the airport with the new information.
        //        existingAirport.City = airport.City;
        //        existingAirport.DateVisited = airport.DateVisited;
        //        existingAirport.Rating = airport.Rating;

        //        return true; // Airport updated successfully.
        //    }
        //}

        //return false; // Airport not found.
    }
    // Builds a ConnectionString, which is used to connect to the database
    static String GetConnectionString()
    {
        var connStringBuilder = new NpgsqlConnectionStringBuilder();
        connStringBuilder.Host = "software-engineering-cluster-13039.5xj.cockroachlabs.cloud";
        connStringBuilder.Port = 26257;
        connStringBuilder.SslMode = SslMode.VerifyFull;
        connStringBuilder.Username = "dannymoczynski"; // won't hardcode this in your app
        connStringBuilder.Password = "EfVIS0j8l5YFWsK3XdS0FA"; // need to hardcode password
        connStringBuilder.Database = "defaultdb";
        connStringBuilder.ApplicationName = "whatever"; // ignored, apparently
        connStringBuilder.IncludeErrorDetail = true;
        return connStringBuilder.ConnectionString;
    }
    // Fetches the password from the user secrets store (um, this works in VS, but not in the beta of VSC's C# extension)
    //static String FetchPassword()
    //{
    //    //IConfiguration config = new ConfigurationBuilder().AddUserSecrets<Database>().Build();
    //    //return config["CockroachDBPassword"] ?? ""; // this works in VS, not VSC
    //}
    // This is pretty straightforward - get a connection, open it, and make a command to invoke CREATE TABLE
    // We execute the command at the same time as we create it (we don't need to store it)
    // ExecuteNonQuery() means we are executing a SQL statement that is not a query (doesn't involve SELECT)
    // We won't actually use this in MovieExtravaganza-SQLReady, we'll create the table at the command line
    static void CreateTable(string connString)
    {
        using var conn = new NpgsqlConnection(connString); // a conn represents a connection to the database
        conn.Open(); // open the connection ... now we are connected!
        new NpgsqlCommand("CREATE TABLE IF NOT EXISTS airports (id VARCHAR(4) PRIMARY KEY, city VARCHAR(25), DateVisited TIMESTAMP, rating INT)", conn).ExecuteNonQuery();
    }

    //internal bool DeleteAirport(ObservableCollection<Airport> airports)
    //{
    //    throw new NotImplementedException();
    //}
}


