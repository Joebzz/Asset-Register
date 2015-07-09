using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace AssetRegister.Poco
{
    [TableName("LookupTrack")] // Name of the Table in the Database
    [PrimaryKey("IdtTrack")] // Primary Key of the Table
	public class Track
	{
        private PetaPoco.Database db = new PetaPoco.Database("connectionString"); // Use the connectionString from the Web.Config to connect to the DB
        
        // Variables below are dirtectly related to the Column Names of the DB Table
        // These are the Variables that all Track Objects will have
        public int IdtTrack { get; set; }
        public string NamTrack { get; set; }
        public string IPSubnet { get; set; }
       
        /// <summary>
        ///  Returns the Track found with the provided ID
        /// </summary>
        /// <param name="id">The ID of the Track</param>
        /// <returns>Track</returns>
        public Track getTrack(int id)
        {
            var Track = db.FirstOrDefault<Track>("SELECT * FROM LookupTrack WHERE IdtTrack = '" + id + "'");
            return Track;
        }

        /// <summary>
        /// Used by the drop down list of Track's
        /// </summary>
        /// <returns>All Track's from the DB table</returns>
        public List<Track> getTracks()
        {
            var Tracks = db.Fetch<Track>("SELECT * FROM LookupTrack");
            return Tracks;
        }
	}
}