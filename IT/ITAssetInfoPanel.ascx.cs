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
    public partial class ITAssetInfoPanel : System.Web.UI.UserControl
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
                    Page.Title = "New IT Asset";
                }
                else
                {
                    ITAsset asset = new ITAsset().getAsset(assetID);

                    // Set the Page Title
                    Page.Title = asset.Description;

                    // set the selected index's in the dropdown list
                    ddlDeviceType.SelectedValue = asset.IdtDeviceType.ToString();
                    ddlManufacture.SelectedValue = asset.IdtManufacture.ToString();
                    ddlOS.SelectedValue = asset.IdtOS.ToString();
                    ddlTrack.SelectedValue = asset.IdtTrack.ToString();
                    ddlDepartment.SelectedValue = asset.IdtDepartment.ToString();

                    tbModel.Text = asset.Model;
                    tbDescription.Text = asset.Description;
                    tbExpressCode.Text = asset.ExpressCode;
                    tbServiceTag.Text = asset.ServiceTag;
                    tbHostname.Text = asset.Hostname;
                    tbIPAddress.Text = asset.IPAddress;

                    tbCurrentValue.Text = asset.CurrentValue.ToString();
                    tbCostValue.Text = asset.CostValue.ToString();

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
        /// Function to enter the Subnet for each track when the Track Drop Down is changed
        /// It will only modify if it does not find a valid IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbIPAddress.Text == "" || tbIPAddress.Text.EndsWith("."))
            {
                Track trk = new Track().getTrack(ddlTrack.SelectedIndex); // get the index of the track ddl
                tbIPAddress.Text = trk.IPSubnet.TrimEnd('#'); // Remove the trailing # symbol
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

            List<DeviceType> devs = new List<DeviceType>();
            devs = new DeviceType().getDeviceTypes();
            ddlDeviceType.DataTextField = "NamDeviceType";
            ddlDeviceType.DataValueField = "IdtDeviceType";
            ddlDeviceType.DataSource = devs;
            ddlDeviceType.DataBind();
            ddlDeviceType.Items.Insert(0, new ListItem("- Choose Device Type -", "NULL"));

            List<Department> depts = new List<Department>();
            depts = new Department().getDepartments();
            ddlDepartment.DataTextField = "NamDepartment";
            ddlDepartment.DataValueField = "IdtDepartment";
            ddlDepartment.DataSource = depts;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("- Choose Department -", "NULL"));

            List<Manufacture> mans = new List<Manufacture>();
            mans = new Manufacture().getManufactures();
            ddlManufacture.DataTextField = "NamManufacture";
            ddlManufacture.DataValueField = "IdtManufacture";
            ddlManufacture.DataSource = mans;
            ddlManufacture.DataBind();
            ddlManufacture.Items.Insert(0, new ListItem("- Choose Manufacture -", "NULL"));

            List<OS> oss = new List<OS>();
            oss = new OS().getOSs();
            ddlOS.DataTextField = "NamOS";
            ddlOS.DataValueField = "IdtOS";
            ddlOS.DataSource = oss;
            ddlOS.DataBind();
            ddlOS.Items.Insert(0, new ListItem("- Choose OS -", "NULL"));
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
                    ITAsset asset = new ITAsset();

                    // Assign all the values to the values from the controls
                    asset.IdtTrack = int.Parse(ddlTrack.SelectedValue);
                    asset.IdtDeviceType = int.Parse(ddlDeviceType.SelectedValue);
                    asset.IdtManufacture = int.Parse(ddlManufacture.SelectedValue);
                    asset.IdtOS = int.Parse(ddlOS.SelectedValue);
                    asset.IdtDepartment = int.Parse(ddlDepartment.SelectedValue);

                    asset.Model = tbModel.Text;
                    asset.Description = tbDescription.Text;
                    asset.Hostname = tbHostname.Text;
                    asset.IPAddress = tbIPAddress.Text;
                    asset.ServiceTag = tbServiceTag.Text;
                    asset.ExpressCode = tbExpressCode.Text;

                    int costVal, curVal;
                    if(!int.TryParse(tbCostValue.Text, out costVal))
                        costVal = 0;
                    if (!int.TryParse(tbCurrentValue.Text, out curVal))
                        curVal = 0;

                    asset.CostValue = costVal;
                    asset.CurrentValue = curVal;

                    DateTime outDate;
                    if (!DateTime.TryParse(tbShipDate.Text, out outDate))
                        asset.ShipDate = new DateTime(1900, 1, 1);
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
                    ITAsset asset = new ITAsset().getAsset(assetID);

                    // Assign all the values to the values from the controls
                    asset.IdtTrack = int.Parse(ddlTrack.SelectedValue);
                    asset.IdtDeviceType = int.Parse(ddlDeviceType.SelectedValue);
                    asset.IdtManufacture = int.Parse(ddlManufacture.SelectedValue);
                    asset.IdtOS = int.Parse(ddlOS.SelectedValue);
                    asset.IdtDepartment = int.Parse(ddlDepartment.SelectedValue);

                    asset.Model = tbModel.Text;
                    asset.Description = tbDescription.Text;
                    asset.Hostname = tbHostname.Text;
                    asset.IPAddress = tbIPAddress.Text;
                    asset.ServiceTag = tbServiceTag.Text;
                    asset.ExpressCode = tbExpressCode.Text;
                    
                    int costVal, curVal;
                    if (!int.TryParse(tbCostValue.Text, out costVal))
                        costVal = 0;
                    if (!int.TryParse(tbCurrentValue.Text, out curVal))
                        curVal = 0;

                    asset.CostValue = costVal;
                    asset.CurrentValue = curVal;

                    DateTime outDate;
                    if (!DateTime.TryParse(tbShipDate.Text, out outDate))
                        asset.ShipDate = new DateTime(1900, 1, 1);
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