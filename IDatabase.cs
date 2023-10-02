using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
/**
Name: Danny Moczynski/ Jordyn Henrich
Date: 10/1/2023
Description: Lab 2, but now with a remote database
Bugs: No Bugs
Reflection: Nothing changed from lab 2.
**/
{
    internal interface IDatabase
    {
        public interface IDatabase
        {
            List<Airport> SelectAllAirports();
            Airport SelectAirport(string id);
            bool InsertAirport(Airport airport);
            bool UpdateAirport(Airport airport);
            bool DeleteAirport(string id);
        }
    }
}
