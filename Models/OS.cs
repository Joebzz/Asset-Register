using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace AssetRegister.Poco
{
    [TableName("LookupOS")] // Name of the Table in the Database
    [PrimaryKey("IdtOS")] // Primary Key of the Table
    public class OS
    {
        private PetaPoco.Database db = new PetaPoco.Database("connectionString"); // Use the connectionString from the Web.Config to connect to the DB

        // Variables below are dirtectly related to the Column Names of the DB Table
        // These are the Variables that all Track Objects will have
        public int IdtOS { get; set; }
        public string NamOS { get; set; }
        
        /// <summary>
        ///  Returns the OS found with the provided ID
        /// </summary>
        /// <param name="id">The ID of the OS</param>
        /// <returns>OS</returns>
        public OS getOS(int id)
        {
            var OS = db.FirstOrDefault<OS>("SELECT * FROM LookupOS WHERE IdtOS = '" + id + "'");
            return OS;
        }

        /// <summary>
        /// Used by the drop down list of OS's
        /// </summary>
        /// <returns>All OS's from the DB table</returns>
        public List<OS> getOSs()
        {
            var OS = db.Fetch<OS>("SELECT * FROM LookupOS");
            return OS;
        }
    }
}