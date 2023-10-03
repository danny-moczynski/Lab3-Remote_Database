using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.ErrorReporting;
using static Lab2.ErrorReporting;

namespace Lab2
{
    /// <summary>
    /// Name: Danny Moczynski/ Jordyn Henrich
    /// Date: 10/1/2023
    /// Description: Lab 2, but now with a remote database
    /// Bugs: No Bugs
    /// Reflection: Nothing changed in this class
    /// </summary>
    public interface IBusinessLogic
    {
        /// <summary>
        /// Adds an airport with the provided information.
        /// </summary>
        /// <param name="id">The Airport ID.</param>
        /// <param name="city">The Airport City.</param>
        /// <param name="dateVisited">The Date Visited.</param>
        /// <param name="rating">The Airport Rating.</param>
        /// <returns>True if the airport is added successfully, false otherwise.</returns>
        bool AddAirport(string id, string city, DateTime dateVisited, int rating);

        /// <summary>
        /// Deletes an airport with the provided ID.
        /// </summary>
        /// <param name="id">The Airport ID to delete.</param>
        /// <returns>True if the airport is deleted successfully, false otherwise.</returns>
        bool DeleteAirport(string id);

        /// <summary>
        /// Edits an airport with the provided information.
        /// </summary>
        /// <param name="id">The Airport ID to edit.</param>
        /// <param name="city">The Airport City.</param>
        /// <param name="dateVisited">The Date Visited.</param>
        /// <param name="rating">The Airport Rating.</param>
        /// <returns>True if the airport is edited successfully, false otherwise.</returns>
        bool EditAirport(string id, string city, DateTime dateVisited, int rating);

        /// <summary>
        /// Calculates statistics about the airports.
        /// </summary>
        /// <returns>A string containing statistics information.</returns>
        string CalculateStatistics();

        /// <summary>
        /// Finds an airport by its ID.
        /// </summary>
        /// <param name="id">The Airport ID to find.</param>
        /// <returns>The found airport or null if not found.</returns>
        Airport FindAirport(string id);

        /// <summary>
        /// Gets a collection of all airports.
        /// </summary>
        /// <returns>An observable collection of airports.</returns>
        ObservableCollection<Airport> GetAirports();

    }
}
