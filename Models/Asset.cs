using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
using System.Data;

namespace AssetRegister.Poco
{
    [TableName("IT_Assets")] // Name of the Table in the Database
    [PrimaryKey("IdtAsset")] // Primary Key of the Table
	public class Asset
	{
        private PetaPoco.Database db = new PetaPoco.Database("connectionString"); // Use the connectionString from the Web.Config to connect to the DB

        // Variables below are dirtectly related to the Column Names of the DB Table
        // These are the Variables that all Asset Objects will have
		public int IdtAsset {get; set;}
		public int IdtTrack {get; set;}
		public int IdtDeviceType {get; set;}
		public int IdtManufacture {get; set;}
        public int IdtOS { get; set; }
        public int IdtDepartment { get; set; }
		public string Model {get; set;}
		public string Description {get; set;}
		public string Hostname {get; set;}
		public string IPAddress {get; set;}
		public string ServiceTag {get; set;}
		public string ExpressCode {get; set;}
        public DateTime ShipDate { get; set; }
        public string Comments { get; set; }
        public int Inactive { get; set; }

        // Results Columns used for the Export functionality
        // These are not column names that come from the DB table
        [ResultColumn]
        public string NamTrack { get; set; }
        [ResultColumn]
        public string NamDeviceType { get; set; }
        [ResultColumn]
        public string NamManufacture { get; set; }
        [ResultColumn]
        public string NamOS { get; set; }        
        [ResultColumn]
        public string NamDepartment { get; set; } 

        /// <summary>
        /// Function that returns a Single Asset for the id passed in
        /// </summary>
        /// <param name="id">The ID of the Asset</param>
        /// <returns>The Asset foor the given ID</returns>
        public Asset getAsset(int id)
        {
            // Sql WHERE To set what to look for in the DB
            var Asset = db.FirstOrDefault<Asset>("WHERE IdtAsset = '" + id + "'");
            return Asset;
        }

        /// <summary>
        /// Function to return all of the Assets in the AssetRegister Table
        /// </summary>
        /// <returns>All of the assets for the gridview databind function</returns>
        public List<Asset> getAssets()
        {
            // Returns all data from the AssetRegister Table
            List<Asset> Assets = db.Fetch<Asset>();
            return Assets;
        }

        /// <summary>
        /// Function to return all the Assets that are Inactive if 1 is passed in or Active if 0 is passed
        /// </summary>
        /// <param name="inactive">1 or 0 to sdhow Inactive Assets or Active</param>
        /// <returns>List of the assets for the gridview databind function</returns>
        public List<Asset> getAssets(int inactive)
        {
            List<Asset> Assets = db.Fetch<Asset>("WHERE Inactive=" + inactive + " ORDER BY ShipDate ASC"); // Order the results by ShipDate
            return Assets;
        }

        /// <summary>
        /// Function to return all the Assets that meet the search criteria entered
        /// Values are passed in as empty strings if they are not needed for the search
        /// </summary>
        /// <param name="description">Description String used to search for the description</param>
        /// <param name="hostname">Hostname String used to search for the Hostname</param>
        /// <param name="ip">IP String used to search for the IP</param>
        /// <returns>List of the assets for the gridview databind function</returns>
        public List<Asset> getAssets(string description, string hostname, string ip)
        {
            // Create the sql string that will be used to query the Table 
            string sqlStr = " WHERE ";

            // Check what values are being passed in
            if (description != "" && ip == "" && hostname == "") // Search on the Description using SQL LIKE
                sqlStr += "Description LIKE '%" + description + "%'";
            else if (description == "" && hostname != "" && ip == "") // Search on the Hostname using SQL LIKE
                sqlStr += "Hostname LIKE '%" + hostname + "%'";
            else if (description == "" && hostname == "" && ip != "") // Search on the IPAddress using SQL LIKE
                sqlStr += "IPAddress LIKE '%" + ip + "%'";

            List<Asset> Assets = db.Fetch<Asset>(sqlStr + " ORDER BY ShipDate ASC"); // Order the results by ShipDate
            return Assets;
        }

        /// <summary>
        /// Function to return the Assets with the provided variables
        /// ? means the value can be passed in as a null value
        /// </summary>
        /// <param name="trkId">ID of the Track can be null</param>
        /// <param name="devId">ID of the Device Type can be null</param>
        /// <param name="osId">ID of the OS can be null</param>
        /// <param name="manId">ID of the Mnaufacture can be null</param>
        /// <param name="deptId">ID of the Department can be null</param>
        /// <param name="showInactive">Boolean Equivelant either a 1 (show Inactive) or 0 (dont show inactive)</param>
        /// <returns>List of the assets for the gridview databind function</returns>
        public List<Asset> getAssets(int? trkId, int? devId, int? osId, int? manId, int? deptId, int showInactive)
        {
            // Create the sql string that will be used to query the Table 
            string sqlStr = " WHERE Inactive=" + showInactive;
            
            if(trkId.HasValue)
                sqlStr += " AND IdtTrack=" + trkId.Value;
            
            // Append depending on what has been passed in to the sql string already, this has to be done to add in the 'AND'
            if (devId.HasValue)
                sqlStr += " AND IdtDeviceType=" + devId.Value;

            // Use OR to se if any previous value has been passed in, this has to be done to add in the 'AND'
            if (osId.HasValue)
                sqlStr += " AND IdtOS=" + osId.Value;

            // Use OR to se if any previous value has been passed in, this has to be done to add in the 'AND'
            if (manId.HasValue)
                sqlStr += " AND IdtManufacture=" + manId.Value;

            // Use OR to se if any previous value has been passed in, this has to be done to add in the 'AND'
            if (deptId.HasValue)
                sqlStr += " AND IdtDepartment=" + deptId.Value;

            List<Asset> Assets = db.Fetch<Asset>(sqlStr + " ORDER BY ShipDate ASC"); // Order the results by ShipDate in ascending order
            return Assets;
        }
        
        /// <summary>
        /// Function to return the Assets with the provided variables
        /// ? means the value can be passed in as a null value
        /// </summary>
        /// <param name="trkId">ID of the Track can be null</param>
        /// <param name="devId">ID of the Device Type can be null</param>
        /// <param name="osId">ID of the OS can be null</param>
        /// <param name="manId">ID of the Mnaufacture can be null</param>
        /// <param name="deptId">ID of the Department can be null</param>
        /// <param name="showInactive">Boolean Equivelant either a 1 (show Inactive) or 0 (dont show inactive)</param>
        /// <returns>List of tnhe assets for the export functions</returns>
        public IList<AssetExport> getAssetsForExport(int? trkId, int? devId, int? osId, int? manId, int? deptId, int showInactive)
        {            
            string sqlSelectStr = "SELECT NamTrack, NamDeviceType, NamManufacture, NamOS, NamDepartment, Model, Description, Hostname, IPAddress, ServiceTag, ExpressCode, ShipDate, Comments FROM AssetRegister ass LEFT JOIN LookupTrack lt on ass.IdtTrack=lt.IdtTrack JOIN LookupDeviceType ldt on ldt.IdtDeviceType=ass.IdtDeviceType JOIN LookupManufacture lm on lm.IdtManufacture=ass.IdtManufacture JOIN LookupOS lo on lo.IdtOS=ass.IdtOS JOIN LookupDepartment ld on ld.IdtDepartment=ass.IdtDepartment";

            // Create the sql string that will be used to query the Table 
            string sqlStr = " WHERE Inactive=" + showInactive;

            if (trkId.HasValue)
                sqlStr += " AND ass.IdtTrack=" + trkId.Value;

            // Append depending on what has been passed in to the sql string already, this has to be done to add in the 'AND'
            if (devId.HasValue)
                sqlStr += " AND ass.IdtDeviceType=" + devId.Value;

            // Use OR to se if any previous value has been passed in, this has to be done to add in the 'AND'
            if (osId.HasValue)
                sqlStr += " AND ass.IdtOS=" + osId.Value;

            // Use OR to se if any previous value has been passed in, this has to be done to add in the 'AND'
            if (manId.HasValue)
                sqlStr += " AND ass.IdtManufacture=" + manId.Value;

            // Use OR to se if any previous value has been passed in, this has to be done to add in the 'AND'
            if (deptId.HasValue)
                sqlStr += " AND ass.IdtDepartment=" + deptId.Value;

            var Assets = db.Fetch<AssetExport>(sqlSelectStr + sqlStr);

            return Assets;
        }
        
        /// <summary>
        /// Update the Asset
        /// </summary>
        /// <param name="asset">The Asset that is to be updated</param>
        public void UpdateAsset(Asset asset)
        {
            db.Update(asset);
        }

        /// <summary>
        /// Create the Asset
        /// </summary>
        /// <param name="asset">The Asset that is to be created</param>
        public void CreateAsset(Asset asset)
        {
            db.Save(asset);
        }
    }

    public class AssetExport
    {
        // Variables for the Results Export Functionality 
        // Used to creat the Excel File from the Data
        public string NamTrack { get; set; }
        public string NamDeviceType { get; set; }
        public string NamManufacture { get; set; }
        public string NamOS { get; set; }
        public string NamDepartment { get; set; } 
        public string Model { get; set; }
        public string Description { get; set; }
        public string Hostname { get; set; }
        public string IPAddress { get; set; }
        public string ServiceTag { get; set; }
        public string ExpressCode { get; set; }
        public DateTime ShipDate { get; set; }
        public string Comments { get; set; }
    }
}