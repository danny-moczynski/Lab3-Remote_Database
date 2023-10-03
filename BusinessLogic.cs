using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using static Lab2.ErrorReporting;

namespace Lab2
/**
Name: Danny Moczynski/ Jordyn Henrich
Date: 10/1/2023
Description: Lab 2, but now with a remote database
Bugs: No output for bad user error. App will not crash or add, edit, or 
delete airport - just no error prints. Have enums, but could could not get
messages to print so reverted to original.
Reflection: The Business Logic class did not change.
**/
{
    public class BusinessLogic : IBusinessLogic

    {
        // The BusinessLogic class talks to the Database
        private Database database;
        private const int bronze = 42;
        private const int silver = 84;
        private const int gold = 125;

        // Create an observable collection of airports that returns the database method of all the airports
        public ObservableCollection<Airport> Airports
        {
            get { return database.SelectAllAirports(); }
        }
        // Constructor
        public BusinessLogic()
        {
            database = new Database();
        }
        /// <summary>
        /// Add an airport that has information based on the parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="city"></param>
        /// <param name="dateVisited"></param>
        /// <param name="rating"></param>
        /// <returns> A boolean that checks if the result of the airport is true or false to add it or to not add it </returns>
        public bool AddAirport(string id, string city, DateTime dateVisited, int rating)
        {
            // Validate the input
            if (id.Length < 3 || id.Length > 4)
            {
                return false;
            }

            if (city.Length > 25)
            {
                return false;
            }

            if (rating < 1 || rating > 5)
            {
                return false;
            }

            // Call the database to add the airport
            bool result = database.InsertAirport(new Airport(id, city, dateVisited, rating));
            return result;
        }

        /// <summary>
        /// Check if the id exists and if it does, then delete the airport.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the airport id exists, false if not</returns>
        public bool DeleteAirport(string id)
        {
            // Call the database to delete the airport
            return database.DeleteAirport(id);

        }
        /// <summary>
        /// Create a new airport with the changes set to the new airport.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="city"></param>
        /// <param name="dateVisited"></param>
        /// <param name="rating"></param>
        /// <returns> True if the user chooses to edit the airport and calls the database update airport method to change the user's airport information</returns>
        public bool EditAirport(string id, string city, DateTime dateVisited, int rating)
        {
            // Create a new airport object and return updated airport
            Airport airport = new Airport(id, city, dateVisited, rating);
            return database.UpdateAirport(airport, city, dateVisited, rating);

        }

        /// <summary>
        /// This method calls the database method to find the airport's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The airport found </returns>
        public Airport FindAirport(string id)
        {
            return database.SelectAirport(id);
        }
        /// <summary>
        /// Checks the count of airports and returns the appropriate sentence for the user to see
        /// </summary>
        /// <returns> A string that tells the user how many airports they have visited and their next threshold</returns>
        public string CalculateStatistics()
        {
            // set a variable to get the count of how many airports have been added
            int airportCount = GetAirports().Count();


            // Check the number of airports for respective medal
            if (airportCount == 1)
            {
                return (airportCount + " airport visited; " + (bronze - airportCount) + " airports remaining until reaching bronze");
            }
            if (airportCount < bronze)
            {
                return (airportCount + " airports visited; " + (bronze - airportCount) + " airports remaining until reaching bronze");
            }
            else if (airportCount < silver)
            {
                return (airportCount + " airports visited; " + (silver - airportCount) + " airports remaining until reaching silver");
            }
            else if (airportCount < gold)
            {
                return (airportCount + " airports visited; " + (gold - airportCount) + " airports remaining until reaching gold");
            }
            else
            {
                return (airportCount + " airports visited; Congrats you have visited all of the Airports in Wisconsin");
            }
        }
        /// <summary>
        /// Calls the database method to select every airport
        /// </summary>
        /// <returns> An observable collection of airports</returns>
        public ObservableCollection<Airport> GetAirports()
        {
            // This is the businesslogic talking to the database
            return database.SelectAllAirports();
        }
    }
}



