using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace AssetRegister.Poco
{
    [TableName("LookupAssetType")] // Name of the Table in the Database
    [PrimaryKey("IdtAssetType")] // Primary Key of the Table
    public class AssetType
	{
        private PetaPoco.Database db = new PetaPoco.Database("connectionString"); // Use the connectionString from the Web.Config to connect to the DB

        // Variables below are dirtectly related to the Column Names of the DB Table
        // These are the Variables that all AssetType Objects will have
        public int IdtAssetType { get; set; }
        public string NamAssetType { get; set; }

        /// <summary>
        ///  Returns the Asset Type found with the provided ID
        /// </summary>
        /// <param name="id">The ID of the AssetType</param>
        /// <returns>Asset Type</returns>
        public AssetType getAssetType(int id)
        {            
            var AssetType = db.FirstOrDefault<AssetType>("WHERE IdtAssetType = '" + id + "'");
            return AssetType;
        }

        /// <summary>
        /// Used by the drop down list of Asset Types
        /// </summary>
        /// <returns>All Asset Types from the DB table</returns>
        public List<AssetType> getAssetTypes()
        {
            var AssetTypes = db.Fetch<AssetType>();
            return AssetTypes;
        }
	}
}