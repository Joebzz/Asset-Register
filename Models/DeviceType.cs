using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace AssetRegister.Poco
{
    [TableName("LookupDeviceType")] // Name of the Table in the Database
    [PrimaryKey("IdtDeviceType")] // Primary Key of the Table
    public class DeviceType
	{
        private PetaPoco.Database db = new PetaPoco.Database("connectionString"); // Use the connectionString from the Web.Config to connect to the DB

        // Variables below are dirtectly related to the Column Names of the DB Table
        // These are the Variables that all DeviceType Objects will have
        public int IdtDeviceType { get; set; }
        public string NamDeviceType { get; set; }

        /// <summary>
        ///  Returns the Device Type found with the provided ID
        /// </summary>
        /// <param name="id">The ID of the DeviceType</param>
        /// <returns>Device Type</returns>
        public DeviceType getDeviceType(int id)
        {            
            var DeviceType = db.FirstOrDefault<DeviceType>("WHERE IdtDeviceType = '" + id + "'");
            return DeviceType;
        }

        /// <summary>
        /// Used by the drop down list of Device Types
        /// </summary>
        /// <returns>All Device Types from the DB table</returns>
        public List<DeviceType> getDeviceTypes()
        {
            var DeviceTypes = db.Fetch<DeviceType>();
            return DeviceTypes;
        }
	}
}