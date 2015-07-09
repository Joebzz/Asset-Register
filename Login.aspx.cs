using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssetRegister
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Funciton to validate if the user can login with the provided deatils or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ValidateUser(object sender, EventArgs e)
        {
            int userId = 0;
            string constr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString; // Use the connectionString from the Web.Config to connect to the DB
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Validate_User"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", loginAssetRegister.UserName);
                    cmd.Parameters.AddWithValue("@Password", loginAssetRegister.Password);
                    cmd.Connection = con;
                    con.Open();
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                switch (userId)
                {
                    case -1:
                        loginAssetRegister.FailureText = "Username and/or password is incorrect.";
                        break;
                    case -2:
                        loginAssetRegister.FailureText = "Account has not been activated.";
                        break;
                    default:
                        FormsAuthentication.RedirectFromLoginPage(loginAssetRegister.UserName, loginAssetRegister.RememberMeSet);
                        break;
                }
            }
        }
    }
}