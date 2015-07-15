using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssetRegister
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {// Check if user has logged in
            if (!this.Page.User.Identity.IsAuthenticated)
                FormsAuthentication.RedirectToLoginPage();           
            else if (this.Page.User.Identity.Name == "General")
                Response.Redirect("General"); // Redirect to the General Assets Page
            else if (this.Page.User.Identity.Name == "IT")
                Response.Redirect("IT"); // Redirect to IT Assets Page            
        }

        public void LogoutLink_OnClick(object sender, EventArgs args)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}