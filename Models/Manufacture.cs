using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace AssetRegister.Poco
{
    [TableName("LookupManufacture")] // Name of the Table in the Database
    [PrimaryKey("IdtManufacture")] // Primary Key of the Table
    public class Manufacture
	{
        private PetaPoco.Database db = new PetaPoco.Database("connectionString"); // Use the connectionString from the Web.Config to connect to the DB

        // Variables below are dirtectly related to the Column Names of the DB Table
        // These are the Variables that all Manufacture Objects will have
		public int IdtManufacture {get; set;}
		public string NamManufacture {get; set;}
       
        /// <summary>
        ///  Returns the Manufacture found with the provided ID
        /// </summary>
        /// <param name="id">The ID of the Manufacture</param>
        /// <returns>Manufacture</returns>
        public Manufacture getManufacture(int id)
        {
            var Manufacture = db.FirstOrDefault<Manufacture>("SELECT * FROM LookupManufacture WHERE IdtManufacture = '" + id + "'");
            return Manufacture;
        }

        /// <summary>
        /// Used by the drop down list of Manufactures
        /// </summary>
        /// <returns>All Manufactures from the DB table</returns>
        public List<Manufacture> getManufactures()
        {
            var Manufactures = db.Fetch<Manufacture>("SELECT * FROM LookupManufacture");
            return Manufactures;
        }
	}
}