using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    /// <summary>
    /// Name: Danny Moczynski/ Jordyn Henrich
    /// Date: 10/1/2023
    /// Description: Lab 2, but now with a remote database
    /// Bugs: No Bugs
    /// Reflection: Nothing changed from lab 2.
    /// </summary>
    internal interface IDatabase
    {
        /// <summary>
        /// Selects all airports from the database.
        /// </summary>
        /// <returns>A list of all airports.</returns>
        ObservableCollection<Airport> SelectAllAirports();

        /// <summary>
        /// Selects an airport by its ID from the database.
        /// </summary>
        /// <param name="id">The Airport ID to select.</param>
        /// <returns>The selected airport or null if not found.</returns>
        Airport SelectAirport(string id);

        /// <summary>
        /// Inserts an airport into the database.
        /// </summary>
        /// <param name="airport">The airport to insert.</param>
        /// <returns>True if the airport is inserted successfully, false otherwise.</returns>
        bool InsertAirport(Airport airport);

        /// <summary>
        /// Updates an airport in the database.
        /// </summary>
        /// <param name="airport">The airport to update.</param>
        /// <returns>True if the airport is updated successfully, false otherwise.</returns>
        public bool UpdateAirport(Airport airportToUpdate, String city, DateTime DateVisited, int rating);

        /// <summary>
        /// Deletes an airport by its ID from the database.
        /// </summary>
        /// <param name="id">The Airport ID to delete.</param>
        /// <returns>True if the airport is deleted successfully, false otherwise.</returns>
        bool DeleteAirport(string id);
    }
}
