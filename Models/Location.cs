using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace AssetRegister.Poco
{
    [TableName("LookupLocation")] // Name of the Table in the Database
    [PrimaryKey("IdtLocation")] // Primary Key of the Table
    public class Location
	{
        private PetaPoco.Database db = new PetaPoco.Database("connectionString"); // Use the connectionString from the Web.Config to connect to the DB

        // Variables below are dirtectly related to the Column Names of the DB Table
        // These are the Variables that all Location Objects will have
        public int IdtLocation { get; set; }
        public string NamLocation { get; set; }

        /// <summary>
        ///  Returns the Location found with the provided ID
        /// </summary>
        /// <param name="id">The ID of the Location</param>
        /// <returns>Location</returns>
        public Location getLocation(int id)
        {            
            var Location = db.FirstOrDefault<Location>("WHERE IdtLocation = '" + id + "'"); 
            return Location;
        }

        /// <summary>
        /// Used by the drop down list of Location
        /// </summary>
        /// <returns>All Locations from the DB table</returns>
        public List<Location> getLocations()
        {
            var Locations = db.Fetch<Location>();
            return Locations;
        }
	}
}