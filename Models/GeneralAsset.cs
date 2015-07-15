using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
using System.Data;

namespace AssetRegister.Poco
{
    [TableName("General_Assets")] // Name of the Table in the Database
    [PrimaryKey("IdtAsset")] // Primary Key of the Table
	public class GeneralAsset
	{
        private PetaPoco.Database db = new PetaPoco.Database("connectionString"); // Use the connectionString from the Web.Config to connect to the DB

        // Variables below are dirtectly related to the Column Names of the DB Table
        // These are the Variables that all Asset Objects will have
		public int IdtAsset {get; set;}
		public int IdtTrack {get; set;}
        public int IdtAssetType { get; set; }
		public int IdtLocation {get; set;}
        public string Description { get; set; }
        public int NumAssets { get; set; }		
        public DateTime? ShipDate { get; set; }
        public string Other { get; set; }
        public int CostValue { get; set; }
        public int CurrentValue { get; set; }
        public string Comments { get; set; }
        public int Inactive { get; set; }

        // Results Columns used for the Export functionality
        // These are not column names that come from the DB table
        [ResultColumn]
        public string NamTrack { get; set; }
        [ResultColumn]
        public string NamAssetType { get; set; }
        [ResultColumn]
        public string NamLocation { get; set; }

        /// <summary>
        /// Function that returns a Single Asset for the id passed in
        /// </summary>
        /// <param name="id">The ID of the Asset</param>
        /// <returns>The Asset foor the given ID</returns>
        public GeneralAsset getAsset(int id)
        {
            // Sql WHERE To set what to look for in the DB
            var Asset = db.FirstOrDefault<GeneralAsset>("WHERE IdtAsset = '" + id + "'");
            return Asset;
        }

        /// <summary>
        /// Function to return all of the Assets in the IT_Assets Table
        /// </summary>
        /// <returns>All of the assets for the gridview databind function</returns>
        public List<GeneralAsset> getAssets()
        {
            // Returns all data from the IT_Assets Table
            List<GeneralAsset> Assets = db.Fetch<GeneralAsset>();
            return Assets;
        }

        /// <summary>
        /// Function to return all the Assets that are Inactive if 1 is passed in or Active if 0 is passed
        /// </summary>
        /// <param name="inactive">1 or 0 to sdhow Inactive Assets or Active</param>
        /// <returns>List of the assets for the gridview databind function</returns>
        public List<GeneralAsset> getAssets(int inactive)
        {
            List<GeneralAsset> Assets = db.Fetch<GeneralAsset>("WHERE Inactive=" + inactive + " ORDER BY ShipDate ASC"); // Order the results by ShipDate
            return Assets;
        }

        /// <summary>
        /// Function to return all the Assets that meet the search criteria entered
        /// Values are passed in as empty strings if they are not needed for the search
        /// </summary>
        /// <param name="description">Description String used to search for the description</param>
        /// <returns>List of the assets for the gridview databind function</returns>
        public List<GeneralAsset> getAssets(string description)
        {
            // Create the sql string that will be used to query the Table 
            string sqlStr = " WHERE ";

            // Check what values are being passed in
            if (description != "") // Search on the Description using SQL LIKE
                sqlStr += "Description LIKE '%" + description + "%'";           

            List<GeneralAsset> Assets = db.Fetch<GeneralAsset>(sqlStr + " ORDER BY ShipDate ASC"); // Order the results by ShipDate
            return Assets;
        }        

        /// <summary>
        /// Function to return the Assets with the provided variables
        /// ? means the value can be passed in as a null value
        /// </summary>
        /// <param name="trkId">ID of the Track can be null</param>
        /// <param name="assTypeId">ID of the Asset Type can be null</param>
        /// <param name="locId">ID of the Location can be null</param>
        /// <param name="showInactive">Boolean Equivelant either a 1 (show Inactive) or 0 (dont show inactive)</param>
        /// <returns>List of the assets for the gridview databind function</returns>
        public List<GeneralAsset> getAssets(int? trkId, int? assTypeId, int? locId, int showInactive)
        {
            // Create the sql string that will be used to query the Table 
            string sqlStr = " WHERE Inactive=" + showInactive;
            
            if(trkId.HasValue)
                sqlStr += " AND IdtTrack=" + trkId.Value;
            
            // Append depending on what has been passed in to the sql string already, this has to be done to add in the 'AND'
            if (assTypeId.HasValue)
                sqlStr += " AND IdtAssetType=" + assTypeId.Value;

            // Use OR to se if any previous value has been passed in, this has to be done to add in the 'AND'
            if (locId.HasValue)
                sqlStr += " AND IdtLocation=" + locId.Value;

            List<GeneralAsset> Assets = db.Fetch<GeneralAsset>(sqlStr + " ORDER BY ShipDate ASC"); // Order the results by ShipDate in ascending order
            return Assets;
        }

        /// <summary>
        /// Function to return the Assets with the provided variables
        /// ? means the value can be passed in as a null value
        /// </summary>
        /// <param name="trkId">ID of the Track can be null</param>
        /// <param name="assTypeId">ID of the Asset Type can be null</param>
        /// <param name="locId">ID of the Location can be null</param>
        /// <param name="showInactive">Boolean Equivelant either a 1 (show Inactive) or 0 (dont show inactive)</param>
        /// <returns>List of the assets for the export functions</returns>
        public List<GeneralAssetExport> getAssetsForExport(int? trkId, int? assTypeId, int? locId, int showInactive)
        {
            string sqlSelectStr = "SELECT NamTrack, NamAssetType, NamLocation, Description, NumAssets, CONVERT(VARCHAR(11),ShipDate, 106) as ShipDate, Comments, CostValue, CurrentValue FROM General_Assets ass LEFT JOIN LookupTrack lt on ass.IdtTrack=lt.IdtTrack JOIN LookupAssetType lat on lat.IdtAssetType=ass.IdtAssetType JOIN LookupLocation ll on ll.IdtLocation=ass.IdtLocation";

            // Create the sql string that will be used to query the Table 
            string sqlStr = " WHERE Inactive=" + showInactive;

            if (trkId.HasValue)
                sqlStr += " AND ass.IdtTrack=" + trkId.Value;

            // Append depending on what has been passed in to the sql string already, this has to be done to add in the 'AND'
            if (assTypeId.HasValue)
                sqlStr += " AND ass.IdtAssetType=" + assTypeId.Value;

            // Use OR to se if any previous value has been passed in, this has to be done to add in the 'AND'
            if (locId.HasValue)
                sqlStr += " AND ass.IdtLocation=" + locId.Value;

            var Assets = db.Fetch<GeneralAssetExport>(sqlSelectStr + sqlStr + " ORDER BY ass.IdtTrack");

            return Assets;
        }

        /// <summary>
        /// Update the Asset
        /// </summary>
        /// <param name="asset">The Asset that is to be updated</param>
        public void UpdateAsset(GeneralAsset asset)
        {
            db.Update(asset);
        }

        /// <summary>
        /// Create the Asset
        /// </summary>
        /// <param name="asset">The Asset that is to be created</param>
        public void CreateAsset(GeneralAsset asset)
        {
            db.Save(asset);
        }
    }

    public class GeneralAssetExport
    {
        // Variables for the Results Export Functionality 
        // Used to creat the Excel File from the Data
        public string NamTrack { get; set; }
        public string NamAssetType { get; set; }
        public string NamLocation { get; set; }
        public string Description { get; set; }
        public string NumAssets { get; set; }        
        public string ShipDate { get; set; }
        public string Other { get; set; }
        public string CostValue { get; set; }
        public string CurrentValue { get; set; }
    }
}