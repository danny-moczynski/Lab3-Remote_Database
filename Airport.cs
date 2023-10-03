using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Reflection: Nothing changed in this class but we did fix from the previous lab to update
    /// the city when edit airport was clicked to create a completely functional app based off 
    /// the requirements.
    /// </summary>
    /// <remarks>
    /// The Airport class represents an airport with properties and validation.
    /// </remarks>
    public class Airport : INotifyPropertyChanged
    {
        // Private fields to store airport properties.
        private String id;
        private String city;
        private DateTime dateVisited;
        private int rating;

        // Event used to notify property changes to subscribers.
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the Airport ID with validation.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the ID is not a 4-character string starting with 'K'.</exception>
        public string Id
        {
            get { return id; }
            set
            {
                // Validate and set the ID property.
                if (value.Length == 4 && value[0] == 'K')
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
                else
                {
                    throw new ArgumentException("The ID property must be a 4-character string starting with 'K'.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Airport City.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the city length exceeds 25 characters.</exception>
        public string City
        {
            get { return city; }
            set
            {
                if (city.Length < 25)
                {
                    city = value;
                    OnPropertyChanged(nameof(City));
                }
                else
                {
                    throw new ArgumentException("City Length too long");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Date Visited with validation.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the date visited is in the future.</exception>
        public DateTime DateVisited
        {
            get { return dateVisited; }
            set
            {
                // Validate and set the DateVisited property.
                if (value <= DateTime.Today)
                {
                    dateVisited = value;
                    OnPropertyChanged(nameof(DateVisited));
                }
                else
                {
                    throw new ArgumentException("Invalid date visited");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Airport Rating with validation.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the rating is not between 1 and 5.</exception>
        public int Rating
        {
            get { return rating; }
            set
            {
                // Validate and set the Rating property.
                if (value > 0 && value < 6)
                {
                    rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
                else
                {
                    throw new ArgumentException("Rating must be between 1 and 5.");
                }
            }
        }

        // Event handler for property changes.
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Initializes a new instance of the Airport class with specified properties.
        /// </summary>
        /// <param name="id">The Airport ID.</param>
        /// <param name="city">The Airport City.</param>
        /// <param name="dateVisited">The Date Visited.</param>
        /// <param name="rating">The Airport Rating.</param>
        public Airport(string id, string city, DateTime dateVisited, int rating)
        {
            this.id = id;
            this.city = city;
            this.dateVisited = dateVisited;
            this.rating = rating;
        }

        // Default constructor (unused in the code).
        public Airport()
        {
            // Example of creating an Airport object with default values.
            Airport airport = new Airport("KATW", "Appleton", new DateTime(), 5);
        }

        /// <summary>
        /// Provides a string representation of the Airport object.
        /// </summary>
        /// <returns>A formatted string representing the Airport object.</returns>
        public override String ToString()
        {
            return $"{id} - {city}, - {dateVisited:MM/dd/yyyy}, {rating}";
        }
    }
}
