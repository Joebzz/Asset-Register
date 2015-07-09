using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssetRegister.Poco;
using System.Data;
using System.Drawing;
using System.Web.Security;
using System.IO;

namespace AssetRegister
{
    public partial class Default : System.Web.UI.Page
    {
        #region Page Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user has logged in
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            // Check the query string for a respose message and display if its found
            if (Request.QueryString["response"] != null && Request.QueryString["response"] != "")
            {
                if (Request.QueryString["err"] == "true")
                {
                    divReturnValue.Attributes["class"] = "alert alert-danger";
                    divReturnValue.InnerHtml = "";
                    divReturnValue.InnerHtml += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                    divReturnValue.InnerHtml += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'></span><span class='sr-only'>Error:</span>";
                }
                else
                {
                    divReturnValue.Attributes["class"] = "alert alert-success";
                    divReturnValue.InnerHtml = "";
                    divReturnValue.InnerHtml += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                    divReturnValue.InnerHtml += "<span class='glyphicon glyphicon-ok' aria-hidden='true'></span><span class='sr-only'>Well done:</span>";
                }

                divReturnValue.InnerHtml += " " + Request.QueryString["response"];
            }

            if (!IsPostBack)
            {
                ddl_DataBind();
                if (Session["AssetTable"] == null)
                {
                    reloadAssets(this, EventArgs.Empty);
                }
                else 
                {
                    grdAssets.DataSource = Session["AssetTable"];
                    grdAssets.DataBind();
                }
            }            
        }

        #endregion

        #region Data Functions
        // Bind the Data from the lookup tables to the Drop Down lists
        protected void ddl_DataBind()
        {
            List<Track> tracks = new List<Track>();
            tracks = new Track().getTracks();
            ddlTrack.DataTextField = "NamTrack";
            ddlTrack.DataValueField = "IdtTrack";
            ddlTrack.DataSource = tracks;
            ddlTrack.DataBind();
            ddlTrack.Items.Insert(0, new ListItem("- Filter By Track -", "")); // Add empty option
            if (Session["Track"] != null)
                ddlTrack.SelectedIndex = int.Parse(Session["Track"].ToString());

            List<DeviceType> devs = new List<DeviceType>();
            devs = new DeviceType().getDeviceTypes();
            ddlDeviceType.DataTextField = "NamDeviceType";
            ddlDeviceType.DataValueField = "IdtDeviceType";
            ddlDeviceType.DataSource = devs;
            ddlDeviceType.DataBind();
            ddlDeviceType.Items.Insert(0, new ListItem("- Filter By Device Type -", "")); // Add empty option
            if (Session["DeviceType"] != null)
                ddlDeviceType.SelectedIndex = int.Parse(Session["DeviceType"].ToString());

            List<Department> depts = new List<Department>();
            depts = new Department().getDepartments();
            ddlDepartment.DataTextField = "NamDepartment";
            ddlDepartment.DataValueField = "IdtDepartment";
            ddlDepartment.DataSource = depts;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("- Filter By Department -", "")); // Add empty option  
            if (Session["Department"] != null)
                ddlDepartment.SelectedIndex = int.Parse(Session["Department"].ToString());

            List<Manufacture> mans = new List<Manufacture>();
            mans = new Manufacture().getManufactures();
            ddlManufacture.DataTextField = "NamManufacture";
            ddlManufacture.DataValueField = "IdtManufacture";
            ddlManufacture.DataSource = mans;
            ddlManufacture.DataBind();
            ddlManufacture.Items.Insert(0, new ListItem("- Filter By Manufacture -", "")); // Add empty option
            if (Session["Manufacture"] != null)
                ddlManufacture.SelectedIndex = int.Parse(Session["Manufacture"].ToString());

            List<OS> oss = new List<OS>();
            oss = new OS().getOSs();
            ddlOS.DataTextField = "NamOS";
            ddlOS.DataValueField = "IdtOS";
            ddlOS.DataSource = oss;
            ddlOS.DataBind();
            ddlOS.Items.Insert(0, new ListItem("- Filter By OS -", "")); // Add empty option         
            if (Session["OS"] != null)
                ddlOS.SelectedIndex = int.Parse(Session["OS"].ToString());
        }
        
        // Rebind the Assets for the GridView 
        protected void reloadAssets(object sender, EventArgs e)
        {
            // Check the show inactive box to see if its active or inactive assets to display
            int showInactive = 0;
            if (chkShowInactive.Checked)
            {
                // Reset the Hostname Search
                tbSearchHostName.Text = "";

                // Reset the Description Search
                tbSearchDescription.Text = "";

                showInactive = 1;
            }
                    
            // Create nullable integers and assign them to null
            int? trkId = null, devId = null, osId = null, manId = null, deptId = null;
            Asset ass = new Asset();

            if (ddlTrack.SelectedIndex != 0)
                trkId = int.Parse(ddlTrack.SelectedValue);
            Session["Track"] = trkId;

            if (ddlDeviceType.SelectedIndex != 0)
                devId = int.Parse(ddlDeviceType.SelectedValue);
            Session["DeviceType"] = devId;

            if (ddlOS.SelectedIndex != 0)
                osId = int.Parse(ddlOS.SelectedValue);
            Session["OS"] = osId;

            if (ddlManufacture.SelectedIndex != 0)
                manId = int.Parse(ddlManufacture.SelectedValue);
            Session["Manufacture"] = manId;
            
            if (ddlDepartment.SelectedIndex != 0)
                deptId = int.Parse(ddlDepartment.SelectedValue);
            Session["Department"] = deptId;

            IList<Asset> assets = ass.getAssets(trkId, devId, osId, manId, deptId, showInactive);
            Session["AssetTable"] = assets;
            grdAssets.DataSource = assets;
            grdAssets.DataBind();
        }

        // Returns the Assets for the Export Function adds Track Names, Device Types, OS Name, Manufacture Names, Department Names
        private IList<AssetExport> getAssetsForExport()
        {
            // Check the show inactive box to see if its active or inactive assets to display
            int showInactive = 0;
            if (chkShowInactive.Checked)
            {
                // Reset the Hostname Search
                tbSearchHostName.Text = "";

                // Reset the Description Search
                tbSearchDescription.Text = "";

                showInactive = 1;
            }

            // Create nullable integers and assign them to null
            int? trkId = null, devId = null, osId = null, manId = null, deptId = null;
            Asset ass = new Asset();

            if (ddlTrack.SelectedIndex != 0)
                trkId = int.Parse(ddlTrack.SelectedValue);

            if (ddlDeviceType.SelectedIndex != 0)
                devId = int.Parse(ddlDeviceType.SelectedValue);

            if (ddlOS.SelectedIndex != 0)
                osId = int.Parse(ddlOS.SelectedValue);

            if (ddlManufacture.SelectedIndex != 0)
                manId = int.Parse(ddlManufacture.SelectedValue);

            if (ddlDepartment.SelectedIndex != 0)
                deptId = int.Parse(ddlDepartment.SelectedValue);

            IList<AssetExport> assets = ass.getAssetsForExport(trkId, devId, osId, manId, deptId, showInactive);
            return assets;            
        }
        #endregion
        
        #region GridView Row Functions
        protected void grdAssets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;

            // Execute the following logic for Items and Alternating Items.
            if (row.RowType == DataControlRowType.DataRow)
            {
                row.CssClass = "DataRow";

                Asset ass = (Asset)row.DataItem;

                LinkButton btnEdit = (LinkButton)row.Cells[8].Controls[0];
                btnEdit.CommandArgument = ass.IdtAsset.ToString();
            }
        }

        protected void grdAssets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // If multiple buttons are used in a GridView control, use the
            // CommandName property to determine which button was clicked.
            if (e.CommandName == "Edit")
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int idtAsset = Convert.ToInt32(e.CommandArgument);

                // Redirect to AssetInfoPage and pass in asset id
                Response.Redirect("AssetInfo.aspx?assetId=" + idtAsset);
            }
        }
        
        #endregion

        #region GridView Paging Functions
        /// <summary>
        /// Changes the page of the GridView 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdAssets_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAssets.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// Reloads the assets when the page index changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdAssets_OnPageIndexChanged(object sender, EventArgs e)
        {
            reloadAssets(this, EventArgs.Empty);
        }
        #endregion
        
        #region Search Functions
        /// <summary>
        /// Function that runs when the Search Description Button is Pressed
        /// Search's for Assets wiht a descrition 'LIKE' what has been entered into the Search Field 
        /// Resets all the other search fields, cannot do multiple search options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchDescription_Click(object sender, EventArgs e)
        {
            // Reset the Hostname Search
            tbSearchIp.Text = "172.17.";
            tbSearchHostName.Text = "";

            if (tbSearchDescription.Text.Length > 0)
            {
                ddl_DataBind();
                Asset ass = new Asset();
                IList<Asset> assets = ass.getAssets(tbSearchDescription.Text, "", "");

                grdAssets.DataSource = assets;
                grdAssets.DataBind();
            }
        }

        /// <summary>
        /// Function that runs when the Search Description Button is Pressed
        /// Search's for Assets with a Hostname 'LIKE' what has been entered into the Search Field 
        /// Resets all the other search fields, cannot do multiple search options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchHostName_Click(object sender, EventArgs e)
        {
            // Reset the Description Search
            tbSearchIp.Text = "172.17.";
            tbSearchDescription.Text = "";

            if (tbSearchHostName.Text.Length > 0)
            {
                ddl_DataBind();
                Asset ass = new Asset();
                IList<Asset> assets = ass.getAssets("", tbSearchHostName.Text, "");

                grdAssets.DataSource = assets;
                grdAssets.DataBind();
            }
        }

        /// <summary>
        /// Function that runs when the Search Description Button is Pressed
        /// Search's for Assets with a IP Adress 'LIKE' what has been entered into the Search Field 
        /// Resets all the other search fields, cannot do multiple search options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchIp_Click(object sender, EventArgs e)
        {
            // Reset the Description Search
            tbSearchHostName.Text = "";
            tbSearchDescription.Text = "";

            if (tbSearchIp.Text.Length > 0)
            {
                ddl_DataBind();
                Asset ass = new Asset();
                IList<Asset> assets = ass.getAssets("", "", tbSearchIp.Text);

                grdAssets.DataSource = assets;
                grdAssets.DataBind();
            }
        }
       
        #endregion

        #region Button Functions
        protected void btnNewAsset_Click(object sender, EventArgs e)
        {

            Response.Redirect("AssetInfo.aspx"); // Refirect to AssetInfo Page without passing in the Asset ID will show that its a new Asset
        }
    
        /// <summary>
        /// Function that runs when the Export Button is Pressed
        /// Creates a Excel File that downloads with all the asset in the selected filter's 
        /// Convert's the ID's to String's for the drop down lists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=IT_Assets_Export.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                using(HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = getAssetsForExport();
                    dg.DataBind();
                    dg.RenderControl(htw);

                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        #endregion

        #region Convert ID's to String Functions
        protected string GetTrackName(int id)
        {
            string trkName = "";
            // Get the Track Name
            Track trk = new Track();
            trkName = trk.getTrack(id).NamTrack;
            return trkName;
        }

        protected string GetDeviceType(int id)
        {
            string devName = "";
            // Get the device type
            DeviceType dev = new DeviceType();
            devName = dev.getDeviceType(id).NamDeviceType;
            return devName;
        }
        #endregion

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}