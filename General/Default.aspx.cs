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
    public partial class General : System.Web.UI.Page
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
            else
            {
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
                    reloadAssets(this, EventArgs.Empty);
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

            List<AssetType> ass = new List<AssetType>();
            ass = new AssetType().getAssetTypes();
            ddlAssetType.DataTextField = "NamAssetType";
            ddlAssetType.DataValueField = "IdtAssetType";
            ddlAssetType.DataSource = ass;
            ddlAssetType.DataBind();
            ddlAssetType.Items.Insert(0, new ListItem("- Filter By Asset Type -", "")); // Add empty option
            if (Session["AssetType"] != null)
                ddlAssetType.SelectedIndex = int.Parse(Session["AssetType"].ToString());

            List<Location> locs = new List<Location>();
            locs = new Location().getLocations();
            ddlLocation.DataTextField = "NamLocation";
            ddlLocation.DataValueField = "IdtLocation";
            ddlLocation.DataSource = locs;
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("- Filter By Location -", "")); // Add empty option
            if (Session["Location"] != null)
                ddlLocation.SelectedIndex = int.Parse(Session["Location"].ToString());
        }
        
        // Rebind the Assets for the GridView 
        protected void reloadAssets(object sender, EventArgs e)
        {
            // Check the show inactive box to see if its active or inactive assets to display
            int showInactive = 0;
            if (chkShowInactive.Checked)
            {
                // Reset the Description Search
                tbSearchDescription.Text = "";

                showInactive = 1;
            }
                    
            // Create nullable integers and assign them to null
            int? trkId = null, assTypeId = null, locId = null;
            GeneralAsset ass = new GeneralAsset();

            if (ddlTrack.SelectedIndex != 0)
                trkId = int.Parse(ddlTrack.SelectedValue);
            Session["Track"] = trkId;

            if (ddlAssetType.SelectedIndex != 0)
                assTypeId = int.Parse(ddlAssetType.SelectedValue);
            Session["AssetType"] = assTypeId;

            if (ddlLocation.SelectedIndex != 0)
                locId = int.Parse(ddlLocation.SelectedValue);
            Session["Location"] = locId;

            IList<GeneralAsset> assets = ass.getAssets(trkId, assTypeId, locId, showInactive);
            Session["AssetTable"] = assets;
            grdAssets.DataSource = assets;
            grdAssets.DataBind();
        }

        // Returns the Assets for the Export Function adds Track Names, Device Types, OS Name, Manufacture Names, Department Names
        private IList<GeneralAssetExport> getAssetsForExport()
        {
            // Check the show inactive box to see if its active or inactive assets to display
            int showInactive = 0;
            if (chkShowInactive.Checked)
            {
                // Reset the Description Search
                tbSearchDescription.Text = "";

                showInactive = 1;
            }

            // Create nullable integers and assign them to null
            int? trkId = null, assTypeId = null, locId = null;
            GeneralAsset ass = new GeneralAsset();

            if (ddlTrack.SelectedIndex != 0)
                trkId = int.Parse(ddlTrack.SelectedValue);

            if (ddlAssetType.SelectedIndex != 0)
                assTypeId = int.Parse(ddlAssetType.SelectedValue);

            if (ddlLocation.SelectedIndex != 0)
                locId = int.Parse(ddlLocation.SelectedValue);

            IList<GeneralAssetExport> assets = ass.getAssetsForExport(trkId, assTypeId, locId, showInactive);
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

                GeneralAsset ass = (GeneralAsset)row.DataItem;

                LinkButton btnEdit = (LinkButton)row.Cells[7].Controls[0];
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
                Response.Redirect("GeneralAssetInfo.aspx?assetId=" + idtAsset);
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
            if (tbSearchDescription.Text.Length > 0)
            {
                ddl_DataBind();
                GeneralAsset ass = new GeneralAsset();
                IList<GeneralAsset> assets = ass.getAssets(tbSearchDescription.Text);

                grdAssets.DataSource = assets;
                grdAssets.DataBind();
            }
        }
        /*
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
       */
        #endregion

        #region Button Functions
        protected void btnNewAsset_Click(object sender, EventArgs e)
        {
            Response.Redirect("GeneralAssetInfo.aspx"); // Refirect to AssetInfo Page without passing in the Asset ID will show that its a new Asset
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
            GridView gv = new GridView();
            gv.DataSource = getAssetsForExport();
            gv.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=General_Assets " + DateTime.Now.ToString("ddMMMyyyy") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gv.AllowPaging = false;

                gv.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gv.HeaderRow.Cells)
                {
                    cell.BackColor = gv.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gv.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gv.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gv.RowStyle.BackColor;
                        }
                        //cell.CssClass = "textmode";
                    }
                }

                gv.RenderControl(hw);

                //style to format numbers to string
                //string style = @"<style> .textmode { mso-number-format:'0'; } </style> ";
                //Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
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

        protected string GetAssetType(int id)
        {
            string assName = "";
            // Get the device type
            AssetType ass = new AssetType();
            assName = ass.getAssetType(id).NamAssetType;
            return assName;
        }

        protected string GetLocation(int id)
        {
            string locName = "";
            // Get the device type
            Location ass = new Location();
            locName = ass.getLocation(id).NamLocation;
            return locName;
        }
        #endregion

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}