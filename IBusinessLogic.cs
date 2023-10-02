using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
/**
Name: Danny Moczynski/ Jordyn Henrich
Date: 10/1/2023
Description: Lab 2, but now with a remote database
Bugs: No Bugs
Reflection: Nothing changed in this class
**/
{
    public interface IBusinessLogic
    {
        bool AddAirport(string id, string city, DateTime dateVisited, int rating);
        bool DeleteAirport(string id);
        bool EditAirport(string id, string city, DateTime dateVisited, int rating);
        string CalculateStatistics();
        Airport FindAirport(string id);
        ObservableCollection<Airport> GetAirports();
    }
}
