using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssetRegister.Poco;
using System.Web.Security;

namespace AssetRegister
{
    public partial class GeneralAssetInfoPanel : System.Web.UI.UserControl
    {
        /// <summary>
        /// Function that runs when the page is being loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user has logged in and redirect to the login page in they havnt
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            if (!IsPostBack)
            {
                ddlBinding();

                // Check the query string for an AssetId to determin if its a new asset or updaing an existing asset
                int assetID;
                if (!int.TryParse(Request.QueryString["AssetId"], out assetID))
                {
                    // Set the Page Title
                    Page.Title = "New General Asset";
                }
                else
                {
                    GeneralAsset asset = new GeneralAsset().getAsset(assetID);

                    // Set the Page Title
                    Page.Title = asset.Description;

                    // set the selected index's in the dropdown list
                    ddlAssetType.SelectedValue = asset.IdtAssetType.ToString();
                    ddlTrack.SelectedValue = asset.IdtTrack.ToString();
                    ddlLocation.SelectedValue = asset.IdtLocation.ToString();

                    tbNumAssets.Text = asset.NumAssets.ToString();
                    tbOther.Text = asset.Other;
                    tbDescription.Text = asset.Description;

                    if (!asset.ShipDate.HasValue)
                        tbShipDate.Text = "";
                    else
                        tbShipDate.Text = asset.ShipDate.Value.ToString("dd-MMM-yyyy");
                    
                    tbComments.Text = asset.Comments;

                    // Set the checkbox to show if the asset is inactive
                    if (asset.Inactive == 0)
                        chkShowInactive.Checked = false;
                    else
                        chkShowInactive.Checked = true;
                }
            }
        }
        
        /// <summary>
        /// Bind all the Drop Down Lists to the data from the Lookup tables
        /// </summary>
        private void ddlBinding()
        {
            List<Track> tracks = new List<Track>();
            tracks = new Track().getTracks();
            ddlTrack.DataTextField = "NamTrack";
            ddlTrack.DataValueField = "IdtTrack";
            ddlTrack.DataSource = tracks;
            ddlTrack.DataBind();
            ddlTrack.Items.Insert(0, new ListItem("- Choose Track -", "NULL"));

            List<AssetType> ass = new List<AssetType>();
            ass = new AssetType().getAssetTypes();
            ddlAssetType.DataTextField = "NamAssetType";
            ddlAssetType.DataValueField = "IdtAssetType";
            ddlAssetType.DataSource = ass;
            ddlAssetType.DataBind();
            ddlAssetType.Items.Insert(0, new ListItem("- Choose Asset Type -", "NULL"));

            List<Location> locs = new List<Location>();
            locs = new Location().getLocations();
            ddlLocation.DataTextField = "NamLocation";
            ddlLocation.DataValueField = "IdtLocation";
            ddlLocation.DataSource = locs;
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("- Choose Location -", "NULL"));
        }

        #region Button Functions
        /// <summary>
        /// Function that runs when the Save Button is clicked.
        /// Checks if its a new asset or just updating an existing using the querystring
        /// Returns the user to the Default page with an attached response message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string response = "";
            try
            {
                // Find out if this is a new asset or updating a previous asset by using the query string
                int assetID;
                if (!int.TryParse(Request.QueryString["AssetId"], out assetID))
                {
                    GeneralAsset asset = new GeneralAsset();

                    // Assign all the values to the values from the controls
                    asset.IdtTrack = int.Parse(ddlTrack.SelectedValue);
                    asset.IdtAssetType = int.Parse(ddlAssetType.SelectedValue);
                    asset.IdtLocation = int.Parse(ddlLocation.SelectedValue);

                    asset.Other = tbOther.Text;
                    asset.Description = tbDescription.Text;
                    asset.NumAssets = int.Parse(tbNumAssets.Text);

                    DateTime outDate;
                    if (!DateTime.TryParse(tbShipDate.Text, out outDate))
                        asset.ShipDate = null;
                    else
                        asset.ShipDate = outDate;

                    asset.Comments = tbComments.Text;
                    
                    // Set the flag to check if its active or inactive assets to display
                    if (chkShowInactive.Checked)
                        asset.Inactive = 1;
                    else
                        asset.Inactive = 0;

                    // Create the asset using the Asset Class
                    asset.CreateAsset(asset);

                    response = "Asset Created Successfully";
                }
                else
                {
                    // Return the Asset using the assetID using the Asset class
                    GeneralAsset asset = new GeneralAsset().getAsset(assetID);
                    // Assign all the values to the values from the controls
                    asset.IdtTrack = int.Parse(ddlTrack.SelectedValue);
                    asset.IdtAssetType = int.Parse(ddlAssetType.SelectedValue);
                    asset.IdtLocation = int.Parse(ddlLocation.SelectedValue);

                    asset.Other = tbOther.Text;
                    asset.Description = tbDescription.Text;
                    asset.NumAssets = int.Parse(tbNumAssets.Text);

                    DateTime outDate;
                    if (!DateTime.TryParse(tbShipDate.Text, out outDate))
                        asset.ShipDate = null;
                    else
                        asset.ShipDate = outDate;

                    asset.Comments = tbComments.Text;
                    
                    // Set the flag to check if its active or inactive assets to display
                    if (chkShowInactive.Checked)
                        asset.Inactive = 1;
                    else
                        asset.Inactive = 0;

                    // Update the asset using the Asset Class
                    asset.UpdateAsset(asset);

                    response = "Asset Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("Default.aspx?err=true&response=" + ex.Message); // Redirect back to the table of assets and pass bakc the error
            }

            Response.Redirect("Default.aspx?response=" + response); // Redirect back to the table of assets and pass back the message
        }

        /// <summary>
        /// Function that runs when the Cancel Button is pressed
        /// Redirects back to the Default page without passing a response
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx"); // Redirect back to the table of assets
        }
        #endregion
    }
}