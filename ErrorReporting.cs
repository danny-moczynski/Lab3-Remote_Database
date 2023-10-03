using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Lab2;

internal class ErrorReporting
{
    /// <summary>
    /// Represents possible errors that can occur when adding an airport.
    /// </summary>
    public enum AirportAdditionError
    {
        /// <summary>
        /// The length of the airport ID is invalid.
        /// </summary>
        InvalidIdLength,

        /// <summary>
        /// The provided rating value is invalid.
        /// </summary>
        InvalidNumStars,

        /// <summary>
        /// The provided date is invalid.
        /// </summary>
        InvalidDate,

        /// <summary>
        /// The length of the city name is invalid.
        /// </summary>
        InvalidCityLength,

        /// <summary>
        /// An error occurred during database addition.
        /// </summary>
        DBAdditionError,

        /// <summary>
        /// An airport with the same ID already exists.
        /// </summary>
        ExistingAirportError,

        /// <summary>
        /// No error occurred during the airport addition.
        /// </summary>
        NoError
    }

    /// <summary>
    /// Represents possible errors that can occur when deleting an airport.
    /// </summary>
    public enum AirportDeletionError
    {
        /// <summary>
        /// The airport to delete was not found.
        /// </summary>
        AirportNotFound,

        /// <summary>
        /// An error occurred during database deletion.
        /// </summary>
        DBDeletionError,

        /// <summary>
        /// No error occurred during the airport deletion.
        /// </summary>
        NoError
    }

    /// <summary>
    /// Represents possible errors that can occur when editing an airport.
    /// </summary>
    public enum AirportEditError
    {
        /// <summary>
        /// The airport to edit was not found.
        /// </summary>
        AirportNotFound,

        /// <summary>
        /// An invalid field value was provided for editing.
        /// </summary>
        InvalidFieldError,

        /// <summary>
        /// An error occurred during database editing.
        /// </summary>
        DBEditError,

        /// <summary>
        /// No error occurred during the airport editing.
        /// </summary>
        NoError
    }
}

