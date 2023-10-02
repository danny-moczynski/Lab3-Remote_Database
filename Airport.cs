using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lab2;
/**
Name: Danny Moczynski/ Jordyn Henrich
Date: 10/1/2023
Description: Lab 2, but now with a remote database
Bugs: No Bugs
Reflection: Nothing changed in this class but we did fix from the previous lab to update
the city when edit airport was clicked to create a completely functional app based off 
the requirements.
**/

// The Airport class represents an airport with properties and validation.
public class Airport : INotifyPropertyChanged
{
    // Private fields to store airport properties.
    private String id;
    private String city;
    private DateTime dateVisited;
    private int rating;

    // Event used to notify property changes to subscribers.
    public event PropertyChangedEventHandler PropertyChanged;

    // Property for Airport ID with validation.
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

    // Property for Airport City.
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

    // Property for Date Visited with validation.
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

    // Property for Airport Rating with validation.
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

    // Constructor with parameters to initialize an Airport object.
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

    // Override of ToString() method to provide a string representation of the Airport object.
    public override String ToString()
    {
        return $"{id} - {city}, - {dateVisited:MM/dd/yyyy}, {rating}";
    }
}
