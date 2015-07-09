using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace AssetRegister.Poco
{
    [TableName("LookupDepartment")] // Name of the Table in the Database
    [PrimaryKey("IdtDepartment")] // Primary Key of the Table
    public class Department
	{
        private PetaPoco.Database db = new PetaPoco.Database("connectionString"); // Use the connectionString from the Web.Config to connect to the DB

        // Variables below are dirtectly related to the Column Names of the DB Table
        // These are the Variables that all Department Objects will have
        public int IdtDepartment { get; set; }
        public string NamDepartment { get; set; }

        /// <summary>
        ///  Returns the Department found with the provided ID
        /// </summary>
        /// <param name="id">The ID of the Department</param>
        /// <returns>Department</returns>
        public Department getDepartment(int id)
        {            
            var Department = db.FirstOrDefault<Department>("WHERE IdtDepartment = '" + id + "'"); 
            return Department;
        }

        /// <summary>
        /// Used by the drop down list of Department
        /// </summary>
        /// <returns>All Departments from the DB table</returns>
        public List<Department> getDepartments()
        {
            var Departments = db.Fetch<Department>();
            return Departments;
        }
	}
}